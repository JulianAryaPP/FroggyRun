 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    protected int horizontalSize;
    public virtual void Generate(int size)
    {
        horizontalSize = size;
        if (size == 0) 
            return;

        if ((float) size % 2 == 0)
            size -= 1;

        int limit = Mathf.FloorToInt((float)size / 2);

        for (int i = -limit; i <= limit; i++)           
            {
                SpawnTile(i);
            }

        var leftBoundaryTile = SpawnTile(-limit - 1);
        var rightBoundaryTile = SpawnTile(limit + 1);
        DarkenObject(leftBoundaryTile);
        DarkenObject(rightBoundaryTile);
    }
    private GameObject SpawnTile(int xPos)
    {
        var go = Instantiate(tilePrefab, transform);
        go.transform.localPosition = new Vector3(xPos, 0, 0);
        return go;
    }
    private void DarkenObject(GameObject go)
    {
        var renderer = go.GetComponentsInChildren<MeshRenderer>(includeInactive: true);

        foreach (var rend in renderer)
        {
            rend.material.color = rend.material.color * Color.grey; 
        }
    }
} 


