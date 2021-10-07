using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Material _collectedMaterial;

    void FixedUpdate()
    {
        switch (GameManager.instance.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
            case GameManager.GameState.MainGame:
                InputManager.instance.Move(transform, _speed);
                break;

            case GameManager.GameState.NextLevel:
                break;

            case GameManager.GameState.FinishGame:
                break;

            case GameManager.GameState.GameOver:
                print("GAME OVER !!!");
                break;
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
        }

        if(other.gameObject.TryGetComponent(out Wall wall))
        {
            
        }
    }
}
