using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A1 RID: 161
[Serializable]
public class SelectionScript : MonoBehaviour
{
	// Token: 0x060003DE RID: 990 RVA: 0x00024F30 File Offset: 0x00023130
	public SelectionScript()
	{
		this.speedmultiply = 1;
		this.lastselectedweapon = 1;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00024F48 File Offset: 0x00023148
	public virtual void Start()
	{
		this.W = new TextMeshProUGUI[9];
		this.B = new Image[9];
		this.W[0] = (TextMeshProUGUI)GameObject.Find("W1").GetComponent(typeof(TextMeshProUGUI));
		this.W[1] = (TextMeshProUGUI)GameObject.Find("W2").GetComponent(typeof(TextMeshProUGUI));
		this.W[2] = (TextMeshProUGUI)GameObject.Find("W3").GetComponent(typeof(TextMeshProUGUI));
		this.W[3] = (TextMeshProUGUI)GameObject.Find("W4").GetComponent(typeof(TextMeshProUGUI));
		this.W[4] = (TextMeshProUGUI)GameObject.Find("W5").GetComponent(typeof(TextMeshProUGUI));
		this.W[5] = (TextMeshProUGUI)GameObject.Find("W6").GetComponent(typeof(TextMeshProUGUI));
		this.W[6] = (TextMeshProUGUI)GameObject.Find("W7").GetComponent(typeof(TextMeshProUGUI));
		this.W[7] = (TextMeshProUGUI)GameObject.Find("W8").GetComponent(typeof(TextMeshProUGUI));
		this.W[8] = (TextMeshProUGUI)GameObject.Find("W9").GetComponent(typeof(TextMeshProUGUI));
		this.B[0] = (Image)GameObject.Find("B0").GetComponent(typeof(Image));
		this.B[1] = (Image)GameObject.Find("B1").GetComponent(typeof(Image));
		this.B[2] = (Image)GameObject.Find("B2").GetComponent(typeof(Image));
		this.B[3] = (Image)GameObject.Find("B3").GetComponent(typeof(Image));
		this.B[4] = (Image)GameObject.Find("B4").GetComponent(typeof(Image));
		this.B[5] = (Image)GameObject.Find("B5").GetComponent(typeof(Image));
		this.B[6] = (Image)GameObject.Find("B6").GetComponent(typeof(Image));
		this.B[7] = (Image)GameObject.Find("B7").GetComponent(typeof(Image));
		this.B[8] = (Image)GameObject.Find("B8").GetComponent(typeof(Image));
		this.inputmanager = (MyInputManager)GameObject.Find("DasMenu").GetComponent(typeof(MyInputManager));
		this.weapontogetto = 1;
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

	// Token: 0x060003E0 RID: 992 RVA: 0x000252A8 File Offset: 0x000234A8
	public virtual void Update()
	{
		if (this.ammoinventory[2] > this.ammoinventory[3])
		{
			this.ammoinventory[3] = this.ammoinventory[2];
		}
		this.ammoinventory[2] = this.ammoinventory[3];
		if (this.weaponinventory[0])
		{
			float a = 0.45f;
			Color color = this.W[0].color;
			float num = color.a = a;
			Color color2 = this.W[0].color = color;
		}
		if (this.weaponinventory[1])
		{
			float a2 = 0.45f;
			Color color3 = this.W[1].color;
			float num2 = color3.a = a2;
			Color color4 = this.W[1].color = color3;
		}
		if (this.weaponinventory[2])
		{
			float a3 = 0.45f;
			Color color5 = this.W[2].color;
			float num3 = color5.a = a3;
			Color color6 = this.W[2].color = color5;
		}
		if (this.weaponinventory[3])
		{
			float a4 = 0.45f;
			Color color7 = this.W[3].color;
			float num4 = color7.a = a4;
			Color color8 = this.W[3].color = color7;
		}
		if (this.weaponinventory[4])
		{
			float a5 = 0.45f;
			Color color9 = this.W[4].color;
			float num5 = color9.a = a5;
			Color color10 = this.W[4].color = color9;
		}
		if (this.weaponinventory[5])
		{
			float a6 = 0.45f;
			Color color11 = this.W[5].color;
			float num6 = color11.a = a6;
			Color color12 = this.W[5].color = color11;
		}
		if (this.weaponinventory[6])
		{
			float a7 = 0.45f;
			Color color13 = this.W[6].color;
			float num7 = color13.a = a7;
			Color color14 = this.W[6].color = color13;
		}
		if (this.weaponinventory[7])
		{
			float a8 = 0.45f;
			Color color15 = this.W[7].color;
			float num8 = color15.a = a8;
			Color color16 = this.W[7].color = color15;
		}
		if (this.weaponinventory[8])
		{
			float a9 = 0.45f;
			Color color17 = this.W[8].color;
			float num9 = color17.a = a9;
			Color color18 = this.W[8].color = color17;
		}
		if (this.weaponinventory[0])
		{
			float a10 = 0.45f;
			Color color19 = this.B[0].color;
			float num10 = color19.a = a10;
			Color color20 = this.B[0].color = color19;
		}
		if (this.weaponinventory[1])
		{
			float a11 = 0.45f;
			Color color21 = this.B[1].color;
			float num11 = color21.a = a11;
			Color color22 = this.B[1].color = color21;
		}
		if (this.weaponinventory[2])
		{
			float a12 = 0.45f;
			Color color23 = this.B[2].color;
			float num12 = color23.a = a12;
			Color color24 = this.B[2].color = color23;
		}
		if (this.weaponinventory[3])
		{
			float a13 = 0.45f;
			Color color25 = this.B[3].color;
			float num13 = color25.a = a13;
			Color color26 = this.B[3].color = color25;
		}
		if (this.weaponinventory[4])
		{
			float a14 = 0.45f;
			Color color27 = this.B[4].color;
			float num14 = color27.a = a14;
			Color color28 = this.B[4].color = color27;
		}
		if (this.weaponinventory[5])
		{
			float a15 = 0.45f;
			Color color29 = this.B[5].color;
			float num15 = color29.a = a15;
			Color color30 = this.B[5].color = color29;
		}
		if (this.weaponinventory[6])
		{
			float a16 = 0.45f;
			Color color31 = this.B[6].color;
			float num16 = color31.a = a16;
			Color color32 = this.B[6].color = color31;
		}
		if (this.weaponinventory[7])
		{
			float a17 = 0.45f;
			Color color33 = this.B[7].color;
			float num17 = color33.a = a17;
			Color color34 = this.B[7].color = color33;
		}
		if (this.weaponinventory[8])
		{
			float a18 = 0.45f;
			Color color35 = this.B[8].color;
			float num18 = color35.a = a18;
			Color color36 = this.B[8].color = color35;
		}
		if (this.selectedweapon == 1)
		{
			int num19 = 1;
			Color color37 = this.W[0].color;
			float num20 = color37.a = (float)num19;
			Color color38 = this.W[0].color = color37;
		}
		if (this.selectedweapon == 2)
		{
			int num21 = 1;
			Color color39 = this.W[1].color;
			float num22 = color39.a = (float)num21;
			Color color40 = this.W[1].color = color39;
		}
		if (this.selectedweapon == 3)
		{
			int num23 = 1;
			Color color41 = this.W[2].color;
			float num24 = color41.a = (float)num23;
			Color color42 = this.W[2].color = color41;
		}
		if (this.selectedweapon == 4)
		{
			int num25 = 1;
			Color color43 = this.W[3].color;
			float num26 = color43.a = (float)num25;
			Color color44 = this.W[3].color = color43;
		}
		if (this.selectedweapon == 5)
		{
			int num27 = 1;
			Color color45 = this.W[4].color;
			float num28 = color45.a = (float)num27;
			Color color46 = this.W[4].color = color45;
		}
		if (this.selectedweapon == 6)
		{
			int num29 = 1;
			Color color47 = this.W[5].color;
			float num30 = color47.a = (float)num29;
			Color color48 = this.W[5].color = color47;
		}
		if (this.selectedweapon == 7)
		{
			int num31 = 1;
			Color color49 = this.W[6].color;
			float num32 = color49.a = (float)num31;
			Color color50 = this.W[6].color = color49;
		}
		if (this.selectedweapon == 8)
		{
			int num33 = 1;
			Color color51 = this.W[7].color;
			float num34 = color51.a = (float)num33;
			Color color52 = this.W[7].color = color51;
		}
		if (this.selectedweapon == 9)
		{
			int num35 = 1;
			Color color53 = this.W[8].color;
			float num36 = color53.a = (float)num35;
			Color color54 = this.W[8].color = color53;
		}
		if (this.selectedweapon == 1)
		{
			int num37 = 1;
			Color color55 = this.B[0].color;
			float num38 = color55.a = (float)num37;
			Color color56 = this.B[0].color = color55;
		}
		if (this.selectedweapon == 2)
		{
			int num39 = 1;
			Color color57 = this.B[1].color;
			float num40 = color57.a = (float)num39;
			Color color58 = this.B[1].color = color57;
		}
		if (this.selectedweapon == 3)
		{
			int num41 = 1;
			Color color59 = this.B[2].color;
			float num42 = color59.a = (float)num41;
			Color color60 = this.B[2].color = color59;
		}
		if (this.selectedweapon == 4)
		{
			int num43 = 1;
			Color color61 = this.B[3].color;
			float num44 = color61.a = (float)num43;
			Color color62 = this.B[3].color = color61;
		}
		if (this.selectedweapon == 5)
		{
			int num45 = 1;
			Color color63 = this.B[4].color;
			float num46 = color63.a = (float)num45;
			Color color64 = this.B[4].color = color63;
		}
		if (this.selectedweapon == 6)
		{
			int num47 = 1;
			Color color65 = this.B[5].color;
			float num48 = color65.a = (float)num47;
			Color color66 = this.B[5].color = color65;
		}
		if (this.selectedweapon == 7)
		{
			int num49 = 1;
			Color color67 = this.B[6].color;
			float num50 = color67.a = (float)num49;
			Color color68 = this.B[6].color = color67;
		}
		if (this.selectedweapon == 8)
		{
			int num51 = 1;
			Color color69 = this.B[7].color;
			float num52 = color69.a = (float)num51;
			Color color70 = this.B[7].color = color69;
		}
		if (this.selectedweapon == 9)
		{
			int num53 = 1;
			Color color71 = this.B[8].color;
			float num54 = color71.a = (float)num53;
			Color color72 = this.B[8].color = color71;
		}
		this.enhancetimer -= Time.deltaTime;
		if (this.enhancetimer <= (float)0)
		{
			this.enhancetimer = (float)0;
			this.weaponenhance = false;
			((Image)GameObject.Find("EnhanceOverlay").GetComponent(typeof(Image))).enabled = false;
		}
		if (this.weaponenhance)
		{
			((Image)GameObject.Find("EnhanceOverlay").GetComponent(typeof(Image))).enabled = true;
		}
		if (this.permshotguns && !this.havedualshotguns && this.weapontogetto != 3)
		{
			this.havedualshotguns = true;
		}
		if (this.permduals && !this.havedualpistols && this.weapontogetto != 2)
		{
			this.havedualpistols = true;
		}
		if (this.permdaikatana && !this.havedaikatana && this.weapontogetto != 1)
		{
			this.havedaikatana = true;
		}
		if (this.permsupershotgun && !this.havesupershotgun && this.weapontogetto != 4)
		{
			this.havesupershotgun = true;
		}
		if (this.selectedweapon != 1 && this.selectedweapon != 0 && this.selectedweapon != 10)
		{
			((TextMeshProUGUI)this.ammocounter.GetComponent(typeof(TextMeshProUGUI))).text = Mathf.Floor(this.ammoinventory[this.selectedweapon - 1]).ToString();
		}
		else
		{
			((TextMeshProUGUI)this.ammocounter.GetComponent(typeof(TextMeshProUGUI))).text = "-";
		}
		if (this.selectedweapon != 0)
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = this.weaponnames[this.selectedweapon - 1];
		}
		else
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = "-";
		}
		if (this.selectedweapon == 2 && this.havedualpistols)
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = "Pistol x2";
		}
		if (this.selectedweapon == 3 && this.havedualshotguns)
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = "Shotgun x2";
		}
		if (this.selectedweapon == 1 && this.havedaikatana)
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = "Sword";
		}
		if (this.selectedweapon == 4 && this.havesupershotgun)
		{
			((TextMeshProUGUI)this.weaponlabel.GetComponent(typeof(TextMeshProUGUI))).text = "Super Shotgun";
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
		if (this.inputmanager.GetKeyInput("pistol / pistols", 1) && this.weaponinventory[1])
		{
			if (this.selectedweapon != 2)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 2;
			if (this.selectedweapon == 2 && this.permduals)
			{
				this.havedualpistols = !this.havedualpistols;
				((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
				((AudioSource)this.weaponswitchsound.GetComponent(typeof(AudioSource))).Play();
			}
		}
		if (this.inputmanager.GetKeyInput("shotgun / shotguns", 1) && this.weaponinventory[2])
		{
			if (this.selectedweapon != 3)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 3;
			if (this.selectedweapon == 3 && this.permshotguns)
			{
				this.havedualshotguns = !this.havedualshotguns;
				((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
				((AudioSource)this.shotgundrawsound.GetComponent(typeof(AudioSource))).Play();
			}
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
		if (this.inputmanager.GetKeyInput("assault rifle", 1) && this.weaponinventory[4])
		{
			if (this.selectedweapon != 5)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 5;
		}
		if (this.inputmanager.GetKeyInput("hunting rifle", 1) && this.weaponinventory[5])
		{
			if (this.selectedweapon != 6)
			{
				this.lastselectedweapon = this.selectedweapon;
			}
			((PickUpScriptV2)GameObject.Find("MainCamera").GetComponent(typeof(PickUpScriptV2))).dropobject(false);
			this.weapontogetto = 6;
		}
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
		if (((AttackScript)this.GetComponent(typeof(AttackScript))).attackdelaytimer <= (float)0)
		{
			if (this.selectedweapon != this.weapontogetto)
			{
				((Animator)this.GetComponent(typeof(Animator))).SetTrigger("StopAll");
				bool flag = false;
				if (this.weapontogetto == 1 && !this.havedaikatana)
				{
					((AudioSource)this.sickledrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 1 && this.havedaikatana)
				{
					((AudioSource)this.sworddrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 6)
				{
					((AudioSource)this.rifledrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 8)
				{
					((AudioSource)this.mortardrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 4)
				{
					((AudioSource)this.supershotgundrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 3)
				{
					((AudioSource)this.shotgundrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 5)
				{
					((AudioSource)this.m16drawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 7)
				{
					((AudioSource)this.crossbowdrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (this.weapontogetto == 9)
				{
					((AudioSource)this.riveterdrawsound.GetComponent(typeof(AudioSource))).Play();
					flag = true;
				}
				if (!flag)
				{
					((AudioSource)this.weaponswitchsound.GetComponent(typeof(AudioSource))).Play();
				}
			}
			this.selectedweapon = this.weapontogetto;
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
			this.deactivateall();
			if (!this.havedualpistols)
			{
				((Renderer)this.pistolmodel.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.pistolslide.GetComponent(typeof(Renderer))).enabled = true;
			}
			else
			{
				((Renderer)this.leftpistolmodel.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.rightpistolmodel.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.leftpistolslide.GetComponent(typeof(Renderer))).enabled = true;
				((Renderer)this.rightpistolslide.GetComponent(typeof(Renderer))).enabled = true;
			}
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
			this.deactivateall();
			((Renderer)this.riflemodel.GetComponent(typeof(Renderer))).enabled = true;
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
		((Renderer)this.pistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.riflemodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.chainsawmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.m16model.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.buzzsawmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.rivetermodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.leftpistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.rightpistolmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.amuletmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.cigarmodel.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.pistolslide.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.leftpistolslide.GetComponent(typeof(Renderer))).enabled = false;
		((Renderer)this.rightpistolslide.GetComponent(typeof(Renderer))).enabled = false;
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
		PersistScript persistScript = (PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript));
		StatScript statScript = (StatScript)GameObject.Find("StatObject").GetComponent(typeof(StatScript));
		PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
		ScreenSizeScript screenSizeScript = (ScreenSizeScript)GameObject.Find("Player").GetComponent(typeof(ScreenSizeScript));
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
		playerHealthManagement.myhealth = persistScript.playerhealth;
		playerHealthManagement.myarmor = persistScript.playerarmor;
		screenSizeScript.currentsize = persistScript.screensize;
		screenSizeScript.huddetail = persistScript.huddetail;
		persistScript.pacifistaward = true;
		persistScript.completionistaward = false;
		persistScript.ninjaaward = true;
		persistScript.lowtechaward = true;
		screenSizeScript.sethudstuff();
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

	// Token: 0x04000501 RID: 1281
	[HideInInspector]
	public Image[] B;

	// Token: 0x04000502 RID: 1282
	[HideInInspector]
	public TextMeshProUGUI[] W;
}
