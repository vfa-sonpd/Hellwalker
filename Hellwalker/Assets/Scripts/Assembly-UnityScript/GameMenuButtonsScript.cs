using System;
using Boo.Lang.Runtime;
using IniParser;
using IniParser.Model;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x02000050 RID: 80
[Serializable]
public class GameMenuButtonsScript : MonoBehaviour
{
	// Token: 0x060001C5 RID: 453 RVA: 0x00010BE4 File Offset: 0x0000EDE4
	public virtual void Awake()
	{
		this.filename = Application.dataPath + "/../config/dusk.ini";
		this.parser = new FileIniDataParser();
		this.data = this.parser.ReadFile(this.filename);
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00010C20 File Offset: 0x0000EE20
	public virtual void Start()
	{
		this.intro = !this.intToBool(this.LoadConfigInt("firstrun"));
		this.introdelaything = (float)1;
		this.sav = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		this.origcoors = this.transform.position;
		this.dasmenu = GameObject.Find("DasMenu");
		this.temphardcore = false;
		((Slider)this.crosshairslider.GetComponent(typeof(Slider))).maxValue = (float)(((CrosshairStyleScript)this.crosshair.GetComponent(typeof(CrosshairStyleScript))).crosshairarray.Length - 1);
		this.wcam = GameObject.Find("WeaponCam");
		this.fswitch = GameObject.Find("FilteringSwitcher");
		this.mcam = GameObject.Find("MainCamera");
		this.mplayer = GameObject.Find("MusicPlayer");
		this.phand = GameObject.Find("PlayerHand");
		this.plr = GameObject.Find("Player");
		this.stats = GameObject.Find("StatObject");
		GameObject gameObject = GameObject.Find("LoadingGameImage");
		this.wan = GameObject.Find("WeaponAnimator");
		if (!PlayerPrefs.HasKey("settingsexist6_2"))
		{
			this.SetDefaults();
		}
		this.SetBasedOnPreset();
		this.UpdateSettings();
		this.UpdateText();
		this.currentresolutionindex = this.findresolutionindex(Screen.width, Screen.height);
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00010DAC File Offset: 0x0000EFAC
	public virtual void Update()
	{
		this.dosavestuff();
		if (((InGameMenuScript)this.dasmenu.GetComponent(typeof(InGameMenuScript))).isPaused)
		{
			this.introdelaything -= Time.deltaTime;
			if (this.introdelaything < (float)0)
			{
				this.introdelaything = (float)0;
			}
			if (Input.GetMouseButtonDown(0) && this.introdelaything <= (float)0)
			{
				this.lookatoptions.active = false;
				this.SaveConfig("firstrun", 1);
				this.intro = false;
			}
		}
		if (Input.GetMouseButtonUp(0) && ((InGameMenuScript)this.dasmenu.GetComponent(typeof(InGameMenuScript))).isPaused)
		{
			this.parser.WriteFile(this.filename, this.data, null);
		}
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00010E90 File Offset: 0x0000F090
	public virtual void PressSettingsButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[1].active = true;
		this.UpdateText();
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00010ED4 File Offset: 0x0000F0D4
	public virtual void PressVideoButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[2].active = true;
		this.UpdateText();
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00010F18 File Offset: 0x0000F118
	public virtual void PressAudioButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[5].active = true;
		this.UpdateText();
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00010F5C File Offset: 0x0000F15C
	public virtual void PressBindingsButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[6].active = true;
		((BindControlsScript)this.GetComponent(typeof(BindControlsScript))).updatetext();
		this.UpdateText();
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00010FB8 File Offset: 0x0000F1B8
	public virtual void PressGameButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[3].active = true;
		this.UpdateText();
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00010FFC File Offset: 0x0000F1FC
	public virtual void PressColorButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[9].active = true;
		this.UpdateText();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00011040 File Offset: 0x0000F240
	public virtual void PressDisplayButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[10].active = true;
		this.UpdateText();
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00011084 File Offset: 0x0000F284
	public virtual void PressAuthenticityButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[11].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x000110C8 File Offset: 0x0000F2C8
	public virtual void PressInputButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[12].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0001110C File Offset: 0x0000F30C
	public virtual void PressCrosshairButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[13].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00011150 File Offset: 0x0000F350
	public virtual void PressCameraButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[14].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00011194 File Offset: 0x0000F394
	public virtual void PressCampaignButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[15].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x000111D8 File Offset: 0x0000F3D8
	public virtual void PressEndlessMenuButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[16].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0001121C File Offset: 0x0000F41C
	public virtual void PressNewGameButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[4].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00011260 File Offset: 0x0000F460
	public virtual void PressSaveLoadButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[7].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x000112A4 File Offset: 0x0000F4A4
	public virtual void continuepress()
	{
		this.LoadThisOnDifficultySelect = 3;
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[8].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x000112EC File Offset: 0x0000F4EC
	public virtual void ep2press()
	{
		this.LoadThisOnDifficultySelect = 22;
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[8].active = true;
		this.UpdateText();
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00011338 File Offset: 0x0000F538
	public virtual void mappress()
	{
		this.LoadThisOnDifficultySelect = 21;
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[8].active = true;
		this.UpdateText();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00011384 File Offset: 0x0000F584
	public virtual void endlesspress()
	{
		this.LoadThisOnDifficultySelect = 15;
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[8].active = true;
		this.UpdateText();
	}

	// Token: 0x060001DB RID: 475 RVA: 0x000113D0 File Offset: 0x0000F5D0
	public virtual void endlesspress2()
	{
		this.LoadThisOnDifficultySelect = 16;
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[8].active = true;
		this.UpdateText();
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0001141C File Offset: 0x0000F61C
	public virtual void loadingscreenornaw(int loadonselect)
	{
		if (loadonselect == 3)
		{
			Application.LoadLevel("LogoScene");
		}
		else if (loadonselect == 21)
		{
			Application.LoadLevel(21);
		}
		else
		{
			Application.LoadLevel("LoadingScene");
		}
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00011460 File Offset: 0x0000F660
	public virtual void DifficultyButton1()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.SaveConfig("difficulty", 0);
		Time.timeScale = (float)1;
		this.SaveConfig("hardcore", this.boolToInt(this.temphardcore));
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = this.LoadThisOnDifficultySelect;
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject)
		{
			((InteractScript)gameObject.GetComponent(typeof(InteractScript))).setscreenstuff();
		}
		this.loadingscreenornaw(this.LoadThisOnDifficultySelect);
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00011544 File Offset: 0x0000F744
	public virtual void DifficultyButton2()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.SaveConfig("difficulty", 1);
		Time.timeScale = (float)1;
		this.SaveConfig("hardcore", this.boolToInt(this.temphardcore));
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = this.LoadThisOnDifficultySelect;
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject)
		{
			((InteractScript)gameObject.GetComponent(typeof(InteractScript))).setscreenstuff();
		}
		this.loadingscreenornaw(this.LoadThisOnDifficultySelect);
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00011628 File Offset: 0x0000F828
	public virtual void DifficultyButton3()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.SaveConfig("difficulty", 2);
		Time.timeScale = (float)1;
		this.SaveConfig("hardcore", this.boolToInt(this.temphardcore));
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = this.LoadThisOnDifficultySelect;
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject)
		{
			((InteractScript)gameObject.GetComponent(typeof(InteractScript))).setscreenstuff();
		}
		this.loadingscreenornaw(this.LoadThisOnDifficultySelect);
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0001170C File Offset: 0x0000F90C
	public virtual void DifficultyButton4()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.SaveConfig("difficulty", 3);
		Time.timeScale = (float)1;
		this.SaveConfig("hardcore", this.boolToInt(this.temphardcore));
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = this.LoadThisOnDifficultySelect;
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject)
		{
			((InteractScript)gameObject.GetComponent(typeof(InteractScript))).setscreenstuff();
		}
		this.loadingscreenornaw(this.LoadThisOnDifficultySelect);
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x000117F0 File Offset: 0x0000F9F0
	public virtual void DifficultyButton5()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.SaveConfig("difficulty", 4);
		Time.timeScale = (float)1;
		this.SaveConfig("hardcore", this.boolToInt(this.temphardcore));
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).leveltoload = this.LoadThisOnDifficultySelect;
		((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).reseteverything();
		GameObject gameObject = GameObject.Find("MainCamera");
		if (gameObject)
		{
			((InteractScript)gameObject.GetComponent(typeof(InteractScript))).setscreenstuff();
		}
		this.loadingscreenornaw(this.LoadThisOnDifficultySelect);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x000118D4 File Offset: 0x0000FAD4
	public virtual void HardcoreButton()
	{
		this.temphardcore = !this.temphardcore;
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00011918 File Offset: 0x0000FB18
	public virtual void exitpress()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		Time.timeScale = (float)1;
		Application.Quit();
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0001194C File Offset: 0x0000FB4C
	public virtual void PressFilterButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("colorfilter"));
		this.SaveConfig("colorfilter", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x000119AC File Offset: 0x0000FBAC
	public virtual void PressSpecButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("lowspec"));
		this.SaveConfig("lowspec", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00011A0C File Offset: 0x0000FC0C
	public virtual void PressBFilterButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("bfilter"));
		this.SaveConfig("bfilter", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x00011A6C File Offset: 0x0000FC6C
	public virtual void PressBloomButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("bloom"));
		this.SaveConfig("bloom", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x00011ACC File Offset: 0x0000FCCC
	public virtual void PressFlaresButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("flares"));
		this.SaveConfig("flares", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x00011B2C File Offset: 0x0000FD2C
	public virtual void PressBuckButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("buck"));
		this.SaveConfig("buck", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00011B8C File Offset: 0x0000FD8C
	public virtual void Press6DGOFButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("6dgofSwimming"));
		this.SaveConfig("6dgofSwimming", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00011BEC File Offset: 0x0000FDEC
	public virtual void PressUnlockAxisButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("unlockedaxis"));
		this.SaveConfig("unlockedaxis", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001EC RID: 492 RVA: 0x00011C4C File Offset: 0x0000FE4C
	public virtual void PressSwayButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("bob"));
		this.SaveConfig("bob", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00011CAC File Offset: 0x0000FEAC
	public virtual void PressRunButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("alwaysrun"));
		this.SaveConfig("alwaysrun", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00011D0C File Offset: 0x0000FF0C
	public virtual void PressCrouchToggle()
	{
		bool b = !this.intToBool(this.LoadConfigInt("crouchtoggle"));
		this.SaveConfig("crouchtoggle", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001EF RID: 495 RVA: 0x00011D6C File Offset: 0x0000FF6C
	public virtual void PressAutoswitchToggle()
	{
		bool b = !this.intToBool(this.LoadConfigInt("autoswitch"));
		this.SaveConfig("autoswitch", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x00011DCC File Offset: 0x0000FFCC
	public virtual void PressMousewheelToggle()
	{
		bool b = !this.intToBool(this.LoadConfigInt("mousewheelflip"));
		this.SaveConfig("mousewheelflip", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00011E2C File Offset: 0x0001002C
	public virtual void PressUIToggle()
	{
		bool b = !this.intToBool(this.LoadConfigInt("uitoggle"));
		this.SaveConfig("uitoggle", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00011E8C File Offset: 0x0001008C
	public virtual void PressInvertButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("inverty"));
		this.SaveConfig("inverty", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00011EEC File Offset: 0x000100EC
	public virtual void PressResolutionButtonRight()
	{
		this.currentresolutionindex++;
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00011F30 File Offset: 0x00010130
	public virtual void PressResolutionButtonLeft()
	{
		this.currentresolutionindex--;
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00011F74 File Offset: 0x00010174
	public virtual void PressResolutionButton()
	{
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		Screen.SetResolution(this.LoadConfigInt("xres"), this.LoadConfigInt("yres"), this.intToBool(this.LoadConfigInt("fullscreen")));
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00011FD4 File Offset: 0x000101D4
	public virtual void PressFullscreenButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("fullscreen"));
		this.SaveConfig("fullscreen", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00012034 File Offset: 0x00010234
	public virtual void PressVsyncButton()
	{
		bool b = !this.intToBool(this.LoadConfigInt("vsync"));
		this.SaveConfig("vsync", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00012094 File Offset: 0x00010294
	public virtual void AdjustSensitivity(float s)
	{
		this.SaveConfig("mousesensitivity", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x000120B4 File Offset: 0x000102B4
	public virtual void AdjustAutoaim(float s)
	{
		this.SaveConfig("autoaim", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FA RID: 506 RVA: 0x000120D4 File Offset: 0x000102D4
	public virtual void PressCrossWeaponToggle()
	{
		bool b = !this.intToBool(this.LoadConfigInt("crossbasedonweapon"));
		this.SaveConfig("crossbasedonweapon", this.boolToInt(b));
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00012134 File Offset: 0x00010334
	public virtual void AdjustCrosshairSize(float s)
	{
		this.SaveConfig("crosshairsize", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00012154 File Offset: 0x00010354
	public virtual void AdjustCrosshair(float s)
	{
		this.SaveConfig("crosshairstyle", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00012174 File Offset: 0x00010374
	public virtual void AdjustCR(float s)
	{
		this.SaveConfig("crtint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x00012194 File Offset: 0x00010394
	public virtual void AdjustCG(float s)
	{
		this.SaveConfig("cgtint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x060001FF RID: 511 RVA: 0x000121B4 File Offset: 0x000103B4
	public virtual void AdjustCB(float s)
	{
		this.SaveConfig("cbtint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000200 RID: 512 RVA: 0x000121D4 File Offset: 0x000103D4
	public virtual void AdjustCA(float s)
	{
		this.SaveConfig("catint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000201 RID: 513 RVA: 0x000121F4 File Offset: 0x000103F4
	public virtual void AdjustBrightness(float s)
	{
		this.SaveConfig("brightness", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00012214 File Offset: 0x00010414
	public virtual void AdjustSaturation(float s)
	{
		this.SaveConfig("saturation", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00012234 File Offset: 0x00010434
	public virtual void AdjustContrast(float s)
	{
		this.SaveConfig("contrast", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00012254 File Offset: 0x00010454
	public virtual void AdjustHue(float s)
	{
		this.SaveConfig("hue", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00012274 File Offset: 0x00010474
	public virtual void AdjustR(float s)
	{
		this.SaveConfig("rtint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00012294 File Offset: 0x00010494
	public virtual void AdjustG(float s)
	{
		this.SaveConfig("gtint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000207 RID: 519 RVA: 0x000122B4 File Offset: 0x000104B4
	public virtual void AdjustB(float s)
	{
		this.SaveConfig("btint", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000208 RID: 520 RVA: 0x000122D4 File Offset: 0x000104D4
	public virtual void AdjustPixel(float s)
	{
		this.SaveConfig("pixel", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x000122F4 File Offset: 0x000104F4
	public virtual void AdjustFov(float s)
	{
		this.SaveConfig("fov", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00012314 File Offset: 0x00010514
	public virtual void AdjustFramerate(float s)
	{
		if (s == (float)241)
		{
			s = (float)-1;
		}
		this.SaveConfig("framelock", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00012348 File Offset: 0x00010548
	public virtual void AdjustWeaponSize(float s)
	{
		this.SaveConfig("weaponsize", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x0600020C RID: 524 RVA: 0x00012368 File Offset: 0x00010568
	public virtual void AdjustMasterAudio(float s)
	{
		this.SaveConfig("mastervolume", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00012388 File Offset: 0x00010588
	public virtual void AdjustMusic(float s)
	{
		this.SaveConfig("musicvolume", s);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x0600020E RID: 526 RVA: 0x000123A8 File Offset: 0x000105A8
	public virtual void PressBackMainButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[0].active = true;
		this.UpdateText();
	}

	// Token: 0x0600020F RID: 527 RVA: 0x000123EC File Offset: 0x000105EC
	public virtual void PressBackNewGameButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[4].active = true;
		this.UpdateText();
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00012430 File Offset: 0x00010630
	public virtual void PressBackSettingsButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[1].active = true;
		this.UpdateText();
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00012474 File Offset: 0x00010674
	public virtual void PressBackVideoButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[2].active = true;
		this.currentresolutionindex = this.findresolutionindex(Screen.width, Screen.height);
		this.UpdateText();
	}

	// Token: 0x06000212 RID: 530 RVA: 0x000124CC File Offset: 0x000106CC
	public virtual void PressBackGameButton()
	{
		this.SetAllInactive();
		((AudioSource)this.GetComponent(typeof(AudioSource))).Play();
		this.menus[3].active = true;
		this.UpdateText();
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00012510 File Offset: 0x00010710
	public virtual void DifficultyDropdown(int i)
	{
		this.SaveConfig("difficulty", i);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00012530 File Offset: 0x00010730
	public virtual void ColorDropdown(int i)
	{
		this.SetColorPresets(i);
		this.UpdateSettings();
		this.UpdateText();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00012548 File Offset: 0x00010748
	public virtual void savegame()
	{
		SaveManagerScript saveManagerScript = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		InputField inputField = (InputField)GameObject.Find("FilenameText").GetComponent(typeof(InputField));
		if (inputField.text != string.Empty)
		{
			saveManagerScript.filename = Application.dataPath + "/../saves/" + inputField.text;
			saveManagerScript.menusave = true;
			saveManagerScript.quicksave();
			((CreateFileNamesScript)GameObject.Find("DasMenu").GetComponent(typeof(CreateFileNamesScript))).updatebuttons();
		}
	}

	// Token: 0x06000216 RID: 534 RVA: 0x000125F8 File Offset: 0x000107F8
	public virtual void loadgame()
	{
		SaveManagerScript saveManagerScript = (SaveManagerScript)GameObject.Find("SaveManager").GetComponent(typeof(SaveManagerScript));
		InputField inputField = (InputField)GameObject.Find("FilenameText").GetComponent(typeof(InputField));
		if (inputField.text != string.Empty)
		{
			saveManagerScript.filename = Application.dataPath + "/../saves/" + inputField.text;
			((InGameMenuScript)this.GetComponent(typeof(InGameMenuScript))).togglemenu();
			saveManagerScript.quickload(true);
		}
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0001269C File Offset: 0x0001089C
	public virtual void SetColorPresets(int i)
	{
		if (i == 0)
		{
		}
		if (i == 1)
		{
			this.SaveConfig("saturation", 0);
			this.SaveConfig("contrast", 0);
			this.SaveConfig("brightness", 0);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 6)
		{
			this.SaveConfig("saturation", -0.6f);
			this.SaveConfig("contrast", -0.1f);
			this.SaveConfig("brightness", 0.3f);
			this.SaveConfig("rtint", 228);
			this.SaveConfig("gtint", 230);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 21);
		}
		if (i == 4)
		{
			this.SaveConfig("saturation", 0.6f);
			this.SaveConfig("contrast", (float)0);
			this.SaveConfig("brightness", 0.4f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 2)
		{
			this.SaveConfig("saturation", -1f);
			this.SaveConfig("contrast", 0.2f);
			this.SaveConfig("brightness", 0.5f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 12)
		{
			this.SaveConfig("saturation", -1f);
			this.SaveConfig("contrast", 0.5f);
			this.SaveConfig("brightness", 3);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 13)
		{
			this.SaveConfig("saturation", 1.3f);
			this.SaveConfig("contrast", 0.4f);
			this.SaveConfig("brightness", 2.4f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 20);
		}
		if (i == 8)
		{
			this.SaveConfig("saturation", -0.6f);
			this.SaveConfig("contrast", 0.3f);
			this.SaveConfig("brightness", 1.5f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 9)
		{
			this.SaveConfig("saturation", -0.7f);
			this.SaveConfig("contrast", 0);
			this.SaveConfig("brightness", 0.4f);
			this.SaveConfig("rtint", 231);
			this.SaveConfig("gtint", 192);
			this.SaveConfig("btint", 143);
			this.SaveConfig("hue", 332);
		}
		if (i == 10)
		{
			this.SaveConfig("saturation", -0.5f);
			this.SaveConfig("contrast", 0.2f);
			this.SaveConfig("brightness", 0.5f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 212);
			this.SaveConfig("btint", 60);
			this.SaveConfig("hue", 311);
		}
		if (i == 11)
		{
			this.SaveConfig("saturation", -1f);
			this.SaveConfig("contrast", 0.7f);
			this.SaveConfig("brightness", 2.9f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 112);
			this.SaveConfig("btint", 0);
			this.SaveConfig("hue", 0);
		}
		if (i == 5)
		{
			this.SaveConfig("saturation", -0.7f);
			this.SaveConfig("contrast", -0.1f);
			this.SaveConfig("brightness", 0.1f);
			this.SaveConfig("rtint", 237);
			this.SaveConfig("gtint", 236);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 0);
		}
		if (i == 7)
		{
			this.SaveConfig("saturation", 1.2f);
			this.SaveConfig("contrast", -0.1f);
			this.SaveConfig("brightness", 0.2f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 0);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 157);
		}
		if (i == 3)
		{
			this.SaveConfig("saturation", -1f);
			this.SaveConfig("contrast", 0.1f);
			this.SaveConfig("brightness", 0.5f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 179);
			this.SaveConfig("btint", 76);
			this.SaveConfig("hue", 0);
		}
		if (i == 14)
		{
			this.SaveConfig("saturation", -1f);
			this.SaveConfig("contrast", 0.3f);
			this.SaveConfig("brightness", 4f);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 0);
			this.SaveConfig("btint", 0);
			this.SaveConfig("hue", 0);
		}
		if (i == 15)
		{
			this.SaveConfig("saturation", (float)0);
			this.SaveConfig("contrast", (float)0);
			this.SaveConfig("brightness", (float)0);
			this.SaveConfig("rtint", 255);
			this.SaveConfig("gtint", 255);
			this.SaveConfig("btint", 255);
			this.SaveConfig("hue", 180);
		}
		((Dropdown)GameObject.Find("ColorDropdown").GetComponent(typeof(Dropdown))).value = 0;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00012F80 File Offset: 0x00011180
	public virtual void UpdateSettings()
	{
		int targetFrameRate = this.LoadConfigInt("framelock");
		Application.targetFrameRate = targetFrameRate;
		Screen.fullScreen = this.intToBool(this.LoadConfigInt("fullscreen"));
		QualitySettings.vSyncCount = this.LoadConfigInt("vsync");
		((CrosshairStyleScript)this.crosshair.GetComponent(typeof(CrosshairStyleScript))).mystyle = this.LoadConfigInt("crosshairstyle");
		((CrosshairColorScript)this.crosshair.GetComponent(typeof(CrosshairColorScript))).mycolor = new Color(this.LoadConfigFloat("crtint") / (float)255, this.LoadConfigFloat("cgtint") / (float)255, this.LoadConfigFloat("cbtint") / (float)255, this.LoadConfigFloat("catint") / (float)255);
		((CrosshairBasedOnWeaponScript)this.crosshair.GetComponent(typeof(CrosshairBasedOnWeaponScript))).crosshairbasedonweapon = this.intToBool(this.LoadConfigInt("crossbasedonweapon"));
		((CrosshairSizeScript)this.crosshair.GetComponent(typeof(CrosshairSizeScript))).menusize = this.LoadConfigFloat("crosshairsize");
		if (GameObject.Find("HUDObjects"))
		{
			ToggleUIScript toggleUIScript = (ToggleUIScript)GameObject.Find("HUDObjects").GetComponent(typeof(ToggleUIScript));
			toggleUIScript.currentlyvisible = this.intToBool(this.LoadConfigInt("uitoggle"));
			toggleUIScript.hidestuff(toggleUIScript.currentlyvisible);
		}
		if (GameObject.Find("PERSIST"))
		{
			((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).hardcore = this.intToBool(this.LoadConfigInt("hardcore"));
			((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).mavolume = this.LoadConfigFloat("mastervolume");
			((PersistScript)GameObject.Find("PERSIST").GetComponent(typeof(PersistScript))).muvolume = this.LoadConfigFloat("musicvolume");
		}
		if (this.phand)
		{
			this.phand.transform.localScale = new Vector3(this.LoadConfigFloat("weaponsize"), this.LoadConfigFloat("weaponsize"), this.LoadConfigFloat("weaponsize"));
			((WeaponScaleScript)this.phand.GetComponent(typeof(WeaponScaleScript))).size = this.LoadConfigFloat("weaponsize");
		}
		if (this.mplayer)
		{
			((MusicPlayerScript)this.mplayer.GetComponent(typeof(MusicPlayerScript))).mastermusicvolume = this.LoadConfigFloat("musicvolume");
		}
		if (this.wcam)
		{
			((OldSchoolPixelFX)this.wcam.GetComponent(typeof(OldSchoolPixelFX))).width = (int)((float)Screen.width / this.LoadConfigFloat("pixel"));
			((OldSchoolPixelFX)this.wcam.GetComponent(typeof(OldSchoolPixelFX))).height = (int)((float)Screen.height / this.LoadConfigFloat("pixel"));
			((ColorCorrectionRamp)this.wcam.GetComponent(typeof(ColorCorrectionRamp))).enabled = this.intToBool(this.LoadConfigInt("colorfilter"));
			((BloomAndFlares)this.wcam.GetComponent(typeof(BloomAndFlares))).enabled = this.intToBool(this.LoadConfigInt("bloom"));
			if ((ZoomScript)this.wcam.GetComponent(typeof(ZoomScript)))
			{
				((ZoomScript)this.wcam.GetComponent(typeof(ZoomScript))).normalsensitivity = this.LoadConfigFloat("mousesensitivity");
			}
			if (this.wcam.GetComponent("SimpleLUT"))
			{
				RuntimeServices.SetProperty(this.wcam.GetComponent("SimpleLUT"), "Saturation", this.LoadConfigFloat("saturation"));
				RuntimeServices.SetProperty(this.wcam.GetComponent("SimpleLUT"), "Brightness", this.LoadConfigFloat("brightness"));
				RuntimeServices.SetProperty(this.wcam.GetComponent("SimpleLUT"), "Contrast", this.LoadConfigFloat("contrast"));
				RuntimeServices.SetProperty(this.wcam.GetComponent("SimpleLUT"), "Hue", this.LoadConfigFloat("hue"));
				RuntimeServices.SetProperty(this.wcam.GetComponent("SimpleLUT"), "TintColor", new Color(this.LoadConfigFloat("rtint") / (float)255, this.LoadConfigFloat("gtint") / (float)255, this.LoadConfigFloat("btint") / (float)255, (float)1));
			}
		}
		if (this.mcam)
		{
			((FlareLayer)this.mcam.GetComponent(typeof(FlareLayer))).enabled = this.intToBool(this.LoadConfigInt("flares"));
			((MyMouseLook)this.mcam.GetComponent(typeof(MyMouseLook))).disablebuck = !this.intToBool(this.LoadConfigInt("buck"));
			((MyMouseLook)this.mcam.GetComponent(typeof(MyMouseLook))).dobob = this.intToBool(this.LoadConfigInt("bob"));
			((MyMouseLook)this.mcam.GetComponent(typeof(MyMouseLook))).inverted = this.intToBool(this.LoadConfigInt("inverty"));
			((MyMouseLook)this.mcam.GetComponent(typeof(MyMouseLook))).disablefreeaxis = !this.intToBool(this.LoadConfigInt("unlockedaxis"));
			((ZoomScript)this.mcam.GetComponent(typeof(ZoomScript))).normalsensitivity = this.LoadConfigFloat("mousesensitivity");
			((MyMouseLook)this.mcam.GetComponent(typeof(MyMouseLook))).lookSensitivity = this.LoadConfigFloat("mousesensitivity");
			((HorFovScript)this.mcam.GetComponent(typeof(HorFovScript))).horfov = this.LoadConfigFloat("fov");
			AudioListener.volume = this.LoadConfigFloat("mastervolume");
			if (this.mcam)
			{
				((HorFovScript)this.mcam.GetComponent(typeof(HorFovScript))).setcamfov();
			}
		}
		if (this.wan)
		{
			((SelectionScript)this.wan.GetComponent(typeof(SelectionScript))).flipwheel = this.intToBool(this.LoadConfigInt("mousewheelflip"));
		}
		if (this.fswitch)
		{
			((FilteringSwitcherScript)this.fswitch.GetComponent(typeof(FilteringSwitcherScript))).switching();
			((LowSpecModeScript)this.fswitch.GetComponent(typeof(LowSpecModeScript))).dolowspecmode = this.intToBool(this.LoadConfigInt("lowspec"));
			((LowSpecModeScript)this.fswitch.GetComponent(typeof(LowSpecModeScript))).updatemode();
		}
		if (this.plr)
		{
			((MyControllerScript)this.plr.GetComponent(typeof(MyControllerScript))).runtoggle = this.intToBool(this.LoadConfigInt("alwaysrun"));
			((MyControllerScript)this.plr.GetComponent(typeof(MyControllerScript))).crouchtoggle = this.intToBool(this.LoadConfigInt("crouchtoggle"));
			((PlayerPickupScript)this.plr.GetComponent(typeof(PlayerPickupScript))).doautoswitch = this.intToBool(this.LoadConfigInt("autoswitch"));
			((MyControllerScript)this.plr.GetComponent(typeof(MyControllerScript))).SixDegreesOfFreedomSwimming = this.intToBool(this.LoadConfigInt("6dgofSwimming"));
		}
		if (this.phand)
		{
			((LockOnScript)GameObject.Find("LockonAimer").GetComponent(typeof(LockOnScript))).lockonamount = this.LoadConfigFloat("autoaim");
			if (this.LoadConfigFloat("autoaim") <= (float)0)
			{
				((LockOnScript)GameObject.Find("LockonAimer").GetComponent(typeof(LockOnScript))).lockondist = (float)0;
			}
			else
			{
				((LockOnScript)GameObject.Find("LockonAimer").GetComponent(typeof(LockOnScript))).lockondist = (float)10000;
			}
			Component[] components = this.phand.GetComponents(typeof(Headbobber));
			int i = 0;
			Component[] array = components;
			int length = array.Length;
			while (i < length)
			{
				((Headbobber)array[i]).enabled = this.intToBool(this.LoadConfigInt("bob"));
				i++;
			}
		}
		if (this.stats)
		{
			((StatScript)this.stats.GetComponent(typeof(StatScript))).difficulty = this.LoadConfigInt("difficulty");
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00013928 File Offset: 0x00011B28
	public virtual void UpdateText()
	{
		((CreateFileNamesScript)GameObject.Find("DasMenu").GetComponent(typeof(CreateFileNamesScript))).updatebuttons();
		if (GameObject.Find("DIFFICULTYMENU"))
		{
			((Text)GameObject.Find("HardcoreText").GetComponent(typeof(Text))).text = "INTRUDER MODE: " + this.intToString(this.boolToInt(this.temphardcore));
		}
		if (GameObject.Find("COLORSETTINGSMENU"))
		{
			((Slider)GameObject.Find("BrightnessSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("brightness");
			((Text)GameObject.Find("BrightnessText").GetComponent(typeof(Text))).text = "Brightness: " + this.LoadConfigFloat("brightness").ToString("0.0");
			((Slider)GameObject.Find("SaturationSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("saturation");
			((Text)GameObject.Find("SaturationText").GetComponent(typeof(Text))).text = "Saturation: " + this.LoadConfigFloat("saturation").ToString("0.0");
			((Slider)GameObject.Find("ContrastSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("contrast");
			((Text)GameObject.Find("ContrastText").GetComponent(typeof(Text))).text = "Contrast: " + this.LoadConfigFloat("contrast").ToString("0.0");
			((Slider)GameObject.Find("HueSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("hue");
			((Text)GameObject.Find("HueText").GetComponent(typeof(Text))).text = "Hue: " + this.LoadConfigFloat("hue").ToString("000");
			((Slider)GameObject.Find("RSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("rtint");
			((Text)GameObject.Find("RText").GetComponent(typeof(Text))).text = "Red Tint: " + this.LoadConfigFloat("rtint").ToString("000");
			((Slider)GameObject.Find("GSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("gtint");
			((Text)GameObject.Find("GText").GetComponent(typeof(Text))).text = "Green Tint: " + this.LoadConfigFloat("gtint").ToString("000");
			((Slider)GameObject.Find("BSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("btint");
			((Text)GameObject.Find("BText").GetComponent(typeof(Text))).text = "Blue Tint: " + this.LoadConfigFloat("btint").ToString("000");
		}
		if (GameObject.Find("DISPLAYSETTINGSMENU"))
		{
			((Slider)GameObject.Find("FovSlider").GetComponent(typeof(Slider))).value = (float)this.LoadConfigInt("fov");
			((Text)GameObject.Find("FovText").GetComponent(typeof(Text))).text = "Fov: " + this.LoadConfigInt("fov").ToString("000");
			if (this.LoadConfigInt("fov") >= 150)
			{
				((Text)GameObject.Find("FovText").GetComponent(typeof(Text))).text = "Fov: Cynical";
			}
			int num = this.LoadConfigInt("framelock");
			if (num == -1)
			{
				num = 241;
			}
			((Slider)GameObject.Find("FramerateSlider").GetComponent(typeof(Slider))).value = (float)num;
			((Text)GameObject.Find("FramerateText").GetComponent(typeof(Text))).text = "Framerate Cap: " + num.ToString("000");
			if (num == 241)
			{
				((Text)GameObject.Find("FramerateText").GetComponent(typeof(Text))).text = "Framerate Cap: Uncapped";
			}
			if (num == 24)
			{
				((Text)GameObject.Find("FramerateText").GetComponent(typeof(Text))).text = "Framerate Cap: Cinematic";
			}
			Resolution[] resolutions = Screen.resolutions;
			if (this.currentresolutionindex >= resolutions.Length)
			{
				this.currentresolutionindex = 0;
			}
			if (this.currentresolutionindex < 0)
			{
				this.currentresolutionindex = resolutions.Length - 1;
			}
			((Text)GameObject.Find("ResolutionDisplayText").GetComponent(typeof(Text))).text = resolutions[this.currentresolutionindex].width.ToString() + "x" + resolutions[this.currentresolutionindex].height.ToString();
			this.SaveConfig("xres", resolutions[this.currentresolutionindex].width);
			this.SaveConfig("yres", resolutions[this.currentresolutionindex].height);
			((Text)GameObject.Find("FullscreenToggleText").GetComponent(typeof(Text))).text = "Fullscreen: " + this.intToString(this.LoadConfigInt("fullscreen"));
			((Text)GameObject.Find("VsyncToggleText").GetComponent(typeof(Text))).text = "Vsync: " + this.intToString(this.LoadConfigInt("vsync"));
		}
		if (GameObject.Find("AUTHENTICITYSETTINGSMENU"))
		{
			((Text)GameObject.Find("BFilterText").GetComponent(typeof(Text))).text = "Bilinear Filtering: " + this.intToString(this.LoadConfigInt("bfilter"));
			((Text)GameObject.Find("BloomText").GetComponent(typeof(Text))).text = "Light Bloom: " + this.intToString(this.LoadConfigInt("bloom"));
			((Text)GameObject.Find("FlaresText").GetComponent(typeof(Text))).text = "Light Flares: " + this.intToString(this.LoadConfigInt("flares"));
			((Slider)GameObject.Find("PixelSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("pixel");
			((Text)GameObject.Find("PixelText").GetComponent(typeof(Text))).text = "Pixelization: " + this.LoadConfigFloat("pixel").ToString("0");
			if (this.LoadConfigFloat("pixel") == (float)1)
			{
				((Text)GameObject.Find("PixelText").GetComponent(typeof(Text))).text = "Pixelization: Off";
			}
			((Text)GameObject.Find("ColorFilterText").GetComponent(typeof(Text))).text = "Limited Palette: " + this.intToString(this.LoadConfigInt("colorfilter"));
			((Text)GameObject.Find("LowSpecText").GetComponent(typeof(Text))).text = "Low Spec Mode: " + this.intToString(this.LoadConfigInt("lowspec"));
		}
		if (GameObject.Find("AUDIOSETTINGSMENU"))
		{
			((Slider)GameObject.Find("MainAudioSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("mastervolume");
			((Text)GameObject.Find("MainAudioText").GetComponent(typeof(Text))).text = "Master Volume: " + (this.LoadConfigFloat("mastervolume") * (float)100).ToString("000");
			((Slider)GameObject.Find("MusicVolumeSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("musicvolume");
			((Text)GameObject.Find("MusicVolumeText").GetComponent(typeof(Text))).text = "Music Volume: " + (this.LoadConfigFloat("musicvolume") * (float)100).ToString("000");
		}
		if (GameObject.Find("INPUTSETTINGSMENU"))
		{
			((Text)GameObject.Find("RunToggleText").GetComponent(typeof(Text))).text = "Always Run: " + this.intToString(this.LoadConfigInt("alwaysrun"));
			((Text)GameObject.Find("MouseInvertText").GetComponent(typeof(Text))).text = "Invert Y Axis: " + this.intToString(this.LoadConfigInt("inverty"));
			((Text)GameObject.Find("CrouchToggleText").GetComponent(typeof(Text))).text = "Toggle Crouch: " + this.intToString(this.LoadConfigInt("crouchtoggle"));
			((Slider)GameObject.Find("MouseSensitivitySlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("mousesensitivity");
			((Slider)GameObject.Find("AutoaimSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("autoaim");
			((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: " + this.LoadConfigFloat("autoaim");
			if (this.LoadConfigFloat("autoaim") == (float)0)
			{
				((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: Off";
			}
			if (this.LoadConfigFloat("autoaim") == (float)1)
			{
				((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: Just a Bit";
			}
			if (this.LoadConfigFloat("autoaim") == (float)2)
			{
				((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: Console Gamer";
			}
			if (this.LoadConfigFloat("autoaim") == (float)3)
			{
				((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: Keyboard Look";
			}
			if (this.LoadConfigFloat("autoaim") == (float)4)
			{
				((Text)GameObject.Find("AutoaimText").GetComponent(typeof(Text))).text = "Autoaim Amount: Shameful";
			}
			((Text)GameObject.Find("AutoswitchText").GetComponent(typeof(Text))).text = "Autoswitch Weapon: " + this.intToString(this.LoadConfigInt("autoswitch"));
			((Text)GameObject.Find("MouseWheelFlipText").GetComponent(typeof(Text))).text = "Flip mousewheel: " + this.intToString(this.LoadConfigInt("mousewheelflip"));
		}
		if (GameObject.Find("CROSSHAIRSETTINGSMENU"))
		{
			((Slider)GameObject.Find("CrosshairSlider").GetComponent(typeof(Slider))).value = (float)this.LoadConfigInt("crosshairstyle");
			((Text)GameObject.Find("CrosshairText").GetComponent(typeof(Text))).text = "Crosshair: " + (this.LoadConfigInt("crosshairstyle") + 1);
			((Image)GameObject.Find("CrosshairImage").GetComponent(typeof(Image))).sprite = ((CrosshairStyleScript)this.crosshair.GetComponent(typeof(CrosshairStyleScript))).crosshairarray[this.LoadConfigInt("crosshairstyle")];
			((Image)GameObject.Find("CrosshairImage").GetComponent(typeof(Image))).color = ((CrosshairColorScript)this.crosshair.GetComponent(typeof(CrosshairColorScript))).mycolor;
			((Slider)GameObject.Find("CRSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("crtint");
			((Text)GameObject.Find("CRText").GetComponent(typeof(Text))).text = "Crosshair Red Tint: " + this.LoadConfigFloat("crtint").ToString("000");
			((Slider)GameObject.Find("CGSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("cgtint");
			((Text)GameObject.Find("CGText").GetComponent(typeof(Text))).text = "Crosshair Green Tint: " + this.LoadConfigFloat("cgtint").ToString("000");
			((Slider)GameObject.Find("CBSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("cbtint");
			((Text)GameObject.Find("CBText").GetComponent(typeof(Text))).text = "Crosshair Blue Tint: " + this.LoadConfigFloat("cbtint").ToString("000");
			((Slider)GameObject.Find("CASlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("catint");
			((Text)GameObject.Find("CAText").GetComponent(typeof(Text))).text = "Crosshair Alpha: " + this.LoadConfigFloat("catint").ToString("000");
			((Text)GameObject.Find("WeaponCrosshairToggleText").GetComponent(typeof(Text))).text = "Crosshair based on weapon: " + this.intToString(this.LoadConfigInt("crossbasedonweapon"));
			((Text)GameObject.Find("CrosshairSizeText").GetComponent(typeof(Text))).text = "Crosshair Size: " + this.LoadConfigFloat("crosshairsize").ToString("0.0");
			((Slider)GameObject.Find("CrosshairSizeSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("crosshairsize");
			float num2 = ((ScreenSizeScript)this.plr.GetComponent(typeof(ScreenSizeScript))).currentsize * ((CrosshairSizeScript)this.crosshair.GetComponent(typeof(CrosshairSizeScript))).menusize;
			GameObject.Find("CrosshairImage").transform.localScale = new Vector3(num2, num2, num2);
		}
		if (GameObject.Find("CAMERASETTINGSMENU"))
		{
			((Text)GameObject.Find("BuckText").GetComponent(typeof(Text))).text = "Camera Animations: " + this.intToString(this.LoadConfigInt("buck"));
			((Text)GameObject.Find("SwayText").GetComponent(typeof(Text))).text = "Weapon Sway: " + this.intToString(this.LoadConfigInt("bob"));
			((Slider)GameObject.Find("WeaponSizeSlider").GetComponent(typeof(Slider))).value = this.LoadConfigFloat("weaponsize");
			((Text)GameObject.Find("WeaponSizeText").GetComponent(typeof(Text))).text = "Weapon Model: " + this.LoadConfigFloat("weaponsize").ToString("0.0");
			if (this.LoadConfigFloat("weaponsize") == (float)0)
			{
				((Text)GameObject.Find("WeaponSizeText").GetComponent(typeof(Text))).text = "Weapon Model: (off)";
			}
			((Text)GameObject.Find("6DGOFText").GetComponent(typeof(Text))).text = "6DOF Water Controls: " + this.intToString(this.LoadConfigInt("6dgofSwimming"));
			((Text)GameObject.Find("FreeAxisText").GetComponent(typeof(Text))).text = "Unlocked Vertical Look: " + this.intToString(this.LoadConfigInt("unlockedaxis"));
		}
		if (GameObject.Find("GAMESETTINGSMENU"))
		{
			((Dropdown)GameObject.Find("DifficultyDropdown").GetComponent(typeof(Dropdown))).value = this.LoadConfigInt("difficulty");
		}
		if (GameObject.Find("Player"))
		{
			((ScreenSizeScript)GameObject.Find("Player").GetComponent(typeof(ScreenSizeScript))).sethudstuff();
		}
	}

	// Token: 0x0600021A RID: 538 RVA: 0x00014C0C File Offset: 0x00012E0C
	public virtual void SetDefaults()
	{
		PlayerPrefs.SetInt("settingsexist6_2", 1);
		PlayerPrefs.SetInt("pixelfilter", 0);
		PlayerPrefs.SetInt("colorfilter", 1);
		PlayerPrefs.SetInt("bfilter", 0);
		PlayerPrefs.SetInt("bloom", 0);
		PlayerPrefs.SetInt("flares", 1);
		PlayerPrefs.SetInt("buck", 1);
		PlayerPrefs.SetInt("bob", 1);
		PlayerPrefs.SetInt("alwaysrun", 1);
		PlayerPrefs.SetInt("inverty", 0);
		PlayerPrefs.SetFloat("mousesensitivity", 0.5f);
		PlayerPrefs.SetFloat("saturation", (float)0);
		PlayerPrefs.SetFloat("brightness", (float)0);
		PlayerPrefs.SetFloat("contrast", (float)0);
		PlayerPrefs.SetFloat("hue", (float)0);
		PlayerPrefs.SetFloat("rtint", (float)255);
		PlayerPrefs.SetFloat("gtint", (float)255);
		PlayerPrefs.SetFloat("btint", (float)255);
		PlayerPrefs.SetFloat("fov", (float)120);
		PlayerPrefs.SetFloat("pixel", (float)1);
		PlayerPrefs.SetInt("difficulty", 3);
		PlayerPrefs.SetInt("crouchtoggle", 1);
		PlayerPrefs.SetInt("fullscreen", 1);
		PlayerPrefs.SetInt("vsync", 0);
		PlayerPrefs.SetInt("xres", Screen.currentResolution.width);
		PlayerPrefs.SetInt("yres", Screen.currentResolution.height);
		PlayerPrefs.SetFloat("crtint", (float)0);
		PlayerPrefs.SetFloat("cgtint", (float)255);
		PlayerPrefs.SetFloat("cbtint", (float)160);
		PlayerPrefs.SetFloat("catint", (float)154);
		PlayerPrefs.SetInt("crosshairstyle", 0);
		PlayerPrefs.SetInt("uitoggle", 1);
		PlayerPrefs.SetFloat("mastervolume", (float)1);
		PlayerPrefs.SetFloat("musicvolume", 0.5f);
		PlayerPrefs.SetFloat("autoaim", (float)0);
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00014DE4 File Offset: 0x00012FE4
	public virtual void SetAllInactive()
	{
		for (int i = 0; i < this.menus.Length; i++)
		{
			this.menus[i].active = false;
		}
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00014E1C File Offset: 0x0001301C
	public virtual void SetBasedOnPreset()
	{
		if (PlayerPrefs.GetInt("didpreset") != 1)
		{
			int qualityLevel = QualitySettings.GetQualityLevel();
			if (qualityLevel == 0)
			{
				PlayerPrefs.SetInt("pixelfilter", 1);
				PlayerPrefs.SetInt("colorfilter", 1);
				PlayerPrefs.SetInt("bfilter", 0);
				PlayerPrefs.SetInt("bloom", 0);
				PlayerPrefs.SetInt("flares", 0);
			}
			if (qualityLevel == 1)
			{
				PlayerPrefs.SetInt("pixelfilter", 0);
				PlayerPrefs.SetInt("colorfilter", 0);
				PlayerPrefs.SetInt("bfilter", 0);
				PlayerPrefs.SetInt("bloom", 0);
				PlayerPrefs.SetInt("flares", 1);
			}
			if (qualityLevel == 2)
			{
				PlayerPrefs.SetInt("pixelfilter", 0);
				PlayerPrefs.SetInt("colorfilter", 0);
				PlayerPrefs.SetInt("bfilter", 1);
				PlayerPrefs.SetInt("bloom", 0);
				PlayerPrefs.SetInt("flares", 1);
			}
		}
		PlayerPrefs.SetInt("didpreset", 1);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00014F04 File Offset: 0x00013104
	public virtual bool intToBool(int i)
	{
		return i != 0;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00014F18 File Offset: 0x00013118
	public virtual int boolToInt(bool b)
	{
		return (!b) ? 0 : 1;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00014F2C File Offset: 0x0001312C
	public virtual string intToString(int i)
	{
		return (i != 0) ? "On" : "Off";
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00014F48 File Offset: 0x00013148
	public virtual void SaveConfig(string key, object contents)
	{
		string value = contents.ToString();
		this.data["SETTINGS"][key] = value;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00014F74 File Offset: 0x00013174
	public virtual string LoadConfigString(string key)
	{
		string text = Application.dataPath + "/config/dusk.ini";
		return this.data["SETTINGS"][key];
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00014FAC File Offset: 0x000131AC
	public virtual float LoadConfigFloat(string key)
	{
		string text = Application.dataPath + "/config/dusk.ini";
		string s = this.data["SETTINGS"][key];
		return float.Parse(s);
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00014FE8 File Offset: 0x000131E8
	public virtual int LoadConfigInt(string key)
	{
		string text = Application.dataPath + "/config/dusk.ini";
		string s = this.data["SETTINGS"][key];
		return int.Parse(s);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00015024 File Offset: 0x00013224
	public virtual void dosavestuff()
	{
		string rhs = null;
		if (this.sav.dosave || this.sav.doload)
		{
			rhs = "?tag=" + this.transform.gameObject.name + this.origcoors.ToString();
		}
		if (this.sav.dosave)
		{
			ES2.Save<int>(this.LoadConfigInt("difficulty"), this.sav.filename + rhs + "d1ff1culty");
			ES2.Save<int>(this.LoadConfigInt("hardcore"), this.sav.filename + rhs + "h4rdc0r3");
			this.UpdateSettings();
			this.UpdateText();
		}
		if (this.sav.doload)
		{
			if (ES2.Exists(this.sav.filename + rhs + "d1ff1culty"))
			{
				this.SaveConfig("difficulty", ES2.Load<int>(this.sav.filename + rhs + "d1ff1culty"));
				this.SaveConfig("hardcore", ES2.Load<int>(this.sav.filename + rhs + "h4rdc0r3"));
			}
			this.UpdateSettings();
			this.UpdateText();
		}
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00015194 File Offset: 0x00013394
	public virtual int findresolutionindex(int x, int y)
	{
		Resolution[] resolutions = Screen.resolutions;
		for (int i = 0; i < resolutions.Length; i++)
		{
			if (resolutions[i].width == x && resolutions[i].height == y)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000151EC File Offset: 0x000133EC
	public virtual void Main()
	{
	}

	// Token: 0x0400027A RID: 634
	public GameObject[] menus;

	// Token: 0x0400027B RID: 635
	public GameObject logo;

	// Token: 0x0400027C RID: 636
	public GameObject back;

	// Token: 0x0400027D RID: 637
	public GameObject crosshair;

	// Token: 0x0400027E RID: 638
	public GameObject crosshairslider;

	// Token: 0x0400027F RID: 639
	public GameObject lookatoptions;

	// Token: 0x04000280 RID: 640
	[HideInInspector]
	public GameObject wcam;

	// Token: 0x04000281 RID: 641
	[HideInInspector]
	public GameObject fswitch;

	// Token: 0x04000282 RID: 642
	[HideInInspector]
	public GameObject mcam;

	// Token: 0x04000283 RID: 643
	[HideInInspector]
	public GameObject phand;

	// Token: 0x04000284 RID: 644
	[HideInInspector]
	public GameObject plr;

	// Token: 0x04000285 RID: 645
	[HideInInspector]
	public GameObject stats;

	// Token: 0x04000286 RID: 646
	[HideInInspector]
	public int currentresolutionindex;

	// Token: 0x04000287 RID: 647
	[HideInInspector]
	public GameObject mplayer;

	// Token: 0x04000288 RID: 648
	[HideInInspector]
	public bool temphardcore;

	// Token: 0x04000289 RID: 649
	public FileIniDataParser parser;

	// Token: 0x0400028A RID: 650
	public string filename;

	// Token: 0x0400028B RID: 651
	public IniData data;

	// Token: 0x0400028C RID: 652
	[HideInInspector]
	public SaveManagerScript sav;

	// Token: 0x0400028D RID: 653
	[HideInInspector]
	public Vector3 origcoors;

	// Token: 0x0400028E RID: 654
	[HideInInspector]
	public int LoadThisOnDifficultySelect;

	// Token: 0x0400028F RID: 655
	[HideInInspector]
	public GameObject wan;

	// Token: 0x04000290 RID: 656
	[HideInInspector]
	public GameObject dasmenu;

	// Token: 0x04000291 RID: 657
	[HideInInspector]
	public bool intro;

	// Token: 0x04000292 RID: 658
	[HideInInspector]
	public float introdelaything;
}
