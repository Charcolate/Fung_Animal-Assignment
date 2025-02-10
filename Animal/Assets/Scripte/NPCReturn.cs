using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class NPCReturn : ActionTask {

        // GameObjects to move
        public GameObject Fox;
        public GameObject Chicken;
        public GameObject other;

        // Target position
        public BBParameter<Vector3> returnLocation;

        public float moveSpeed = 5f; // Speed at which to move 
        private bool FoxReturn = false, ChickenReturn = false, otherReturn = false; // Movement flags for each


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            if (FoxReturn)
            {
                Fox.transform.position = Vector3.MoveTowards(
                    Fox.transform.position,
                    returnLocation.value, 
                    moveSpeed * Time.deltaTime
                );
                if (Fox.transform.position == returnLocation.value)
                {
                    FoxReturn = false;
                }
            }

            if(ChickenReturn)
            {
                Chicken.transform.position = Vector3.MoveTowards(
                    Chicken.transform.position,
                    returnLocation.value,
                    moveSpeed * Time.deltaTime
                );
                if (Chicken.transform.position == returnLocation.value)
                {
                    ChickenReturn = false;
                }
            }

            if(otherReturn)
            {
                other.transform.position = Vector3.MoveTowards(
                    other.transform.position,
                    returnLocation.value,
                    moveSpeed * Time.deltaTime
                );
                if (other.transform.position == returnLocation.value)
                {
                    otherReturn = false;
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