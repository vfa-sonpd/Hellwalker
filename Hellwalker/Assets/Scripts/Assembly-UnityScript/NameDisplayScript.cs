using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
[Serializable]
public class NameDisplayScript : MonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
	public virtual void Start()
	{
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
	}

	// Token: 0x0600033C RID: 828 RVA: 0x0001F108 File Offset: 0x0001D308
	public virtual void Update()
	{
		string rhs = string.Empty;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<string>(this.myname, this.sav.filename + rhs + "myn4m3");
		}
		if (this.sav.doload && ES2.Exists(this.sav.filename + rhs + "myn4m3"))
		{
			this.myname = ES2.Load<string>(this.sav.filename + rhs + "myn4m3");
		}
	}

	// Token: 0x0600033D RID: 829 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
	public virtual void Main()
	{
	}

	// Token: 0x040003FD RID: 1021
	public string myname;

	// Token: 0x040003FE RID: 1022
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x040003FF RID: 1023
	[HideInInspector]
	public Vector3 origcoors;
}
