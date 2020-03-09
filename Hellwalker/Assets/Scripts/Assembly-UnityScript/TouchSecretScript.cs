using System;
using TMPro;
using UnityEngine;

// Token: 0x020000C1 RID: 193
[Serializable]
public class TouchSecretScript : MonoBehaviour
{
	// Token: 0x06000482 RID: 1154 RVA: 0x00029F6C File Offset: 0x0002816C
	public virtual void Start()
	{
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00029F70 File Offset: 0x00028170
	public virtual void Update()
	{
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00029F74 File Offset: 0x00028174
	public virtual void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.gameObject.layer == 22)
		{
			((TextMeshProUGUI)GameObject.Find("SecretText").GetComponent(typeof(TextMeshProUGUI))).text = "You found a secret!";
			((ClearMessageAfterTime)GameObject.Find("SecretText").GetComponent(typeof(ClearMessageAfterTime))).timer = ((ClearMessageAfterTime)GameObject.Find("MessageText").GetComponent(typeof(ClearMessageAfterTime))).defaulttime;
			((AudioSource)this.secretsound.GetComponent(typeof(AudioSource))).Play();
			UnityEngine.Object.Destroy(hit.transform.gameObject);
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0002A038 File Offset: 0x00028238
	public virtual void Main()
	{
	}

	// Token: 0x0400057F RID: 1407
	public GameObject secretsound;
}
