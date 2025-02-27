﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Transition[] transitions;

    public Color sceneGizmoColor = Color.grey;

    public bool Interupptable;

    public void UpdateState(Enemy controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    public void DoActions(Enemy controller)
    {
        for(int i = 0; i < actions.Length; i ++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransitions(Enemy controller)
    {
        for(int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
    }

    public void Interrupt(Enemy controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].OnInterrupt(controller);
        }
    }
}
