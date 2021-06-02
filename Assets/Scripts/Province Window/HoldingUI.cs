using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HoldingUI : MonoBehaviour
{
    public WindowProvince provinceWindow;
    public Holding holdingCounterpart;
    public int holdingCounterpartIndex;
    [Space(10)]

    public TextMeshProUGUI holdingName;
    public TextMeshProUGUI terrainTypeText;
    public TextMeshProUGUI popsText;
    public TextMeshProUGUI unemployedText;
    public TextMeshProUGUI homelessText;
    public ButtonSound createHoldingButton;
    public RawResourceUI[] rawResourcesUI;

    public void Refresh(bool built)
    {
        if (built)
        {
            holdingName.text = "holding";
            createHoldingButton.gameObject.SetActive(false);
        }
        else
        {
            createHoldingButton.gameObject.SetActive(true);
        }

        for (int i = 0; i < rawResourcesUI.Length; i++)
        {
            rawResourcesUI[i].Refresh(holdingCounterpart.rawResources[i].resource, holdingCounterpart.rawResources[i].amount);
        }

        terrainTypeText.text = holdingCounterpart.terrainType.ToString();
        popsText.text = holdingCounterpart.pops.Count.ToString();
        unemployedText.text = holdingCounterpart.unemployedPops.Count.ToString();
        homelessText.text = holdingCounterpart.homelessPops.Count.ToString();
    }

    public void CreateHolding()
    {
        //provinceWindow.provinceTarget.CreateHolding();
    }

    public void Awake()
    {
        provinceWindow = FindObjectOfType<WindowProvince>();
    }
    #region Scrolling
    public RectTransform buildingScroller;
    const float Top = -2f;
    const float Bot = -100f;

    public BuildingUI[] buildings;

    public void ScrollBuildings(float value)
    {
        buildingScroller.anchoredPosition = new Vector2(buildingScroller.anchoredPosition.x, Mathf.Lerp(Top, Bot, value));
    }
    #endregion
    //bool buildingsExpanded;

    //public List<RectTransform> buildings = new List<RectTransform>();

    //public void PopoutBuildings()
    //{
    //    buildingsExpanded = !buildingsExpanded;

    //    for (int i = 0; i < buildings.Count; i++)
    //    {
    //        buildings[i].productionPreview.sizeDelta = new Vector2(0f, holdings[i].productionPreview.sizeDelta.y);
    //        buildings[i].productionPopuout.sizeDelta = new Vector2(holdings[i].productionPopuout.sizeDelta.x, HoldingUI.ProductionPopoutHeight);
    //        buildings[i].popsPopout.sizeDelta = new Vector2(HoldingUI.PopsPopoutWidth, holdings[i].popsPopout.sizeDelta.y);

    //        if (i == index)
    //        {

    //        }
    //        else
    //        {
    //            holdings[i].productionPreview.sizeDelta = new Vector2(HoldingUI.ProductionPreviewWidth, holdings[i].productionPreview.sizeDelta.y);
    //            holdings[i].productionPopuout.sizeDelta = new Vector2(holdings[i].productionPopuout.sizeDelta.x, 0f);
    //            holdings[i].popsPopout.sizeDelta = new Vector2(0f, holdings[i].popsPopout.sizeDelta.y);
    //        }
    //    }

    //    selectedBuilding = index;
    //}

    //IEnumerator AnimateBuildings

    //public void PopoutBuilding(int index)
    //{
    //    if (index == selectedBuilding)
    //    {
    //        index = -1;
    //    }

    //    for (int i = 0; i < buildings.Count; i++)
    //    {
    //        if (i == index)
    //        {
    //            buildings[i].productionPreview.sizeDelta = new Vector2(0f, holdings[i].productionPreview.sizeDelta.y);
    //            buildings[i].productionPopuout.sizeDelta = new Vector2(holdings[i].productionPopuout.sizeDelta.x, HoldingUI.ProductionPopoutHeight);
    //            buildings[i].popsPopout.sizeDelta = new Vector2(HoldingUI.PopsPopoutWidth, holdings[i].popsPopout.sizeDelta.y);
    //        }
    //        else
    //        {
    //            holdings[i].productionPreview.sizeDelta = new Vector2(HoldingUI.ProductionPreviewWidth, holdings[i].productionPreview.sizeDelta.y);
    //            holdings[i].productionPopuout.sizeDelta = new Vector2(holdings[i].productionPopuout.sizeDelta.x, 0f);
    //            holdings[i].popsPopout.sizeDelta = new Vector2(0f, holdings[i].popsPopout.sizeDelta.y);
    //        }
    //    }

    //    selectedBuilding = index;
    //}
}
