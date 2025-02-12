using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;


namespace NodeCanvas.Tasks.Actions
{

    public class Walking : ActionTask<UnityEngine.AI.NavMeshAgent>
    {
        // The speed at which the agent moves
        public BBParameter<float> moveSpeed = 3.5f;

        // The stopping distance for the agent
        public BBParameter<float> stoppingDistance = 1.0f;

        // Reference to the WaypointManager
        public BBParameter<WaypointManager> WaypointManager;

        private Transform currentWaypoint; // Store the current waypoint



        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {

            if (WaypointManager.value == null)
            {
                Debug.LogError("Next waypoint is null!");

                EndAction(false);
                return;
            }

            if (agent == null)
            {
                Debug.LogError("NavMeshAgent is not assigned!");
                EndAction(false);
                return;
            }

            currentWaypoint = WaypointManager.value.GetNextWaypoint(); // Get the *first* waypoint

            if (currentWaypoint != null)
            {
                agent.SetDestination(currentWaypoint.position);
                agent.speed = moveSpeed.value;
                agent.stoppingDistance = stoppingDistance.value;
            }
            else
            {
                EndAction(false); // No waypoints, fail
            }


        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            if (currentWaypoint == null)  //Handle if waypoint is deleted during the walk
            {
                EndAction(false);
                return;
            }

            // Check if the agent has reached the destination
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // Reached the *current* waypoint. Now, check if it's the LAST one.
                if (WaypointManager.value.IsLastWaypoint(currentWaypoint))
                {
                    EndAction(true); // Success! Reached the last waypoint
                }
                else
                {
                    // Not the last waypoint, get the next one
                    currentWaypoint = WaypointManager.value.GetNextWaypoint();
                    if (currentWaypoint != null)
                    {
                        agent.SetDestination(currentWaypoint.position);
                    }
                    else
                    {
                        EndAction(false); // No more waypoints, but not the last one, fail.
                    }
                }
            }

        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            agent.speed = 3.5f;

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }

    }
}