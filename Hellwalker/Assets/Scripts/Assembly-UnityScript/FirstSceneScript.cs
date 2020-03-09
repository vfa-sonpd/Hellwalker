using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[Serializable]
public class FirstSceneScript : MonoBehaviour
{
	// Token: 0x060001A6 RID: 422 RVA: 0x0001062C File Offset: 0x0000E82C
	public virtual void Start()
	{
		PlayerPrefs.SetInt("FakeLoadingScreen", 0);
		Application.LoadLevel("Menu");
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00010644 File Offset: 0x0000E844
	public virtual void Update()
	{
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00010648 File Offset: 0x0000E848
	public virtual void Main()
	{
	}
}
