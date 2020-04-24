using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    // AI State
    public State currentState;
    public State nextState;
    public State remainState;
    public bool aiActive;

    // Animation state
    public AnimancerState animancerState;

    // Componenets
    public Transform eyes;

    // Current Target
    public Transform target;

    // Current Target
    public bool initialized;

    // Model of this enemy
    public EnemyData enemyData;

    [SerializeField] protected AnimancerComponent animancer;
    [SerializeField] protected RVOAgent rvoAgent;
    [SerializeField] protected CharacterController characterController;

    // Local variables
 

    // Use this for initialization
    protected virtual void Start () {

        rvoAgent = this.GetComponent<RVOAgent>();

        characterController = this.GetComponent<CharacterController>();

        animancer = this.GetComponent<AnimancerComponent>();
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (!aiActive)
            return;

        currentState.UpdateState(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            initialized = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (currentState)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, 1);
        }
    }

    public void PlayIdle()
    {
        if (!animancer.IsPlaying(enemyData.GetIdleClip()))
        {
            animancer.Play(enemyData.GetIdleClip());
        }
    }

    public void PlayWalking()
    {
        if (!animancer.IsPlaying(enemyData.GetWalkingClip()))
        {
            animancerState = animancer.Play(enemyData.GetWalkingClip(), 0.25f);
        }
    }

    public void PlayAttack()
    {
        animancerState = animancer.Play(enemyData.GetAttackClip(), 0.25f);
    }

    public void GoToTarget()
    {
        rvoAgent.SetTarget(target);
    }

    public void StopGoToTarget()
    {
        rvoAgent.Stop();
    }
}
