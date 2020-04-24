using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {
    public override bool Decide(Enemy controller)
    {
        if(CheckLoS(controller) && CheckFoV(controller))
        {
            return true;
        }
        return false;
    }

    public virtual bool CheckLoS(Enemy controller)
    {
        RaycastHit raycastHit = default(RaycastHit);
        Transform myTarget = controller.target;
        bool result = false;
        Vector3 vector = controller.eyes.position + new Vector3((float)0, .5f, (float)0);
        bool flag = Physics.Raycast(vector, (myTarget.transform.position - vector).normalized, out raycastHit, Vector3.Distance(vector, myTarget.transform.position));
        bool flag2 = false;
        if (flag)
        {
            if (raycastHit.transform.gameObject == controller.target.gameObject)
            {
                flag2 = true;
            }
        }
        else
        {
            flag2 = true;
        }
        if (flag2)
        {
            result = true;
        }

        return result;
    }

    public virtual bool CheckFoV(Enemy controller)
    {

        Transform myTarget = controller.target;
        bool result = false;
        Vector3 from = myTarget.transform.position - controller.transform.position;
        float num = Vector3.Angle(from, controller.transform.forward);

        if (num < (float)100)
        {
            result = true;
        }

        return result;
    }

}
