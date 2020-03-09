using System;
using UnityEngine;

// Token: 0x0200006E RID: 110
[Serializable]
public class LoadoutStuffScript : MonoBehaviour
{
	// Token: 0x060002B9 RID: 697 RVA: 0x000196A8 File Offset: 0x000178A8
	public virtual void Start()
	{
	}

	// Token: 0x060002BA RID: 698 RVA: 0x000196AC File Offset: 0x000178AC
	public virtual void Update()
	{
	}

	// Token: 0x060002BB RID: 699 RVA: 0x000196B0 File Offset: 0x000178B0
	public virtual void toggleloadoutscreen()
	{
		this.loadoutback.active = !this.loadoutback.active;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000196CC File Offset: 0x000178CC
	public virtual void assigninventory()
	{
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		persistScript.weaponinventory[1] = ((LoadoutEntryScript)this.pistolobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[2] = ((LoadoutEntryScript)this.shotgunobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[3] = ((LoadoutEntryScript)this.supershotgunobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[4] = ((LoadoutEntryScript)this.assaultrifleobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[5] = ((LoadoutEntryScript)this.huntingrifleobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[6] = ((LoadoutEntryScript)this.crossbowobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[7] = ((LoadoutEntryScript)this.mortarobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.weaponinventory[8] = ((LoadoutEntryScript)this.riveterobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.ammoinventory[1] = (float)((LoadoutEntryScript)this.pistolobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[2] = (float)((LoadoutEntryScript)this.shotgunobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[3] = (float)((LoadoutEntryScript)this.supershotgunobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[4] = (float)((LoadoutEntryScript)this.assaultrifleobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[5] = (float)((LoadoutEntryScript)this.huntingrifleobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[6] = (float)((LoadoutEntryScript)this.crossbowobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[7] = (float)((LoadoutEntryScript)this.mortarobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.ammoinventory[8] = (float)((LoadoutEntryScript)this.riveterobject.GetComponent(typeof(LoadoutEntryScript))).ammo;
		persistScript.selectedweapon = 1;
		persistScript.havedualshotguns = ((LoadoutEntryScript)this.dualshotgunobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.havedualpistols = ((LoadoutEntryScript)this.dualpistolobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.havedaikatana = ((LoadoutEntryScript)this.swordobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.permduals = ((LoadoutEntryScript)this.dualpistolobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.permshotguns = ((LoadoutEntryScript)this.dualshotgunobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
		persistScript.permdaikatana = ((LoadoutEntryScript)this.swordobject.GetComponent(typeof(LoadoutEntryScript))).haveweapon;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x00019AA0 File Offset: 0x00017CA0
	public virtual void Main()
	{
	}

	// Token: 0x04000327 RID: 807
	public GameObject loadoutback;

	// Token: 0x04000328 RID: 808
	public GameObject dualshotgunobject;

	// Token: 0x04000329 RID: 809
	public GameObject dualpistolobject;

	// Token: 0x0400032A RID: 810
	public GameObject swordobject;

	// Token: 0x0400032B RID: 811
	public GameObject pistolobject;

	// Token: 0x0400032C RID: 812
	public GameObject shotgunobject;

	// Token: 0x0400032D RID: 813
	public GameObject supershotgunobject;

	// Token: 0x0400032E RID: 814
	public GameObject assaultrifleobject;

	// Token: 0x0400032F RID: 815
	public GameObject huntingrifleobject;

	// Token: 0x04000330 RID: 816
	public GameObject crossbowobject;

	// Token: 0x04000331 RID: 817
	public GameObject mortarobject;

	// Token: 0x04000332 RID: 818
	public GameObject riveterobject;
}
