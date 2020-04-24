using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetDistanceDecision")]
public class TargetDistanceDecision : Decision
{
    public override bool Decide(Enemy controller)
    {
        float distance = Vector3.Distance(controller.transform.position, controller.target.position);
        if (distance < controller.enemyData.rangeOfAttack)
        {
            return true;
        }
        return false;
    }

}
