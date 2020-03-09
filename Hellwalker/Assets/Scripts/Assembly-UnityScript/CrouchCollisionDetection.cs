using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
[Serializable]
public class CrouchCollisionDetection : MonoBehaviour
{
	// Token: 0x060000E9 RID: 233 RVA: 0x0000C940 File Offset: 0x0000AB40
	public virtual void Start()
	{
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.origheight = ((CharacterController)this.GetComponent(typeof(CharacterController))).height;
		this.cont = (MyControllerScript)this.GetComponent(typeof(MyControllerScript));
	}

	// Token: 0x060000EA RID: 234 RVA: 0x0000C9AC File Offset: 0x0000ABAC
	public virtual void Update()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.transform.position, this.transform.up, out raycastHit, this.origheight - (float)1, this.environmentlayers))
		{
			this.cont.CrouchState = true;
		}
		else if (!this.cont.crouchtoggle && !this.inputmanager.GetKeyInput("crouch", 0))
		{
			this.cont.CrouchState = false;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000CA3C File Offset: 0x0000AC3C
	public virtual void Main()
	{
	}

	// Token: 0x040001BC RID: 444
	public LayerMask environmentlayers;

	// Token: 0x040001BD RID: 445
	[HideInInspector]
	public float origheight;

	// Token: 0x040001BE RID: 446
	[HideInInspector]
	public MyControllerScript cont;

	// Token: 0x040001BF RID: 447
	[HideInInspector]
	public MyInputManager inputmanager;
}
