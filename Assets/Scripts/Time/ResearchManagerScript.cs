using FarmerDemo;
using UnityEngine;

public class ResearchManagerScript : MonoBehaviourSingletonBase<ResearchManagerScript>
{
    public GameObject ResearchStatusBar; 
    public int CurrentResearch = 0;
    public int MaxResearch = 100;
    protected override void Awake()
    {
        base.Awake();
        ResetResearchBar();
        HideResearchStatusBar();
    }

    public void HideResearchStatusBar()
    {
        ResearchStatusBar.transform.parent.gameObject.SetActive(false);
    }

    public void ShowResearchStatusBar()
    {
        ResearchStatusBar.transform.parent.gameObject.SetActive(true);
    }

    public void ResetResearchBar()
    {
        CurrentResearch = 0;
        UpdateResearchStatusBarSize();
    }

    public void IncrementResearch()
    {
        CurrentResearch++;
        UpdateResearchStatusBarSize();
        if (CurrentResearch >= MaxResearch)
        {
            EraManagerScript.Instance.AdvanceEra();
            CurrentResearch = 0;
        }
    }

    private void UpdateResearchStatusBarSize()
    {
        RectTransform researchStatusBarSizing = ResearchStatusBar.GetComponent<RectTransform>();
        Vector2 desiredSize = new Vector2(100 * CurrentResearch / MaxResearch, researchStatusBarSizing.sizeDelta.y);
        researchStatusBarSizing.sizeDelta = desiredSize;
    }
}
