using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Walking : ActionTask<Transform> {

        public BBParameter<Vector3[]> waypoints;
        private int currentIndex = 0;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            MoveToNextWaypoint();

        }

        //Called once per frame while the action is active.
        protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}

        void MoveToNextWaypoint()
        {
            if (waypoints.isNull || waypoints.value.Length == 0)
            {
                EndAction(false);
                return;
            }

            agent.position = Vector3.MoveTowards(agent.position, waypoints.value[currentIndex], Time.deltaTime * 3);

            if (Vector3.Distance(agent.position, waypoints.value[currentIndex]) < 0.1f)
            {
                currentIndex = (currentIndex + 1) % waypoints.value.Length;
            }
        }

    }
}