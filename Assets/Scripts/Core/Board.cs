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
        if (shape == null) return false;

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

    public void ClearBoard()
    {
        List<int> rowIndex = new List<int>();
        List<int> columnIndex = new List<int>();

        for (int y = 0; y < boardHeight; y++)
        {
            if(IsRowComplete(y))
            {
                ClearRow(y);
                rowIndex.Add(y); 
            }
        }

        for (int x = 0; x < boardWidth; x++)
        {
            if (IsColumnComplete(x))
            {
                ClearColumn(x);
                columnIndex.Add(x);
            }
        }

        if(rowIndex.Count > 0)
        {
            ClearRowStack(rowIndex);
        }
        
        if(columnIndex.Count > 0)
        {
            ClearColumnStack(columnIndex);
        }
    }

    bool IsRowComplete(int y)
    {
        for (int x = 0; x < boardWidth; x++)
        {
            if(grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    bool IsColumnComplete(int x)
    {
        for (int y = 0; y < boardHeight; y++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }

        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < boardWidth; x++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
        }
    }

    void ClearColumn(int x)
    {
        for (int y = 0; y < boardHeight; y++)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y].gameObject);
            }
        }
    }

    void ClearRowStack(List<int> rows)
    {
        foreach(int rowIndex in rows)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                grid[x, rowIndex] = null;
            }
        }
    }

    void ClearColumnStack(List<int> columns)
    {
        foreach (int columnIndex in columns)
        {
            for (int y = 0; y < boardWidth; y++)
            {
                grid[columnIndex, y] = null;
            }
        }
    }
}
