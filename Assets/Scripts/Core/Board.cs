using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int boardWidth = 10;
    [SerializeField] int boardHeight = 10;
    [SerializeField] Transform emptyCellSprite;

    Transform[,] grid;

    private void Awake()
    {
        grid = new Transform[boardWidth, boardHeight];
    }

    // Create game board with empty cells based width and height values
    public void CreateBoard()
    {
        if(emptyCellSprite)
        {
            for (int x = 0; x < boardHeight; x++)
            {
                for (int y = 0; y < boardWidth; y++)
                {
                    Transform cell;
                    cell = Instantiate(emptyCellSprite, new Vector2(x, y), Quaternion.identity);
                    cell.name = "Cell (" + x + ", " + y + ")";
                    cell.parent = transform;
                }
            }
        }
        else
        {
            Debug.LogError("Please assign the empty cell sprite");
        }
    }

    public bool IsValidPosition(Shape shape)
    {
        foreach(Transform child in shape.transform)
        {
            Vector2 pos = Vector2Int.RoundToInt(child.position);

            if(!IsWithinBoard((int) pos.x, (int) pos.y))
            {
                return false;
            }

            if (IsOccupied((int) pos.x, (int) pos.y))
            {
                return false;
            }
        }

        return true;
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < boardWidth && y >= 0 && y < boardHeight);
    }

    bool IsOccupied(int x, int y)
    {
        return (grid[x, y] != null);
    }

    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
            return;

        foreach(Transform child in shape.transform)
        {
            Vector2 pos = Vector2Int.RoundToInt(child.position);
            grid[(int)pos.x, (int)pos.y] = child;
        }
    }
}
