using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint exploredFrom;
    

    const int gridSize = 1;

    public Vector2Int GetPos()
    {
        return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / gridSize) * gridSize,
                Mathf.RoundToInt(transform.position.z / gridSize) * gridSize
            );
    }
}
