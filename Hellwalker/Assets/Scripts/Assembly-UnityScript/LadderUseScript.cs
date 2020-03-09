using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
[Serializable]
public class LadderUseScript : MonoBehaviour
{
	// Token: 0x0600027D RID: 637 RVA: 0x00017AA0 File Offset: 0x00015CA0
	public virtual void Start()
	{
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.s = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
		this.g = (MyControllerScript)this.GetComponent(typeof(MyControllerScript));
		this.cam = GameObject.Find("MainCamera");
		this.t = this.soundtime - 0.01f;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00017B34 File Offset: 0x00015D34
	public virtual void Update()
	{
		RaycastHit raycastHit = default(RaycastHit);
		if (Physics.Raycast(this.cam.transform.position, this.transform.forward, out raycastHit, (float)1, this.ladderlayers))
		{
			if (!this.ignoreladdertoggle)
			{
				if (raycastHit.transform.gameObject.layer == 18)
				{
					this.s.havejetpack = true;
					this.g.CrouchState = false;
				}
				if (this.climbanything && this.inputmanager.GetKeyInput("walk", 0))
				{
					this.s.havejetpack = true;
				}
			}
			if (raycastHit.transform.gameObject.tag == "IgnoreSpoder")
			{
				this.s.havejetpack = false;
			}
		}
		else if (this.inputmanager.GetKeyInput("forward", 0) || this.inputmanager.GetKeyInput("backward", 0) || this.inputmanager.GetKeyInput("left", 0) || this.inputmanager.GetKeyInput("right", 0) || this.inputmanager.GetKeyInput("jump", 0) || this.inputmanager.GetKeyInput("crouch", 0))
		{
			this.s.havejetpack = false;
			this.t = this.soundtime - 0.01f;
			this.ignoreladdertoggle = false;
		}
		if (this.ignoreladdertoggle)
		{
			this.s.havejetpack = false;
		}
		if (this.s.havejetpack && this.g.gravityforce != (float)0)
		{
			this.t += Time.deltaTime;
			if (this.t > this.soundtime)
			{
				((AudioSource)this.mysound.GetComponent(typeof(AudioSource))).pitch = UnityEngine.Random.Range(0.9f, 1.1f);
				((AudioSource)this.mysound.GetComponent(typeof(AudioSource))).Play();
				this.t = (float)0;
			}
		}
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00017D70 File Offset: 0x00015F70
	public virtual void Main()
	{
	}

	// Token: 0x040002DB RID: 731
	public GameObject mysound;

	// Token: 0x040002DC RID: 732
	public float soundtime;

	// Token: 0x040002DD RID: 733
	public LayerMask ladderlayers;

	// Token: 0x040002DE RID: 734
	public bool climbanything;

	// Token: 0x040002DF RID: 735
	public bool ignoreladdertoggle;

	// Token: 0x040002E0 RID: 736
	[HideInInspector]
	public SelectionScript s;

	// Token: 0x040002E1 RID: 737
	[HideInInspector]
	public float t;

	// Token: 0x040002E2 RID: 738
	[HideInInspector]
	public MyControllerScript g;

	// Token: 0x040002E3 RID: 739
	[HideInInspector]
	public GameObject cam;

	// Token: 0x040002E4 RID: 740
	[HideInInspector]
	public MyInputManager inputmanager;
}
