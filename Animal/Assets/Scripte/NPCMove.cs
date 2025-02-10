using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class NPCMove : ActionTask {

        public GameObject targetNPC; // The NPC to move
        public BBParameter<Vector3> targetPosition; // Target position
        public float moveSpeed = 5f; // Movement speed
        private bool isMoving = false;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            // Check if the Q key is pressed to start the movement
            if (Input.GetKeyDown(KeyCode.Q))
            {
                isMoving = true;
            }

        }

		//Called once per frame while the action is active.
		protected override void OnUpdate()
		{

			if (isMoving)
			{
                targetNPC.transform.position = Vector3.MoveTowards(
                    targetNPC.transform.position,
					targetPosition.value,
					moveSpeed * Time.deltaTime
				);

				// Stop NPC when the target position is reached
				if (targetNPC.transform.position == targetPosition.value)
				{
					isMoving = false;
					EndAction();
				}

			}
		}
		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}