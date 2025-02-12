using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    // List of waypoints (assign in the Inspector)
    public List<Transform> Waypoints = new List<Transform>();

    // Index of the current waypoint
    private int currentWaypointIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
     
    }
     

    // Get the next waypoint
    public Transform GetNextWaypoint()

    {
        // Check if the waypoints list is empty
        if (Waypoints == null || Waypoints.Count == 0)
        {
            Debug.LogError("Waypoints list is null or empty!");
            return null;
        }

        // Get the current waypoint
        Transform nextWaypoint = Waypoints[currentWaypointIndex];

        // Check if the next waypoint is null
        if (nextWaypoint == null)
        {
            Debug.LogError("Next waypoint is null!");
            return null;
        }

        // Move to the next waypoint (loop back to the start if needed)
        currentWaypointIndex = (currentWaypointIndex + 1) % Waypoints.Count;

        return nextWaypoint;
    }

    public bool IsLastWaypoint(Transform waypoint)
    {
        if (Waypoints == null || Waypoints.Count == 0)
        {
            return false;
        }

        //Find the index of the waypoint passed in
        int waypointIndex = Waypoints.IndexOf(waypoint);

        if (waypointIndex == -1) //If the waypoint is not in the list
        {
            return false;
        }

        return waypointIndex == Waypoints.Count - 1; // It's the last if the index is the last in the List
    }

}
