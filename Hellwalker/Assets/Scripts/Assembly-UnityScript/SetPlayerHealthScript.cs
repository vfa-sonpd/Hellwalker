using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[Serializable]
public class SetPlayerHealthScript : MonoBehaviour
{
	// Token: 0x060003FF RID: 1023 RVA: 0x000275DC File Offset: 0x000257DC
	public virtual void Start()
	{
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x000275E0 File Offset: 0x000257E0
	public virtual void Update()
	{
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x000275E4 File Offset: 0x000257E4
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 10)
		{
			PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)hit.transform.gameObject.GetComponent(typeof(PlayerHealthManagement));
			playerHealthManagement.myhealth = this.health;
			playerHealthManagement.myarmor = this.armor;
			UnityEngine.Object.Destroy(this.transform.gameObject);
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00027650 File Offset: 0x00025850
	public virtual void Main()
	{
	}

	// Token: 0x04000511 RID: 1297
	public float health;

	// Token: 0x04000512 RID: 1298
	public float armor;
}
