using UnityEngine;
using UnityEngine.AI;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace NodeCanvas.Tasks.Actions
{
    public class Walk : ActionTask
    {
        // Defines how far the agent should search for a random position
        public float randomPositionDistance = 5f;
        public float arrivalDistance = 1f;// Defines the distance threshold at which the agent is considered to have arrive

        private NavMeshAgent navmeshAgent; // Reference to the agent controlling movement
        private Vector3 destination; // Stores the chosen destination for movement


        protected override string OnInit()
        {
            navmeshAgent = agent.GetComponent<NavMeshAgent>();
            return null;
        }

        protected override void OnExecute()
        {
            // Ensure the NavMeshAgent component is present
            if (navmeshAgent == null)
            {
                Debug.LogError("NavMeshAgent is missing!");
                EndAction(false);
                return;
            }

            // Ensure agent is active and can move
            navmeshAgent.isStopped = false;
            navmeshAgent.speed = 3.5f; // Adjust as needed

            // Find a random valid NavMesh position
            for (int i = 0; i < 10; i++) // Try 10 times to find a valid spot
            {
                // Generate a random position within the specified distance
                Vector3 randomPosition = agent.transform.position + Random.insideUnitSphere * randomPositionDistance;
                randomPosition.y = agent.transform.position.y; // Keep y the same to avoid weird height issues

                NavMeshHit navMeshHit;
                // Check if the generated position is on the NavMesh
                if (NavMesh.SamplePosition(randomPosition, out navMeshHit, randomPositionDistance * 2, NavMesh.AllAreas))
                {
                    destination = navMeshHit.position; // Store the valid destination
                    navmeshAgent.SetDestination(destination); // Move the agent
                    return; // Exit the loop early
                }
            }

            Debug.LogWarning("Failed to find a valid random position.");
            EndAction(false);
        }

        protected override void OnUpdate()
        {
            // Check if the agent has reached the destination
            if (!navmeshAgent.pathPending && navmeshAgent.remainingDistance <= arrivalDistance)
            {
                EndAction(true);
            }
        }
    }
}