using Animancer;
using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    [Header("Current Target")]
    // Current Target
    public Transform target;

    [Header("AI State")]
    // AI State
    public State currentState;
    public State lastState;
    public State nextState;
    public State remainState;
    public State flinchState;
    public bool aiActive;

    [Header("Components")]
    // Componenets
    public Transform eyes;
    // Model of this enemy
    public EnemyData enemyData;

    [Header("Particles")]
    public ParticleSystem bloodMistParticle;

    // Animation state
    public AnimancerState animancerState_layer1;
    public AnimancerState animancerState_layer2;

    //Animancer component
    [HideInInspector]
    public AnimancerComponent animancer;
    protected RVOAgent rvoAgent;
    protected CharacterController characterController;

    // Local variables

    [Header("Other Variables")]
    public bool isFlinching;

    // Initialized
    [HideInInspector]
    public bool initialized;

    // Use this for initialization
    protected virtual void Start () {

        rvoAgent = this.GetComponent<RVOAgent>();

        characterController = this.GetComponent<CharacterController>();

        animancer = this.GetComponent<AnimancerComponent>();

        lastState = currentState;
    }

    // Update is called once per frame
    protected virtual void Update () {
        if (!aiActive)
            return;

        currentState.UpdateState(this);

        if(Input.GetKeyDown(KeyCode.A))
        {
            OnHit(100);
        }
    }

    public void TransitionToState(State nextState, bool rememberLastState = true)
    {
        if (nextState != remainState)
        {
            if(rememberLastState)
            {
                lastState = currentState;
            }

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
            animancerState_layer1 = animancer.Play(enemyData.GetIdleClip());
        }
    }

    public void PlayWalking()
    {
        if (!animancer.IsPlaying(enemyData.GetWalkingClip()))
        {
            animancerState_layer1 = animancer.Play(enemyData.GetWalkingClip(), 0.25f);
        }
    }

    public void PlayAttack()
    {
        animancerState_layer1 = animancer.Play(enemyData.GetAttackClip(), 0.25f);
    }

    public void GoToTarget()
    {
        rvoAgent.SetTarget(target,enemyData.speed);
    }

    public void StopGoToTarget()
    {
        rvoAgent.Stop();
    }

    /// <summary>
    /// Continuos rotate to target
    /// </summary>
    public void LookAtAtarget()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position);

        transform.rotation =
        Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * 10);
    }

    public void ReturnToLastState()
    {
        if (lastState)
        {
            currentState = lastState;
        }
    }

    /// <summary>
    /// Hit by anything (bullet,etc..)
    /// </summary>
    /// <param name="damage"></param>
    public void OnHit(int damage)
    {
        Flinch();
    }

    void Flinch()
    {
        if(isFlinching)
        {
            return;
        }

        if(currentState.Interupptable)
        {
            animancerState_layer1.NormalizedTime = 0;
            animancerState_layer1.IsPlaying = false;
        }

        animancer.Layers[1].IsAdditive = true;
        animancerState_layer2 = animancer.Layers[1].Play(enemyData.GetFlinchClip());
        animancerState_layer2.NormalizedTime = 0;
        animancerState_layer2.Speed = 1.75f;

        animancerState_layer2.Events.Add(0.6f, OnEndFlinching);

        //Spawn Effect
        MessageDispatcher.SendMessageData(GameEvent.SPANW_BLOOD_MIST, transform.position + Vector3.up);

        isFlinching = true;
    }

    void OnEndFlinching()
    {
        animancerState_layer1.IsPlaying = true;
        isFlinching = false;
    }
}
