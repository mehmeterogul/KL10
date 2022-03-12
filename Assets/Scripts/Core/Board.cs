using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int boardWidth = 10;
    [SerializeField] int boardHeight = 10;
    [SerializeField] Transform emptyCellSprite;

    // Create game board with empty cells based width and height values
    public void CreateBoard()
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
}
