using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions {

	public class Wait : ConditionTask {

        public float waitTime = 3f;   // The amount of time to wait
        private float timer;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
            timer = 0f; // Reset the timer

        }

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            if (timer < waitTime)
            {
                timer += Time.deltaTime;
                return false;  // Still waiting, condition is not met
            }

            return true;  // Condition is met after the wait time
        }
	}
}