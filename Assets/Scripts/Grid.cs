using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int x, y;
    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    public void  SetGrid(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public GameObject GetGrid()
    {
        return gameObject;
    }
}
