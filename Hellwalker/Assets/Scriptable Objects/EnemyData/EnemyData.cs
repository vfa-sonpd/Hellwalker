using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Enemy Data", order = 1)]
public class EnemyData : ScriptableObject
{
    public float rangeOfAttack = 1;

    [SerializeField]
    private AnimationClip idleClip;
    [SerializeField]
    private AnimationClip walkingClip;
    [SerializeField]
    private AnimationClip attackClip;

    public AnimationClip GetWalkingClip()
    {
        return walkingClip;
    }

    public AnimationClip GetIdleClip()
    {
        return idleClip;
    }

    public AnimationClip GetAttackClip()
    {
        return attackClip;
    }
}
