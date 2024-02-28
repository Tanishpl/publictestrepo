using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mazegenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _cellprefab;

    [SerializeField] private int _mazewidth;
    [SerializeField] private int _mazedepth;

    private MazeCell[,] _mazeGrid;
    IEnumerator Start()
    {

        _mazeGrid = new MazeCell[_mazewidth, _mazedepth];
        for (int x = 0; x < _mazewidth; x++)
        {
            for (int z = 0; z < _mazedepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_cellprefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
    }

    private IEnumerator generatemaze(MazeCell previous, MazeCell current)
    {
        current.Visit();
    }

    private void clear(MazeCell previous, MazeCell current)
    {

    }
    void Update()
    {

    }
}
