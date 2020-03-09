using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
[Serializable]
public class ScarecrowPostSCript : MonoBehaviour
{
	// Token: 0x060003CA RID: 970 RVA: 0x00024570 File Offset: 0x00022770
	public virtual void Awake()
	{
		if (!this.spawnscarecrow)
		{
			this.transform.gameObject.tag = "Untagged";
		}
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000245A0 File Offset: 0x000227A0
	public virtual void Start()
	{
	}

	// Token: 0x060003CC RID: 972 RVA: 0x000245A4 File Offset: 0x000227A4
	public virtual void Update()
	{
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000245A8 File Offset: 0x000227A8
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			this.die(hit);
		}
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000245D4 File Offset: 0x000227D4
	public virtual void die(Collider hit)
	{
		if (this.spawnscarecrow)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.myenemy, this.transform.position, Quaternion.identity);
			gameObject.transform.LookAt(hit.transform);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.createondie, this.transform.position - this.transform.right * (float)1, Quaternion.Euler((float)-90, (float)0, (float)0));
			((Collider)this.GetComponent(typeof(Collider))).enabled = false;
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00024680 File Offset: 0x00022880
	public virtual void Main()
	{
	}

	// Token: 0x040004AB RID: 1195
	public bool spawnscarecrow;

	// Token: 0x040004AC RID: 1196
	public GameObject myenemy;

	// Token: 0x040004AD RID: 1197
	public GameObject createondie;
}
