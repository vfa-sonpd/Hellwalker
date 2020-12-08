using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A1 RID: 161
[Serializable]
public class SelectionScript : MonoBehaviour
{
    public Inventory inventory;

	// Token: 0x060003DE RID: 990 RVA: 0x00024F30 File Offset: 0x00023130
	public SelectionScript()
	{
		this.speedmultiply = 1;
		this.lastselectedweapon = 1;
	}



	// Token: 0x060003DF RID: 991 RVA: 0x00024F48 File Offset: 0x00023148
	public virtual void Start()
	{
        this.inputmanager = Essential.Instance.inputManager;
        this.weapontogetto = 0;
		this.lastselectedweapon = 1;
		this.havedaikatana = false;
		this.ammocounter = GameObject.Find("AmmoCounter");
		this.weaponlabel = GameObject.Find("WeaponLabel");
		this.startlevel();
		if (this.weapontogetto == 1)
		{
			((AudioSource)this.sickledrawsound.GetComponent(typeof(AudioSource))).Play();
		}

    }

    // Reference to the current holding weapon
    private WeaponData currentWeapon;

    void Equip(WeaponType weaponType)
    {
        // If current weapon is already pistol, return
        if(currentWeapon && currentWeapon.weaponType == weaponType)
        {
            return;
        }

        InventoryMessage invMessage = inventory.GetWeapon(weaponType);

        if(invMessage.status == InventoryStatus.WEAPON_IS_IN_INVENTORY)
        {
            // If the current weapon is null, DON'T start the unequip process
            if(currentWeapon)
            {
                currentWeapon.Unequip();
            }

            currentWeapon = invMessage.GetWeapon();
            currentWeapon.Equip(transform);
        }

        this.deactivateall();
        ((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
    }

	// Token: 0x060003E0 RID: 992 RVA: 0x000252A8 File Offset: 0x000234A8
	public virtual void Update()
	{
        if (this.inputmanager.GetKeyInput("pistol / pistols", 1))
        {
            Equip(WeaponType.Pistol);
        }

        if (this.inputmanager.GetKeyInput("hunting rifle", 1))
        {
            Equip(WeaponType.HuntingRifle);
        }

        if (this.inputmanager.GetKeyInput("shotgun / shotguns", 1))
        {
            Equip(WeaponType.Shotgun);
        }

        if (this.inputmanager.GetKeyInput("assault rifle", 1))
        {
            Equip(WeaponType.MachineGun);
        }

        if (this.inputmanager.GetKeyInput("holster", 1))
		{
			if (this.selectedweapon != 0)
			{
				this.holsteredweapon = this.selectedweapon;
				this.weapontogetto = 0;
				((AudioSource)this.holstersound.GetComponent(typeof(AudioSource))).Play();
			}
			else
			{
				this.weapontogetto = this.holsteredweapon;
			}
		}
		if (this.inputmanager.GetKeyInput("sickles / sword", 1) && this.weaponinventory[0])
		{
			if (this.selectedweapon != 1)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 1;
			if (this.selectedweapon == 1 && this.permdaikatana)
			{
				this.havedaikatana = !this.havedaikatana;
				((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
				if (this.havedaikatana)
				{
					((AudioSource)this.sworddrawsound.GetComponent(typeof(AudioSource))).Play();
				}
				else
				{
					((AudioSource)this.sickledrawsound.GetComponent(typeof(AudioSource))).Play();
				}
			}
		}
		//if (this.inputmanager.GetKeyInput("pistol / pistols", 1) && this.weaponinventory[1])
		//{
  //          Pistol();
  //      }

        if (this.inputmanager.GetKeyInput("shotgun / shotguns", 1) && this.weaponinventory[2])
		{
			//if (this.selectedweapon != 3)
			//{
			//	this.lastselectedweapon = this.selectedweapon;
			//}
			//((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			//this.weapontogetto = 3;
			//if (this.selectedweapon == 3 && this.permshotguns)
			//{
			//	this.havedualshotguns = !this.havedualshotguns;
			//	((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
			//	((AudioSource)this.shotgundrawsound.GetComponent(typeof(AudioSource))).Play();
			//}
		}
		if (this.inputmanager.GetKeyInput("super shotgun", 1) && this.weaponinventory[3])
		{
			if (this.selectedweapon != 4)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 4;
		}
		//if (this.inputmanager.GetKeyInput("assault rifle", 1) && this.weaponinventory[4])
		//{
		//	if (this.selectedweapon != 5)
		//	{
		//		this.lastselectedweapon = this.selectedweapon;
		//	}
		//	((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
		//	this.weapontogetto = 5;
		//}
		//if (this.inputmanager.GetKeyInput("hunting rifle", 1) && this.weaponinventory[5])
		//{
  //          HungtingRifle();
  //      }
		if (this.inputmanager.GetKeyInput("crossbow", 1) && this.weaponinventory[6])
		{
			if (this.selectedweapon != 7)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 7;
		}
		if (this.inputmanager.GetKeyInput("mortar", 1) && this.weaponinventory[7])
		{
			if (this.selectedweapon != 8)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 8;
		}
		if (this.inputmanager.GetKeyInput("riveter", 1) && this.weaponinventory[8])
		{
			if (this.selectedweapon != 9)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 9;
		}
		if (this.inputmanager.GetKeyInput("cigar", 1) && this.weaponinventory[9])
		{
			if (this.selectedweapon != 10)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 10;
		}
		if (this.inputmanager.GetKeyInput("last used weapon", 1) && this.selectedweapon != 0 && this.lastselectedweapon != 0)
		{
			this.weapontogetto = this.lastselectedweapon;
			this.lastselectedweapon = this.selectedweapon;
		}
		this.scroll();
       // print("this.selectedweapon  " + this.selectedweapon);
		if (((AttackScript)this.GetComponent(typeof(AttackScript))).attackdelaytimer <= (float)0)
		{
            if (this.selectedweapon != this.weapontogetto)
            {
                ((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
                //bool flag = false;
                //if (this.weapontogetto == 1 && !this.havedaikatana)
                //{
                //    ((AudioSource)this.sickledrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 1 && this.havedaikatana)
                //{
                //    ((AudioSource)this.sworddrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 6)
                //{
                //    ((AudioSource)this.rifledrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 8)
                //{
                //    ((AudioSource)this.mortardrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 4)
                //{
                //    ((AudioSource)this.supershotgundrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 3)
                //{
                //    ((AudioSource)this.shotgundrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 5)
                //{
                //    ((AudioSource)this.m16drawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 7)
                //{
                //    ((AudioSource)this.crossbowdrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (this.weapontogetto == 9)
                //{
                //    ((AudioSource)this.riveterdrawsound.GetComponent(typeof(AudioSource))).Play();
                //    flag = true;
                //}
                //if (!flag)
                //{
                //    ((AudioSource)this.weaponswitchsound.GetComponent(typeof(AudioSource))).Play();
                //}
                this.selectedweapon = this.weapontogetto;
            }
          
		}
		if (this.selectedweapon == 1)
		{

			this.deactivateall();
			if (!this.havedaikatana)
			{
				((Renderer)this.hammermodel.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.hammermodel2.GetComponent(typeof(Renderer))).enabled = true;
			}
			else
			{
				((Renderer)this.swordmodel.GetComponent(typeof(Renderer))).enabled = true;
			}
		}
		if (this.selectedweapon == 4)
		{
			this.deactivateall();
			((Renderer)this.supershotgunmodel.GetComponent(typeof(Renderer))).enabled = true;
		}
		if (this.selectedweapon == 2)
		{
            //this.deactivateall();
            //if (!this.havedualpistols)
            //{
            //    print("nell");
            //    ((Renderer)this.pistolmodel.GetComponent(typeof(Renderer))).enabled = true;
            //    ((Renderer)this.pistolslide.GetComponent(typeof(Renderer))).enabled = true;
            //}
            //else
            //{
            //    ((Renderer)this.leftpistolmodel.GetComponent(typeof(Renderer))).enabled = true;
            //    ((Renderer)this.rightpistolmodel.GetComponent(typeof(Renderer))).enabled = true;
            //    ((Renderer)this.leftpistolslide.GetComponent(typeof(Renderer))).enabled = true;
            //    ((Renderer)this.rightpistolslide.GetComponent(typeof(Renderer))).enabled = true;
            //}
        }
		if (this.selectedweapon == 3)
		{
			this.deactivateall();
			if (!this.havedualshotguns)
			{
				((Renderer)this.shotgunmodel.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.shotgunlevermodel.GetComponent(typeof(Renderer))).enabled = true;
			}
			else
			{
				((Renderer)this.dualshotgunmodelleft.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.dualshotgunmodelleftlever.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.dualshotgunmodelright.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.dualshotgunmodelrightlever.GetComponent(typeof(Renderer))).enabled = true;
			}
		}
		if (this.selectedweapon == 7)
		{
			this.deactivateall();
			((Renderer)this.chainsawmodel.GetComponent(typeof(Renderer))).enabled = true;
			this.arrowparts.active = true;
			if (this.ammoinventory[6] <= (float)0)
			{
				this.arrowparts.active = false;
			}
		}
		if (this.selectedweapon == 5)
		{
			this.deactivateall();
			((Renderer)this.m16model.GetComponent(typeof(Renderer))).enabled = true;
		}
		if (this.selectedweapon == 6)
		{
         
		}
		if (this.selectedweapon == 8)
		{
			this.deactivateall();
			((Renderer)this.buzzsawmodel.GetComponent(typeof(Renderer))).enabled = true;
		}
		if (this.selectedweapon == 9)
		{
			this.deactivateall();
			((Renderer)this.rivetermodel.GetComponent(typeof(Renderer))).enabled = true;
		}
		if (this.selectedweapon == 10)
		{
			this.deactivateall();
			((Renderer)this.cigarmodel.GetComponent(typeof(Renderer))).enabled = true;
		}
		if (this.selectedweapon == 0)
		{
			this.deactivateall();
		}
		this.chainsawsound.active = ((Renderer)this.chainsawmodel.GetComponent(typeof(Renderer))).enabled;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00026D64 File Offset: 0x00024F64
	public virtual void deactivateall()
	{
		((Renderer)this.hammermodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.hammermodel2.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.swordmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.shotgunmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.shotgunlevermodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.supershotgunmodel.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.pistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.riflemodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.chainsawmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.m16model.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.buzzsawmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.rivetermodel.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.leftpistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.rightpistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.amuletmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.cigarmodel.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.pistolslide.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.leftpistolslide.GetComponent(typeof(Renderer))).enabled = false;
		//((Renderer)this.rightpistolslide.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.dualshotgunmodelleft.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.dualshotgunmodelleftlever.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.dualshotgunmodelright.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.dualshotgunmodelrightlever.GetComponent(typeof(Renderer))).enabled = false;
		this.arrowparts.active = false;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00027060 File Offset: 0x00025260
	public virtual void scroll()
	{
		int num = 0;
		if (Input.GetAxis("Mouse ScrollWheel") > (float)0 || Input.GetButtonDown("joystickbutton4"))
		{
			if (!this.flipwheel)
			{
				num = -1;
			}
			else
			{
				num = 1;
			}
		}
		if (Input.GetAxis("Mouse ScrollWheel") < (float)0 || Input.GetButtonDown("joystickbutton5"))
		{
			if (!this.flipwheel)
			{
				num = 1;
			}
			else
			{
				num = -1;
			}
		}
		if (num != 0 && this.selectedweapon != 0)
		{
			this.lastselectedweapon = this.selectedweapon;
			bool flag = false;
			while (!flag)
			{
				this.weapontogetto += num;
				if (this.weapontogetto > 9)
				{
					this.weapontogetto = 1;
				}
				if (this.weapontogetto < 1)
				{
					this.weapontogetto = 9;
				}
				if (this.weaponinventory[this.weapontogetto - 1])
				{
					flag = true;
				}
			}
		}
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00027158 File Offset: 0x00025358
	public virtual void startlevel()
	{
        return;
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		//StatScript statScript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		//PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		//ScreenSizeScript screenSizeScript = (ScreenSizeScript)GameObject.Find("Player").GetComponent(typeof(ScreenSizeScript));
		if (persistScript.hardcore)
		{
			persistScript.resetweapons();
		}
		for (int i = 0; i < 10; i++)
		{
			this.weaponinventory[i] = persistScript.weaponinventory[i];
			this.ammoinventory[i] = persistScript.ammoinventory[i];
		}
		this.weapontogetto = persistScript.selectedweapon;
		this.selectedweapon = persistScript.selectedweapon;
		this.havedualshotguns = persistScript.havedualshotguns;
		this.havedualpistols = persistScript.havedualpistols;
		this.havedaikatana = persistScript.havedaikatana;
		this.permduals = persistScript.permduals;
		this.permshotguns = persistScript.permshotguns;
		this.permdaikatana = persistScript.permdaikatana;
		//playerHealthManagement.myhealth = persistScript.playerhealth;
		//playerHealthManagement.myarmor = persistScript.playerarmor;
		//screenSizeScript.currentsize = persistScript.screensize;
		//screenSizeScript.huddetail = persistScript.huddetail;
		persistScript.pacifistaward = true;
		persistScript.completionistaward = false;
		persistScript.ninjaaward = true;
		persistScript.lowtechaward = true;
		//screenSizeScript.sethudstuff();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000272F4 File Offset: 0x000254F4
	public virtual void Main()
	{
	}

	// Token: 0x040004BF RID: 1215
	public bool flipwheel;

	// Token: 0x040004C0 RID: 1216
	public bool haveredkey;

	// Token: 0x040004C1 RID: 1217
	public bool havebluekey;

	// Token: 0x040004C2 RID: 1218
	public bool haveyellowkey;

	// Token: 0x040004C3 RID: 1219
	public bool weaponenhance;

	// Token: 0x040004C4 RID: 1220
	public bool havejetpack;

	// Token: 0x040004C5 RID: 1221
	public bool ignorelava;

	// Token: 0x040004C6 RID: 1222
	public int speedmultiply;

	// Token: 0x040004C7 RID: 1223
	[HideInInspector]
	public float enhancetimer;

	// Token: 0x040004C8 RID: 1224
	public GameObject ammocounter;

	// Token: 0x040004C9 RID: 1225
	public GameObject weaponlabel;

	// Token: 0x040004CA RID: 1226
	public string[] weaponnames;

	// Token: 0x040004CB RID: 1227
	public bool havedualshotguns;

	// Token: 0x040004CC RID: 1228
	public bool havedualpistols;

	// Token: 0x040004CD RID: 1229
	public bool havesupershotgun;

	// Token: 0x040004CE RID: 1230
	[HideInInspector]
	public bool havedaikatana;

	// Token: 0x040004CF RID: 1231
	[HideInInspector]
	public bool permduals;

	// Token: 0x040004D0 RID: 1232
	public bool permshotguns;

	// Token: 0x040004D1 RID: 1233
	public bool permdaikatana;

	// Token: 0x040004D2 RID: 1234
	public bool permsupershotgun;

	// Token: 0x040004D3 RID: 1235
	public bool havesingleshotgun;

	// Token: 0x040004D4 RID: 1236
	public bool[] weaponinventory;

	// Token: 0x040004D5 RID: 1237
	public float[] ammoinventory;

	// Token: 0x040004D6 RID: 1238
	public float[] maxammo;

	// Token: 0x040004D7 RID: 1239
	public float[] zoomamount;

	// Token: 0x040004D8 RID: 1240
	public int selectedweapon;

	// Token: 0x040004D9 RID: 1241
	public GameObject hammermodel;

	// Token: 0x040004DA RID: 1242
	public GameObject hammermodel2;

	// Token: 0x040004DB RID: 1243
	public GameObject swordmodel;

	// Token: 0x040004DC RID: 1244
	public GameObject shotgunmodel;

	// Token: 0x040004DD RID: 1245
	public GameObject shotgunlevermodel;

	// Token: 0x040004DE RID: 1246
	public GameObject dualshotgunmodelleft;

	// Token: 0x040004DF RID: 1247
	public GameObject dualshotgunmodelleftlever;

	// Token: 0x040004E0 RID: 1248
	public GameObject dualshotgunmodelright;

	// Token: 0x040004E1 RID: 1249
	public GameObject dualshotgunmodelrightlever;

	// Token: 0x040004E2 RID: 1250
	public GameObject supershotgunmodel;

	// Token: 0x040004E3 RID: 1251
	public GameObject pistolmodel;

	// Token: 0x040004E4 RID: 1252
	public GameObject rightpistolmodel;

	// Token: 0x040004E5 RID: 1253
	public GameObject leftpistolmodel;

	// Token: 0x040004E6 RID: 1254
	public GameObject pistolslide;

	// Token: 0x040004E7 RID: 1255
	public GameObject rightpistolslide;

	// Token: 0x040004E8 RID: 1256
	public GameObject leftpistolslide;

	// Token: 0x040004E9 RID: 1257
	public GameObject riflemodel;

	// Token: 0x040004EA RID: 1258
	public GameObject chainsawmodel;

	// Token: 0x040004EB RID: 1259
	public GameObject arrowparts;

	// Token: 0x040004EC RID: 1260
	public GameObject chainsawsound;

	// Token: 0x040004ED RID: 1261
	public GameObject m16model;

	// Token: 0x040004EE RID: 1262
	public GameObject buzzsawmodel;

	// Token: 0x040004EF RID: 1263
	public GameObject rivetermodel;

	// Token: 0x040004F0 RID: 1264
	public GameObject amuletmodel;

	// Token: 0x040004F1 RID: 1265
	public GameObject cigarmodel;

	// Token: 0x040004F2 RID: 1266
	public GameObject weaponswitchsound;

	// Token: 0x040004F3 RID: 1267
	public GameObject holstersound;

	// Token: 0x040004F4 RID: 1268
	public GameObject sickledrawsound;

	// Token: 0x040004F5 RID: 1269
	public GameObject supershotgundrawsound;

	// Token: 0x040004F6 RID: 1270
	public GameObject shotgundrawsound;

	// Token: 0x040004F7 RID: 1271
	public GameObject rifledrawsound;

	// Token: 0x040004F8 RID: 1272
	public GameObject m16drawsound;

	// Token: 0x040004F9 RID: 1273
	public GameObject sworddrawsound;

	// Token: 0x040004FA RID: 1274
	public GameObject riveterdrawsound;

	// Token: 0x040004FB RID: 1275
	public GameObject mortardrawsound;

	// Token: 0x040004FC RID: 1276
	public GameObject crossbowdrawsound;

	// Token: 0x040004FD RID: 1277
	[HideInInspector]
	public int weapontogetto;

	// Token: 0x040004FE RID: 1278
	[HideInInspector]
	public int holsteredweapon;

	// Token: 0x040004FF RID: 1279
	[HideInInspector]
	public int lastselectedweapon;

	// Token: 0x04000500 RID: 1280
	[HideInInspector]
	public MyInputManager inputmanager;

}
