using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Weapons/Inventory", order = 1)]

public class Inventory : ScriptableObject
{
    public WeaponData[] weapons;
}
