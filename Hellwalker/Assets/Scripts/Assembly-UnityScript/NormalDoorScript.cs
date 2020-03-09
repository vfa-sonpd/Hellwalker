using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000084 RID: 132
[Serializable]
public class NormalDoorScript : MonoBehaviour
{
	// Token: 0x06000344 RID: 836 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
	public virtual void Start()
	{
		this.initialoffset = this.transform.position - this.mypivot.transform.position;
		this.currentrotation = this.mypivot.transform.localEulerAngles;
		this.col = (BoxCollider)this.GetComponent(typeof(BoxCollider));
		this.nav = (NavMeshObstacle)this.GetComponent(typeof(NavMeshObstacle));
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001F424 File Offset: 0x0001D624
	public virtual void Update()
	{
		if (this.targetrotation > this.currentrotation.y)
		{
			this.currentrotation.y = this.currentrotation.y + Time.deltaTime * this.openspeed;
			this.col.enabled = false;
			if (this.currentrotation.y > this.targetrotation)
			{
				this.col.enabled = true;
				this.currentrotation.y = this.targetrotation;
			}
		}
		if (this.targetrotation < this.currentrotation.y)
		{
			this.currentrotation.y = this.currentrotation.y - Time.deltaTime * this.openspeed;
			this.col.enabled = false;
			if (this.currentrotation.y < this.targetrotation)
			{
				this.col.enabled = true;
				this.currentrotation.y = this.targetrotation;
			}
		}
		this.mypivot.transform.localEulerAngles = this.currentrotation;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001F538 File Offset: 0x0001D738
	public virtual void OnCollisionEnter(Collision hit)
	{
		GameObject gameObject = hit.transform.gameObject;
		if (gameObject.layer == 14)
		{
			if (this.targetrotation != this.openrotation)
			{
				((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
				((NameDisplayScript)this.GetComponent(typeof(NameDisplayScript))).myname = "Close";
			}
			this.targetrotation = this.openrotation;
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0001F5B4 File Offset: 0x0001D7B4
	public virtual void Main()
	{
	}

	// Token: 0x04000405 RID: 1029
	public GameObject mypivot;

	// Token: 0x04000406 RID: 1030
	[HideInInspector]
	public float targetrotation;

	// Token: 0x04000407 RID: 1031
	public float openspeed;

	// Token: 0x04000408 RID: 1032
	public float openrotation;

	// Token: 0x04000409 RID: 1033
	[HideInInspector]
	public Vector3 currentrotation;

	// Token: 0x0400040A RID: 1034
	[HideInInspector]
	public BoxCollider col;

	// Token: 0x0400040B RID: 1035
	[HideInInspector]
	public NavMeshObstacle nav;

	// Token: 0x0400040C RID: 1036
	[HideInInspector]
	public Vector3 initialoffset;
}
