using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    Vector3 spawnPosition = new Vector3(0, 0, 0);

    public void SetSpawnPosition(Vector3 value)
    {
        spawnPosition = value;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }

    public void SetShapeParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void SetScaleDown()
    {
        transform.localScale = new Vector3(0.55f, 0.55f, 1f);
    }

    public void SetScaleUp()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 1f, 1f), 1f);
    }

    public void DestroyCollider()
    {
        Destroy(GetComponent<BoxCollider2D>());
    }

    public void SetTagPlaced()
    {
        gameObject.tag = "Placed";
    }
}
