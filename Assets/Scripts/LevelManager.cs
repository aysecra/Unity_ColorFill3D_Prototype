using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] GameObject _groundPrefab, _wallPrefab, _collectedPrefab, _gatePrefab;
    [SerializeField] Transform _gridParent, _wallParent;
    [SerializeField] int _xSize, _ySize;

    public int YSize { get => _ySize; }
    public int XSize { get => _xSize; }

    void Awake()
    {
        for (int i = 0; i <= XSize; i++)
        {
            for (int j = 0; j <= YSize; j++)
            {
                if (i == XSize - 1 || i == XSize || j == YSize)
                {
                    if (new Vector3(0, 1, 10) != new Vector3(i, 1, j))
                    {
                        Instantiate(_wallPrefab, new Vector3(i, 1, j), Quaternion.identity, _wallParent);
                        Instantiate(_wallPrefab, new Vector3(i, 1, -j), Quaternion.identity, _wallParent);
                        Instantiate(_wallPrefab, new Vector3(-i, 1, j), Quaternion.identity, _wallParent);
                        Instantiate(_wallPrefab, new Vector3(-i, 1, -j), Quaternion.identity, _wallParent);
                    }
                }
                else
                {
                    GameObject current1 = Instantiate(_groundPrefab, new Vector3(i, 0, j), Quaternion.identity, _gridParent);
                    current1.GetComponent<Grid>().SetGrid(XSize + i - 2, YSize + j - 1); ;
                    GameObject current2 = Instantiate(_groundPrefab, new Vector3(i, 0, -j), Quaternion.identity, _gridParent);
                    current2.GetComponent<Grid>().SetGrid(XSize + i - 2, YSize - j - 1);
                    GameObject current3 = Instantiate(_groundPrefab, new Vector3(-i, 0, j), Quaternion.identity, _gridParent);
                    current3.GetComponent<Grid>().SetGrid(XSize - i - 2, YSize + j - 1);
                    GameObject current4 = Instantiate(_groundPrefab, new Vector3(-i, 0, -j), Quaternion.identity, _gridParent);
                    current4.GetComponent<Grid>().SetGrid(XSize - i - 2, YSize - j - 1);

                    Instantiate(_collectedPrefab, new Vector3(i, 1, j), Quaternion.identity, current1.transform).GetComponent<CollectedArea>().SetCollectedArea(XSize + i - 2, YSize + j - 1);
                    Instantiate(_collectedPrefab, new Vector3(i, 1, -j), Quaternion.identity, current2.transform).GetComponent<CollectedArea>().SetCollectedArea(XSize + i - 2, YSize - j - 1);
                    Instantiate(_collectedPrefab, new Vector3(-i, 1, j), Quaternion.identity, current3.transform).GetComponent<CollectedArea>().SetCollectedArea(XSize - i - 2, YSize + j - 1);
                    Instantiate(_collectedPrefab, new Vector3(-i, 1, -j), Quaternion.identity, current4.transform).GetComponent<CollectedArea>().SetCollectedArea(XSize - i - 2, YSize - j - 1);
                }
            }
        }
        Instantiate(_wallParent, new Vector3(0, 0, 2 * _ySize + 10), Quaternion.identity);
        Instantiate(_gridParent, new Vector3(0, 0, 2 * _ySize + 10), Quaternion.identity);

        for (int i = 10; i <= 20; i++)
        {
            Instantiate(_groundPrefab, new Vector3(0, 0, i), Quaternion.identity);
        }

        Instantiate(_gatePrefab, new Vector3(0, 1, 10), Quaternion.identity);
        Instantiate(_gatePrefab, new Vector3(0, 1, 20), Quaternion.identity);
        Instantiate(_wallPrefab, new Vector3(0, 1, 40), Quaternion.identity, _wallParent);
        Instantiate(_wallPrefab, new Vector3(0, 1, -10), Quaternion.identity, _wallParent);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (SceneManager.sceneCount > SceneManager.GetActiveScene().buildIndex)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
