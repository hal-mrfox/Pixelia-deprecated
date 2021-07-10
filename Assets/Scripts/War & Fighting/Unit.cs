using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    public string unitName;
    public UnitType unitType;
    public Army army;

    public int flankingRange;
    public int speed;
    public int range;
    public int value;
    public int morale;

    [ReadOnly]
    public bool isInfantry;
    [ShowIf("isInfantry")]
    public Infantry infantry;

    [ReadOnly]
    public bool isCavalry;
    [ShowIf("isCavalry")]
    public Cavalry cavalry;

    [ReadOnly]
    public bool isArtillery;
    [ShowIf("isArtillery")]
    public Artillery artillery;

    #region Infantry
    [System.Serializable]
    public class Infantry
    {
        [ProgressBar("Head" ,75, EColor.Red)]
        public int headHealth;
        [ProgressBar("Torso", 80, EColor.Orange)]
        public int torsoHealth;
        [ProgressBar("Legs", 60, EColor.Yellow)]
        public int legsHealth;

        public ArmorType headArmor;
        public ArmorType torsoArmor;
        public ArmorType legsArmor;

        [ShowAssetPreview]
        public Weapon equippedWeapon;
    }

    [System.Serializable]
    public class Cavalry : Infantry
    {
        [ProgressBar("Animal", 120, EColor.Indigo)]
        public int animalHealth;

        public ArmorType animalArmor;
    }
    #endregion

    #region Artillery
    [System.Serializable]
    public class Artillery
    {
        [ProgressBar("Health", 60, EColor.Red)]
        public int health;
    }
    #endregion

    #region Initialize Equipment and Stats
    #endregion
}
