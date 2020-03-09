using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
[Serializable]
public class MainMenuPressAnyKeyScript : MonoBehaviour
{
	// Token: 0x060002DC RID: 732 RVA: 0x0001A230 File Offset: 0x00018430
	public virtual void Start()
	{
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0001A234 File Offset: 0x00018434
	public virtual void Update()
	{
		if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !((InGameMenuScript)GameObject.Find("DasMenu").GetComponent(typeof(InGameMenuScript))).isPaused)
		{
			((InGameMenuScript)GameObject.Find("DasMenu").GetComponent(typeof(InGameMenuScript))).togglemenu();
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0001A2A4 File Offset: 0x000184A4
	public virtual void Main()
	{
	}
}
