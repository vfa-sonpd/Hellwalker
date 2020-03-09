using System;
using TMPro;
using UnityEngine;

// Token: 0x02000033 RID: 51
[Serializable]
public class DisplayPlayerCoors : MonoBehaviour
{
	// Token: 0x06000137 RID: 311 RVA: 0x0000DED8 File Offset: 0x0000C0D8
	public virtual void Start()
	{
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000DEDC File Offset: 0x0000C0DC
	public virtual void Update()
	{
		if (GameObject.Find("Player"))
		{
			Vector3 position = GameObject.Find("Player").transform.position;
			((TextMeshProUGUI)this.GetComponent(typeof(TextMeshProUGUI))).text = "x" + position.x.ToString("000") + " y" + position.y.ToString("000") + " z" + position.z.ToString("000");
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000DF88 File Offset: 0x0000C188
	public virtual void Main()
	{
	}
}
