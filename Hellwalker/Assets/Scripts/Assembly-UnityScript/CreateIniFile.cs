using System;
using System.IO;
using UnityEngine;

// Token: 0x0200001E RID: 30
[Serializable]
public class CreateIniFile : MonoBehaviour
{
	// Token: 0x060000D5 RID: 213 RVA: 0x0000C668 File Offset: 0x0000A868
	public virtual void Start()
	{
		if (!File.Exists(Application.dataPath + "/../config/dusk.ini"))
		{
			File.Copy(Application.dataPath + "/../defaults/default.ini", Application.dataPath + "/../config/dusk.ini");
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
	public virtual void Update()
	{
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
	public virtual void Main()
	{
	}
}
