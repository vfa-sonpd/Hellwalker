using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[Serializable]
public class TreasureChestScript : MonoBehaviour
{
	// Token: 0x0600048B RID: 1163 RVA: 0x0002A170 File Offset: 0x00028370
	public virtual void Start()
	{
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0002A1B4 File Offset: 0x000283B4
	public virtual void Update()
	{
		this.dosavestuff();
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0002A1BC File Offset: 0x000283BC
	public virtual void dosavestuff()
	{
		string rhs = null;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<int>(this.alreadyused, this.sav.filename + rhs + "alr3dyu53d");
		}
		if (this.sav.doload)
		{
			if (ES2.Exists(this.sav.filename + rhs + "alr3dyu53d"))
			{
				this.alreadyused = ES2.Load<int>(this.sav.filename + rhs + "alr3dyu53d");
				if (this.alreadyused == 1)
				{
					this.transform.gameObject.tag = "Untagged";
					((Animator)this.GetComponent(typeof(Animator))).SetTrigger("OpenTrigger");
				}
			}
			else
			{
				UnityEngine.Object.Destroy(this.transform.gameObject);
			}
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002A2FC File Offset: 0x000284FC
	public virtual void Main()
	{
	}

	// Token: 0x04000586 RID: 1414
	public GameObject myitem;

	// Token: 0x04000587 RID: 1415
	public int alreadyused;

	// Token: 0x04000588 RID: 1416
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x04000589 RID: 1417
	[HideInInspector]
	public Vector3 origcoors;
}
