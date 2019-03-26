using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    Dictionary<Vector2, Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();

    //start and end points
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    //searchPoint
    Waypoint searchStart;

    List<Waypoint> path = new List<Waypoint>();

    bool isRuning = true;
    
    public List<Waypoint> GetterPath()
    {
        //adding grids to the dictionary
        LoadBlocks();
        //add/remove to/from the queue and exploring the blocks around our current point
        BreathFirstSearchAlg();
        //creating the shortest path to our end point
        CreatePath();
        return path;
    }

    Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.right,
            Vector2Int.down,
            Vector2Int.left
        };

    private void LoadBlocks()
    {

        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetPos());
            if (!isOverlapping)
            {
                grid.Add(waypoint.GetPos(), waypoint);
            }
        }
    }

    private void BreathFirstSearchAlg()
    {
        queue.Enqueue(startWaypoint);

        while (queue.Count > 0 && isRuning)
        {
            searchStart = queue.Dequeue();
            searchStart.isExplored = true;

            if (searchStart == endWaypoint)
            {
                isRuning = false;
                break;
            }

            ExploreNeighbours();
        }
    }

    private void ExploreNeighbours()
    {
        if (!isRuning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = searchStart.GetPos() + direction;
            if (grid.ContainsKey(explorationCoordinates))
            {
                Waypoint neighbourCoordinates = grid[explorationCoordinates];

                if (!neighbourCoordinates.isExplored || queue.Contains(neighbourCoordinates))
                {
                    queue.Enqueue(neighbourCoordinates);
                    neighbourCoordinates.exploredFrom = searchStart;
                }
            }
        }
    }

    private void CreatePath()
    {
        path.Add(endWaypoint);

        Waypoint previous = endWaypoint.exploredFrom;
        while (previous != startWaypoint)
        {
            path.Add(previous);
            previous = previous.exploredFrom;
        }

        path.Add(startWaypoint);
        path.Reverse();
    }









}
