using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    // Maximum ammo this weapon can have
    public int maxAmmo;

    //current ammo this weapon has
    public int ammo;

    // Is this weapon picked up by player ?
    public bool pickedUp;

    // Is this weapon equipped ?
    public bool equipped;

    // Tracer prefabs
    public GameObject tracerobject;

    // Token: 0x04000052 RID: 82
    public LayerMask hitlayers;

    // Token: 0x040000B2 RID: 178
    public GameObject bloodarcs;

    // Token: 0x040000B3 RID: 179
    public Color defaultparticlecolor;

    // Token: 0x040000B4 RID: 180
    public AudioClip defaulthitsound;

    // Token: 0x04000049 RID: 73
    public bool spawndust;

    // Token: 0x04000048 RID: 72
    public bool spawndecals;

    // Token: 0x040000B5 RID: 181
    public Color ragdollbloodcolor;

    // Token: 0x040000AF RID: 175
    public GameObject hitparticles;

    // Token: 0x04000054 RID: 84
    public LayerMask bloodsplatlayers;

    // References to prefabs
    public GameObject bulletholes;
    public GameObject dynamicbloodsplats;

    public float traceroffset;

    public MyInputManager inputmanager;


    public float FireSpeed;
    public float WeaponSpeed;
    public float AttackFrame;
    public float Buck;
    public float Inaccuracy;
    public float Damage;

    public bool pauseafterthrowing;
    public bool doattack;
    public bool didattack;

    public GameObject weaponPrefab;
}
