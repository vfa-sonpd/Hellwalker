using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Should this unity attack again ?
/// </summary>
/// 
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Attack Again Decision")]
public class AttackAgainDecision : Decision
{
    public override bool Decide(Enemy controller)
    {
        throw new System.NotImplementedException();
    }

}
