using System;
using UnityEngine;

// Token: 0x02000073 RID: 115
[Serializable]
public class LowSpecModeScript : MonoBehaviour
{
	// Token: 0x060002CF RID: 719 RVA: 0x00019FEC File Offset: 0x000181EC
	public virtual void Start()
	{
		if (GameObject.Find("WeaponAnimator"))
		{
			AttackScript attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
		}
		DisableForLowSpecModeScript[] array = ((DisableForLowSpecModeScript[])UnityEngine.Object.FindObjectsOfType(typeof(DisableForLowSpecModeScript))) as DisableForLowSpecModeScript[];
		this.obs = new GameObject[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			this.obs[i] = array[i].gameObject;
		}
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x0001A088 File Offset: 0x00018288
	public virtual void Update()
	{
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x0001A08C File Offset: 0x0001828C
	public virtual void updatemode()
	{
		if (this.obs.Length > 0)
		{
			for (int i = 0; i < this.obs.Length; i++)
			{
				if (this.obs[i])
				{
					this.obs[i].active = !this.dolowspecmode;
				}
			}
		}
		if (this.attack != null)
		{
			this.attack.spawndust = !this.dolowspecmode;
			this.attack.spawndecals = !this.dolowspecmode;
		}
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0001A128 File Offset: 0x00018328
	public virtual void Main()
	{
	}

	// Token: 0x04000348 RID: 840
	public bool dolowspecmode;

	// Token: 0x04000349 RID: 841
	[HideInInspector]
	public GameObject[] obs;

	// Token: 0x0400034A RID: 842
	[HideInInspector]
	public AttackScript attack;
}
