using UnityEngine;

public class WaitBehaviour : StateMachineBehaviour {
    public float waitTime;
    private float startTime;
    private float timer;

    override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        startTime = Time.time;
        timer = Time.time;
    }

    override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer += Time.deltaTime;
        if (timer >= startTime + waitTime) {
            Debug.Log ("Change to Fly Around");
            animator.SetTrigger ("flyAround");
        }
    }
}