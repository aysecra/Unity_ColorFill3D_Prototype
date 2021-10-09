using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Material _collectedMaterial, _oldMaterial;
    [SerializeField] GameObject _btnReload, _btnNext;
    private Stack<Transform> _stackCollected = new Stack<Transform>();
    private int _maxX = -1, _minX = 1000, _maxY = -1, _minY = 1000;

    void Awake()
    {
        transform.position = new Vector3(0, 1, -LevelManager.instance.YSize + 1);
    }

    void FixedUpdate()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
            case GameManager.GameState.MainGame:
                InputManager.instance.Move(transform, _speed);
                break;

            case GameManager.GameState.NextPart:
                break;

            case GameManager.GameState.FinishGame:
                _btnNext.SetActive(true);
                break;

            case GameManager.GameState.GameOver:
                _btnReload.SetActive(true);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CollectedArea ca))
        {
            print(ca);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            GameManager.instance.CurrentGameState = GameManager.GameState.GameOver;
        }

        if (other.gameObject.GetComponent<MeshRenderer>().material.name == "m_Collected (Instance)")
        {
            GameManager.instance.CurrentGameState = GameManager.GameState.GameOver;
        }

        if (other.gameObject.TryGetComponent(out Grid grid))
        {
            other.gameObject.GetComponent<MeshRenderer>().material = _collectedMaterial;
            _stackCollected.Push(other.transform);
        }

        if (other.gameObject.TryGetComponent(out Wall wall))
        {
            if (GameManager.instance.CurrentGameState == GameManager.GameState.MainGame)
            {
                foreach (Transform go in _stackCollected)
                {
                    go.GetChild(0).gameObject.SetActive(true);
                    go.GetComponent<MeshRenderer>().material = _oldMaterial;

                    if (_minX > go.gameObject.GetComponent<Grid>().X)
                        _minX = go.gameObject.GetComponent<Grid>().X;

                    if (_maxX < go.gameObject.GetComponent<Grid>().X)
                        _maxX = go.gameObject.GetComponent<Grid>().X;

                    if (_minY > go.gameObject.GetComponent<Grid>().Y)
                        _minY = go.gameObject.GetComponent<Grid>().Y;

                    if (_maxY < go.gameObject.GetComponent<Grid>().Y)
                        _maxY = go.gameObject.GetComponent<Grid>().Y;
                }
                ColorFill.instance.Fill((_maxX + _minX) / 2, (_maxY + _minY) / 2);
                _maxX = -1; _minX = 1000; _maxY = -1; _minY = 1000;
            }
        }  
    }
}
