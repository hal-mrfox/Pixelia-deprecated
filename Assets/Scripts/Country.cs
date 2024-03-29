﻿using System.Collections;
using NaughtyAttributes;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class Country : MonoBehaviour
{
    #region Country Level
    public enum Tier { barony, county, duchy, kingdom, empire }
    [BoxGroup("Country Level")]
    public Tier tier;
    [BoxGroup("Country Level")]
    [Range(0,1)]public float prestige;
    #endregion
    #region Cosmetics
    [BoxGroup("Cosmetics")]
    public Color countryColor;
    #endregion
    #region Beliefs
    [BoxGroup("Beliefs")]
    public Religion religion;
    [BoxGroup("Beliefs")]
    public Culture culture;
    [BoxGroup("Beliefs")]
    public Ideology ideology;
    [BoxGroup("Beliefs")]
    public BeliefsManager.Nationality nationality;
    #endregion
    #region Statistics
    [BoxGroup("Statistics")]
    public Province capitalProvince;
    [BoxGroup("Statistics")]
    public List<Province> ownedProvinces;
    [BoxGroup("Statistics")]
    public List<Holding> ownedHoldings;
    [BoxGroup("Statistics")]
    public List<Population> population;
    [BoxGroup("Statistics")]
    public List<Population> militaryPops;
    [BoxGroup("Statistics")]
    public List<OldBuilding> buildings;
    [BoxGroup("Statistics")]
    public List<Army> armies;
    #endregion
    #region Money & Resources
    [BoxGroup("Money & Resources")]
    public int money;
    [BoxGroup("Money & Resources")]
    public List<CountryResource> resources = new List<CountryResource>();
    #endregion
    #region Diplomacy
    [BoxGroup("Diplomacy")]
    public List<Country> atWar;
    #endregion
    #region Armies
    [Button]
    public void CreateArmy()
    {
        Army original = Resources.Load<UnitManager>("UnitManager").armyPrefab;
        armies.Add(Instantiate(original, ownedProvinces[0].transform));
        var newArmy = armies[armies.Count - 1];

        newArmy.owner = this;
        newArmy.armyTier = ArmyTier.Troop;
    }

    public Army armyToAddTo;
    public UnitType newUnitType;

    [Button]
    public void CreateUnit()
    {
        for (int i = 0; i < Resources.Load<UnitManager>("UnitManager").units.Count; i++)
        {
            if (Resources.Load<UnitManager>("UnitManager").units[i].unitType == newUnitType)
            {
                var original = Resources.Load<UnitManager>("UnitManager").unitPrefab;
                armyToAddTo.units.Add(Instantiate(original, armyToAddTo.transform));
                var newUnit = armyToAddTo.units[armyToAddTo.units.Count - 1];

                newUnit.unitType = newUnitType;
                Resources.Load<UnitManager>("UnitManager").units[i].Initialize(newUnit);
                newUnit.army = armyToAddTo;
                break;
            }
        }
    }
    #endregion
    #region All Country Stuff
    public void Start()
    {
        for (int i = 0; i < ownedHoldings.Count; i++)
        {
            ownedHoldings[i].owner = this;
            for (int j = 0; j < ownedHoldings[i].buildings.Count; j++)
            {
                ownedHoldings[i].buildings[j].holding = ownedHoldings[i];
            }
            for (int j = 0; j < ownedHoldings[i].pops.Count; j++)
            {
                ownedHoldings[i].pops[j].controller = this;
            }
        }

        for (int i = 0; i < ownedProvinces.Count; i++)
        {
            ownedProvinces[i].owner = this;
            ownedProvinces[i].FirstStart();
        }

        Refresh();
    }

    #region Resource Class
    [System.Serializable]
    public class CountryResource
    {
        public Resource resource;
        public int amount;

        public CountryResource(Resource resource, int amount)
        {
            this.resource = resource;
            this.amount = amount;
        }
    }
    #endregion

    public void Refresh()
    {
        population.Clear();
        for (int i = 0; i < ownedHoldings.Count; i++)
        {
            ownedHoldings[i].RefreshValues();
            for (int j = 0; j < ownedHoldings[i].pops.Count; j++)
            {
                population.Add(ownedHoldings[i].pops[j]);
            }
        }
    }

    public void NextTurn()
    {
        for (int i = 0; i < ownedHoldings.Count; i++)
        {
            ownedHoldings[i].NextTurn();
        }
    }

    public void UpgradeTier()
    {
        if (prestige >= 1)
        {
            tier += 1;
            prestige -= 1;
        }
    }

    public void DowngradeTier()
    {

    }

    public void CalculateResources()
    {
        resources.Clear();
        for (int i = 0; i < ownedProvinces.Count; i++)
        {
            for (int j = 0; j < ownedProvinces[i].storedResources.Count; j++)
            {
                resources.Add(new CountryResource(ownedProvinces[i].storedResources[j].resource, ownedProvinces[i].storedResources[j].resourceCount));
            }
        }
    }
    #endregion
}
