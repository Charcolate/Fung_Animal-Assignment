using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class NPCMove : ActionTask {

        public GameObject targetNPC; // The NPC to move
        public BBParameter<Vector3> targetPosition; // Target position
        public float moveSpeed = 5f; // Movement speed
        private bool isMoving = false;

        public BBParameter<bool> isRotate; // Whether to rotate toward the target
        public float rotationSpeed = 120f; // Rotation speed in degrees per second
        public float stoppingAngle = 5f; // Angle threshold to stop rotating
        private Quaternion targetRotation;

        public NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            // Get or add the NavMeshAgent component to the NPC
            navMeshAgent = targetNPC.GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                navMeshAgent = targetNPC.AddComponent<NavMeshAgent>();
            }

            // Configure the NavMeshAgent
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.angularSpeed = rotationSpeed;
            navMeshAgent.stoppingDistance = 0.1f; // Adjust as needed

            return null;
        }

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            // Set the destination for the NavMeshAgent
            navMeshAgent.SetDestination(targetPosition.value);

            // Start by rotating toward the target direction
            isRotate.value = true;
            Vector3 direction = (targetPosition.value - targetNPC.transform.position).normalized;
            if (direction != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0); // Make sure to face the correct direction (right)
            }

            isMoving = true; // Start moving
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {

            if (isRotate.value)
            {
                RotateTowardTarget();

                // Check if the rotation is complete
                float angleDifference = Quaternion.Angle(targetNPC.transform.rotation, targetRotation);
                if (angleDifference <= stoppingAngle)
                {
                    Debug.Log("Rotation complete!");
                    isRotate.value = false;
                    isMoving = true; // Start moving after rotation is complete
                }
            }


            if (isMoving)
            {
                // Check if the NPC has reached the target position
                if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    Debug.Log("Target reached!");
                    isMoving = false;
                    EndAction(true); // Mark the action as successfully completed
                }
            }
        }

        //Face the direction of where the target location is
        private void RotateTowardTarget()
        {
            targetNPC.transform.rotation = Quaternion.RotateTowards(
                targetNPC.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }


        //Called when the task is disabled.
        protected override void OnStop() {
            isRotate.value = false;
            isMoving = false; // Reset the states

            // Stop the NavMeshAgent
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }

        }
	}
}