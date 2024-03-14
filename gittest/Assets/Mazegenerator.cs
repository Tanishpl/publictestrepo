using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Mazegenerator : MonoBehaviour
{
    [SerializeField] private MazeCell _cellprefab;

    [SerializeField] private int _mazewidth;
    [SerializeField] private int _mazedepth;

    private MazeCell[,] _mazeGrid;
    void Start()
    {
         IEnumerator MazeCoroutine = StartMaze();
         StartCoroutine(MazeCoroutine);
    }

    private IEnumerator StartMaze()
    {
        _mazeGrid = new MazeCell[_mazewidth, _mazedepth];
        for (int x = 0; x < _mazewidth; x++)
        {
            for (int z = 0; z < _mazedepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_cellprefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }
        //yields-- so this should be the last to be called 
        yield return GenerateMaze(null, _mazeGrid[0, 0]);//--actually read this one--there is not previous and the current is the starting point-should turn this into a variable later so i can make it movebale
    }

    private IEnumerator GenerateMaze(MazeCell previous, MazeCell current)
    {
        current.Visit();//set starting pointed as visited so you dont go back
        Clear(previous, current);//to carve out inital walls and get going-- the null is always just 0 for the current to always be larger than and the "previous" just returns nothing

        yield return new WaitForSeconds(0.05f);//this is not nessary but means  i can actually see the maze generate

        MazeCell chosenCell;
        do
        {
            chosenCell = FindNextCell(current);
            if (chosenCell != null) //if there is an unvisited cell
            {
                yield return GenerateMaze(current, chosenCell); //recursion,calls itself until no unvisited cells left(hopefully)
            }
        } while (chosenCell != null);//stops after no cells left in its current radius
        print("done");
    }

    private MazeCell FindNextCell(MazeCell currentCell)//returns only 1 cell randomly
    {

        //use nextcells to find the neighboroughing cells to the current cell and put them in a collection
        IEnumerable<MazeCell> unvisitedCells = NextCells(currentCell);//idk why ienumerable works here and not list

        //returns a random cell in that collection of unvisited cells 
        return unvisitedCells.OrderBy(placeholder => Random.Range(1, 10)).FirstOrDefault();//returns null if not any, need firstorderdefault or this thing breaks
    }

    private IEnumerable<MazeCell> NextCells(MazeCell currentCell)//finds what the next cell is and only if the cell isnt visited---returns a collection
    {
        int x = (int)currentCell.transform.position.x;//for some reason they need to be explictily casted 
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazewidth) //if in bounds
        {
            MazeCell cellToTheRight = _mazeGrid[x + 1, z];
            if (cellToTheRight.Isvisited == false)
            {
                yield return cellToTheRight;
            }
        }
        if (x - 1 >= 0)//from 0 because that is the min start point that the width depends on 
        {
            MazeCell cellToTheLeft = _mazeGrid[x-1,z];
            if (cellToTheLeft.Isvisited == false)
            {
                yield return cellToTheLeft;
            }
        }
        if (z + 1 < _mazedepth) //if in bounds
        {
            MazeCell cellToTheFront = _mazeGrid[x, z+1];
            if (cellToTheFront.Isvisited == false)
            {
                yield return cellToTheFront;
            }
        }
        if (z - 1>=0) //if in bounds from back
        {
            MazeCell cellToTheBack = _mazeGrid[x, z - 1];
            if (cellToTheBack.Isvisited == false)
            {
                yield return cellToTheBack;
            }
        }
    }
    private void Clear(MazeCell previous, MazeCell current)//clearing walls to carve out maze based on direction
    {
        if (previous == null)
        {
            return;
        }
        if (previous.transform.position.x < current.transform.position.x)
        {
            previous.Clearright();
            current.Clearleft();
            return;
        }
        if (previous.transform.position.x > current.transform.position.x)
        {
            previous.Clearleft();
            current.Clearright();
            return;
        }
        if (previous.transform.position.z < current.transform.position.z)
        {
            previous.Clearfront();
            current.Clearbackt();
            return;
        }
        if (previous.transform.position.z > current.transform.position.z)
        {
            previous.Clearbackt();
            current.Clearfront();
            return;
        }
    }
    void Update()
    {
        
    }
}
