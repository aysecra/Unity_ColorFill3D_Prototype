using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] GameObject _groundPrefab, _wallPrefab, _collectedPrefab;
    [SerializeField] Transform _gridParent;


    void Awake()
    {
        for (int i = 0; i <= 10; i++)
        {
            for (int j = 0; j <= 13; j++)
            {
                if (i == 0 && j == 0)
                    Instantiate(_groundPrefab, new Vector3(i, 0, i), Quaternion.identity, _gridParent);
                else
                {
                    if (i == 9 || i == 10 || j == 13)
                    {
                        Instantiate(_wallPrefab, new Vector3(i, 1, j), Quaternion.identity, _gridParent);
                        Instantiate(_wallPrefab, new Vector3(i, 1, -j), Quaternion.identity, _gridParent);
                        Instantiate(_wallPrefab, new Vector3(-i, 1, j), Quaternion.identity, _gridParent);
                        Instantiate(_wallPrefab, new Vector3(-i, 1, -j), Quaternion.identity, _gridParent);
                    }
                    else
                    {
                        Instantiate(_groundPrefab, new Vector3(i, 0, j), Quaternion.identity, _gridParent);
                        Instantiate(_groundPrefab, new Vector3(i, 0, -j), Quaternion.identity, _gridParent);
                        Instantiate(_groundPrefab, new Vector3(-i, 0, j), Quaternion.identity, _gridParent);
                        Instantiate(_groundPrefab, new Vector3(-i, 0, -j), Quaternion.identity, _gridParent);

                        Instantiate(_collectedPrefab, new Vector3(i, 1, j), Quaternion.identity, _gridParent);
                        Instantiate(_collectedPrefab, new Vector3(i, 1, -j), Quaternion.identity, _gridParent);
                        Instantiate(_collectedPrefab, new Vector3(-i, 1, j), Quaternion.identity, _gridParent);
                        Instantiate(_collectedPrefab, new Vector3(-i, 1, -j), Quaternion.identity, _gridParent);
                    }
                }
            }
        }
    }
}
