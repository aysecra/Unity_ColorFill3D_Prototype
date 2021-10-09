using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFill : MonoSingleton<ColorFill>
{
    [SerializeField] Transform _gridParent;
    [SerializeField] Material _newColor;
    static int _sizeX, _sizeY;
    private GameObject[,] _arrGrid;

    public void Fill(int x, int y)
    {
        CreateArray();
        FloodFill(x, y);
    }

    void FloodFill(int x, int y)
    {
        if (x < 0 || y < 0 || x > 2 * (_sizeX - 2) || y > 2 * (_sizeY - 1) || _arrGrid[x, y].transform.GetChild(0).gameObject.active == true)
            return;

        if (_arrGrid[x, y].transform.GetChild(0).gameObject.active == false)
        {
            _arrGrid[x, y].transform.GetChild(0).gameObject.SetActive(true);
            FloodFill(x + 1, y);
            FloodFill(x, y + 1);
            FloodFill(x - 1, y);
            FloodFill(x, y - 1);
        }

    }

    public void CreateArray()
    {
        _sizeX = LevelManager.instance.XSize;
        _sizeY = LevelManager.instance.YSize;
        _arrGrid = new GameObject[2 * (_sizeX - 2) + 1, 2 * (_sizeY - 1) + 1];

        foreach (Transform child in _gridParent)
        {
            int x = child.gameObject.GetComponent<Grid>().X;
            int y = child.gameObject.GetComponent<Grid>().Y;
            _arrGrid[x, y] = child.gameObject;
        }
    }
}
