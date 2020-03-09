using System;
using TMPro;
using UnityEngine;

// Token: 0x0200001C RID: 28
[Serializable]
public class ClearMessageAfterTime : MonoBehaviour
{
	// Token: 0x060000C7 RID: 199 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
	public virtual void Start()
	{
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000C2DC File Offset: 0x0000A4DC
	public virtual void Update()
	{
		this.timer -= Time.deltaTime;
		if (this.timer <= (float)0)
		{
			((TextMeshProUGUI)this.GetComponent(typeof(TextMeshProUGUI))).text = string.Empty;
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000C328 File Offset: 0x0000A528
	public virtual void Main()
	{
	}

	// Token: 0x040001A8 RID: 424
	public float timer;

	// Token: 0x040001A9 RID: 425
	public float defaulttime;
}
