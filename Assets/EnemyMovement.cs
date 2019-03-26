using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 targetPos;

    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetterPath();
        StartCoroutine(FollowPath(path));
    }
     
    // looping through the waypoints from the path in order to move our enemy objects
    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position =  waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
