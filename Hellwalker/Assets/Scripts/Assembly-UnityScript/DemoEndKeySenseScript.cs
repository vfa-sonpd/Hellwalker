using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
[Serializable]
public class DemoEndKeySenseScript : MonoBehaviour
{
	// Token: 0x06000103 RID: 259 RVA: 0x0000CD50 File Offset: 0x0000AF50
	public virtual void Start()
	{
		Input.ResetInputAxes();
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000CD58 File Offset: 0x0000AF58
	public virtual void Update()
	{
		if (Input.anyKeyDown)
		{
			Application.LoadLevel("Menu");
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000CD70 File Offset: 0x0000AF70
	public virtual void Main()
	{
	}
}
