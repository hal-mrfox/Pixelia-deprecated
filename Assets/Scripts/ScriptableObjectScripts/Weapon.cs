using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public enum WeaponType { Blunt, Sword, Spear, Bow, Firearm }
//need ammunition types

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public WeaponType weaponType;

    public MaterialType material;

    public string weaponName;
    [ShowAssetPreview]
    public Sprite weaponIcon;

    public int damage;

    [Range(0, 1)] public float accuracy;//increased by skill
    [Range(0, 1)] public float blockChance;//increased by skill

    [CurveRange(0, 0, 1, 1, EColor.Red)]
    public AnimationCurve rangeEffectiveness;

    public int durability;

    [Range(0, 4)] public int woodEffectiveness;
    [Range(0, 4)] public int lightMetalEffectiveness;
    [Range(0, 4)] public int heavyMetalEffectiveness;

    public int bonusFlankingRange;
}
