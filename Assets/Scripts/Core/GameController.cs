using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Spawner spawner;
    Board gameBoard;

    Camera mainCamera;
    [SerializeField] LayerMask shapeLayer;

    Shape selectedShape;

    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        gameBoard = FindObjectOfType<Board>();
        mainCamera = Camera.main;

        if (!gameBoard)
        {
            Debug.LogError("There is no game board defined");
        }
        else
        {
            gameBoard.CreateBoard();
        }

        if (!spawner)
        {
            Debug.LogError("There is no spawner object defined");
        }
        else
        {
            spawner.SpawnShapes();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawner || !gameBoard) return;

        SelectShape();
        MoveSelectedShape();
    }

    private void SelectShape()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, shapeLayer);
        {
            // Select shape with left mouse button
            if (Input.GetMouseButton(0))
            {
                if (!rayHit) return;

                if (!selectedShape)
                {
                    selectedShape = rayHit.collider.GetComponent<Shape>();
                    selectedShape.SetScaleUp();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!rayHit) return;
                if (!selectedShape) return;

                DropShapeOnBoard();

                // CHECK DROP POSITIN OF SHAPE IS VALID
                if (gameBoard.IsValidPosition(selectedShape))
                {
                    // if drop position is valid
                    selectedShape.SetShapeParent(gameBoard.transform);
                    selectedShape.SetTagPlaced();
                    selectedShape.DestroyCollider();

                    gameBoard.StoreShapeInGrid(selectedShape);
                    gameBoard.ClearBoard();

                    spawner.DecreaseShapeCount();

                    // Check shape count for spawn condition
                    if (IsShapeCountZero())
                    {
                        spawner.SpawnShapes();
                    }

                    // do the remaining shapes fit on the board?
                    if (!CheckShapesFitArea())
                    {
                        Debug.Log("Þekiller sýðmýyor.");
                        Debug.Log("game over");
                        isGameOver = true;
                    }
                    else
                    {
                        Debug.Log("Þekiller sýðýyor.");
                    }
                }
                else
                {
                    // if drop position is invalid
                    selectedShape.SetScaleDown();
                    selectedShape.transform.position = selectedShape.GetSpawnPosition();
                }

                selectedShape = null;
            }
        }
    }

    void MoveSelectedShape()
    {
        if (!selectedShape) return;

        selectedShape.transform.position = new Vector2(
                    mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                    mainCamera.ScreenToWorldPoint(Input.mousePosition).y
                    );
    }

    void DropShapeOnBoard()
    {
        if (!selectedShape) return;

        selectedShape.transform.position = Vector3Int.RoundToInt(new Vector2(
                    mainCamera.ScreenToWorldPoint(Input.mousePosition).x,
                    mainCamera.ScreenToWorldPoint(Input.mousePosition).y
                    ));
    }

    bool IsShapeCountZero()
    {
        return (spawner.GetShapeCount() == 0);
    }

    bool CheckShapesFitArea()
    {
        GameObject[] shapes = GameObject.FindGameObjectsWithTag("NotPlaced");

        if (shapes.Length > 0)
        {
            foreach (GameObject shape in shapes)
            {
                Debug.Log(shape.transform.name);

                if (!gameBoard.CheckShapeIsFitting(shape.GetComponent<Shape>()))
                {
                    return false;
                }
            }
        }

        return true;
    }
}