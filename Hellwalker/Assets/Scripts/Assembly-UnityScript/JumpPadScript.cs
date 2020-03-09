using System;
using UnityEngine;

// Token: 0x02000060 RID: 96
[Serializable]
public class JumpPadScript : MonoBehaviour
{
	// Token: 0x06000278 RID: 632 RVA: 0x000178D0 File Offset: 0x00015AD0
	public virtual void Start()
	{
	}

	// Token: 0x06000279 RID: 633 RVA: 0x000178D4 File Offset: 0x00015AD4
	public virtual void Update()
	{
	}

	// Token: 0x0600027A RID: 634 RVA: 0x000178D8 File Offset: 0x00015AD8
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			MyControllerScript myControllerScript = (MyControllerScript)hit.transform.gameObject.GetComponent(typeof(MyControllerScript));
			myControllerScript.gravityforce = this.myjumpamount;
			myControllerScript.currentHeight = myControllerScript.originalCrouchHeight;
			myControllerScript.CrouchState = false;
			float y = 0.13f;
			Vector3 localPosition = GameObject.Find("WeaponCam").transform.localPosition;
			float num = localPosition.y = y;
			Vector3 vector = GameObject.Find("WeaponCam").transform.localPosition = localPosition;
			((MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook))).buck = (float)15;
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		}
		Rigidbody rigidbody = (Rigidbody)hit.transform.gameObject.GetComponent(typeof(Rigidbody));
		if (rigidbody)
		{
			float y2 = this.myjumpamount * (float)25;
			Vector3 velocity = rigidbody.velocity;
			float num2 = velocity.y = y2;
			Vector3 vector2 = rigidbody.velocity = velocity;
			((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		}
		if (hit.transform.gameObject.layer == 14)
		{
			BasicAIScript basicAIScript = (BasicAIScript)hit.transform.gameObject.GetComponent(typeof(BasicAIScript));
			if (basicAIScript)
			{
				basicAIScript.jumpvelocity = this.myjumpamount * (float)30;
			}
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00017A94 File Offset: 0x00015C94
	public virtual void Main()
	{
	}

	// Token: 0x040002DA RID: 730
	public float myjumpamount;
}
