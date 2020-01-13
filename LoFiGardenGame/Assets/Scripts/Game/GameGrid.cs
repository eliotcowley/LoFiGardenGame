using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grid))]
public class GameGrid : MonoBehaviour
{
    [SerializeField]
    private int rows = 4;

    [SerializeField]
    private int columns = 4;

    private Grid grid;

    private void Start()
    {
        grid = GetComponent<Grid>();
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3Int cell = new Vector3Int(i, 0, j);
                Vector3 cellCenter = grid.GetCellCenterWorld(cell);
                GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = cellCenter;
            }
        }
    }

    public Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return grid.WorldToCell(worldPosition);
    }

    public Vector3 CellToWorld(Vector3Int cellPosition)
    {
        return grid.CellToWorld(cellPosition);
    }
}
