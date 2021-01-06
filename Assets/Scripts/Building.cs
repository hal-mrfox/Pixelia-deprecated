﻿using System.Collections;
using NaughtyAttributes;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Building : MonoBehaviour, IClickable
{
    public enum BuildingType { Castle }
    public BuildingType buildingType;
    public Country controller;
    public ProvinceScript provinceController;
    public List<Population> containingPops;
    public int popCapacity;
    [ReadOnly]
    public bool hovering;
    bool popCanEnter;
    bool controllersAtWar;
    bool allControlled;

    public void Start()
    {
        RefreshColor();
    }

    [Button]
    public void RaisePop()
    {
        //place pops in correct place! (capitals coords)
        if (containingPops.Count == 0)
        {
            print("You have no pops in this building");
        }
        else
        {
            //setting 1 pop in building to free
            containingPops[containingPops.Count - 1].gameObject.SetActive(true);
            containingPops[containingPops.Count - 1].transform.position = transform.position;
            containingPops.Remove(containingPops[containingPops.Count - 1]);
        }
    }

    [Button]
    public void DestroyBuilding()
    {
        if (containingPops.Count == 0 && controller.capital != this)
        {
            Destroy(gameObject);
            CountryManager.instance.totalBuildings.Remove(this);
        }
        else if (controller.capital == this)
        {
            print("You cannot destroy your capital");
        }
        else if (containingPops.Count > 0)
        {
            print("You still have pops in this building!");
        }
    }

    //pop entering building
    public void OnPointerDown()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && hovering)
        {
            if (popCanEnter)
            {
                if (!controllersAtWar)
                {
                    if (buildingType == BuildingType.Castle)
                    {
                        //take out of countryManager selected pop and set mouse to visible
                        containingPops.Add(CountryManager.instance.selectedPop);
                        RefreshPopState();
                        CountryManager.instance.VisibleMouse();
                    }
                }
                else
                {
                    //***DO BATTLE HERE***\\
                    ChangeBuildingOwnership();
                }
            }
            else
            {
                print("You cannot enter here because you have no military access");
            }
        }
    }

    public void ChangeBuildingOwnership()
    {
        //changing ownership of pops -- have option to kill all or add to yours?
        for (int i = 0; i < containingPops.Count; i++)
        {
            containingPops[i].controller = CountryManager.instance.playerCountry;
        }
        containingPops.Add(CountryManager.instance.selectedPop);


        //changing ownership of this buildings province if its either the capital or all buildings in province are owned by player country
        //looping through all buildings to see if all buildings are owned by player country
        for (int i = 0; i < provinceController.buildings.Count; i++)
        {
            if (provinceController.buildings[i].provinceController == CountryManager.instance.playerCountry)
            {

            }
        }
        if (this == controller.capital || allControlled)
        {
            provinceController.ChangeProvinceOwnership();
            provinceController.owner = CountryManager.instance.playerCountry;
        }
        //changing ownership of this building
        controller = CountryManager.instance.playerCountry;
        RefreshPopState();
        RefreshColor();
        CountryManager.instance.VisibleMouse();
    }

    public void ifPopCanEnter()
    { 
        if (controller == CountryManager.instance.playerCountry)
        {
            popCanEnter = true;
            CountryManager.instance.cursorIcon.GetComponent<Image>().color = Color.green;
        }
        else if (controllersAtWar)
        {
            popCanEnter = true;
            CountryManager.instance.cursorIcon.GetComponent<Image>().color = Color.red;
        }
        else if (!controllersAtWar)
        {
            popCanEnter = false;
            CountryManager.instance.cursorIcon.GetComponent<Image>().color = Color.grey;
        }
    }

    public void OnPointerEnter()
    {
        hovering = true;
        if (CountryManager.instance.selectedPop != null)
        {
            ifPopCanEnter();
        }

        if (CountryManager.instance.playerCountry.atWar.Contains(controller))
        {
            controllersAtWar = true;
        }
        else
        {
            controllersAtWar = false;
        }
    }
    //same with this!
    public void OnPointerExit()
    {
        hovering = false;
    }

    //refresh after taken over
    public void RefreshColor()
    {
        GetComponent<Image>().color = controller.countryColor;
    }

    public void RefreshPopState()
    {
        //sets selectedPop in countrymanager to null so theres nothing selected!
        for (int i = 0; i < containingPops.Count; i++)
        {
            containingPops[i].gameObject.SetActive(false);
            CountryManager.instance.selectedPop = null;
        }
    }

    public Image GetImage()
    {
        return GetComponent<Image>();
    }

    public bool IsProvince()
    {
        return false;
    }
}