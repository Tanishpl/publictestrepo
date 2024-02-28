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

        yield return generatemaze(null, _mazeGrid[0, 0]);//--actually read this one--there is not previous and the current is the starting point-should turn this into a variable later so i can make it movebale
    }



    private IEnumerator generatemaze(MazeCell previous, MazeCell current)
    {
        current.Visit();//set starting pointed as visited so you dont go back
        clear(previous, current);//to carve out inital walls and get going

        yield return new WaitForSeconds(0.05f);//this is not nessary but means  i can actually see the maze generate

        MazeCell chosenCell;
        do
        {
            chosenCell = FindnextCell(current);
            if (chosenCell != null) //if there is an unvisited cell
            {
                yield return generatemaze(current, chosenCell); //recursion,calls itself until no unvisited cells left(hopefully)
            }
        } while (chosenCell != null);//stops after no cells left in its current radius
    }

    private MazeCell FindnextCell(MazeCell currentCell)//returns only 1 cell randomly
    {

        //use nextcells to find the neighboroughing cells to the current cell and put them in a collection
        IEnumerable<MazeCell> unvisitedCells = nextCells(currentCell);//idk why ienumerable works here and not list

        //returns a random cell in that collection of unvisited cells 
        return unvisitedCells.OrderBy(placeholder => Random.Range(1, 10)).FirstOrDefault();//returns null if not any, need firstorderdefault or this thing breaks
    }

    private IEnumerable<MazeCell> nextCells(MazeCell currentCell)//finds what the next cell is and only if the cell isnt visited---returns a collection
    {
        int x = (int)currentCell.transform.position.x;//for some reason they need to be explictily casted 
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < _mazewidth) //if in bounds
        {
            MazeCell cell_to_the_right = _mazeGrid[x + 1, z];
            if (cell_to_the_right.Isvisited == false)
            {
                yield return cell_to_the_right;
            }
        }
        if (x - 1 >= 0)//from 0 because that is the min start point that the width depends on 
        {
            MazeCell cell_to_the_left = _mazeGrid[x-1,z];
            if (cell_to_the_left.Isvisited == false)
            {
                yield return cell_to_the_left;
            }
        }
        if (z + 1 < _mazedepth) //if in bounds
        {
            MazeCell cell_to_the_front = _mazeGrid[x, z+1];
            if (cell_to_the_front.Isvisited == false)
            {
                yield return cell_to_the_front;
            }
        }
        if (z - 1>=0) //if in bounds from back
        {
            MazeCell cell_to_the_back = _mazeGrid[x, z - 1];
            if (cell_to_the_back.Isvisited == false)
            {
                yield return cell_to_the_back;
            }
        }
    }
    private void clear(MazeCell previous, MazeCell current)//clearing walls to carve out maze based on direction
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
