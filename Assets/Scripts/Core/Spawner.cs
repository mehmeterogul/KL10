using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Shape[] shapes;
    [SerializeField] Transform[] spawnPoints;

    int currentShapeInStock = 0;

    // Spawn 3 shapes at spawn points
    public void SpawnShapes()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Shape shape = Instantiate(GetRandomShape(), spawnPoints[i].position, Quaternion.identity);
            shape.SetScaleDown();
            shape.SetShapeParent(spawnPoints[i]);
            shape.SetSpawnPosition(spawnPoints[i].position);
        }

        currentShapeInStock = 3;
    }

    Shape GetRandomShape()
    {
        return shapes[Random.Range(0, shapes.Length)];
    }

    public void DecreaseShapeCount()
    {
        if (currentShapeInStock < 1) return;

        currentShapeInStock--;
    }

    public int GetShapeCount()
    {
        return currentShapeInStock;
    }
}
