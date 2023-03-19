using UnityEngine;

public class sunSphereScript : StateMachineBehaviour
{
    //onstateexit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Destroy(animator.gameObject);

    }
}
