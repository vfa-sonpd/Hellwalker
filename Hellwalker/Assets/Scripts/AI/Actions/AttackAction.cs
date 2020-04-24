using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack Action")]
public class AttackAction : Action
{
    public override void Act(Enemy controller)
    {
        if (!controller.initialized)
        {
            Debug.Log("CALL AttackAction");
            controller.initialized = true;
            controller.StopGoToTarget();

            controller.PlayAttack();
        }

        // check if fight animation is over
        if (controller.animancerState.NormalizedTime >= 1)
        {
            controller.PlayAttack();
        }
    }


    
}
