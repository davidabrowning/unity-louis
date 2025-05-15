using FarmerDemo;
using NUnit.Framework;
using UnityEngine;

public abstract class BuildingBehaviour : ItemInteractableBehaviour, IElectricityStatusObserver
{
    protected override void Start()
    {
        base.Start();
        if (transform.Find("PowerIndicator") != null)
        {
            PlayerScript.Instance.ElectricityStatusObservers.Add(this);
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
        if (transform.Find("PowerIndicator") != null)
            transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * -100 + 1);
    }

    public void Deconstruct()
    {
        PlayerScript.Instance.AddToInventory(CostCalculator.GetItemCosts(ItemInstance.ItemData.ItemType));
        PlayerScript.Instance.ElectricityStatusObservers.Remove(this);
        GridManagerScript.Instance.RemoveItem(ItemInstance);
        Destroy(gameObject);
    }

    protected void StartIdleAnimation()
    {
        GetComponent<Animator>().SetBool("IsWorking", false);
    }

    protected void StartWorkingAnimation()
    {
        GetComponent<Animator>().SetBool("IsWorking", true);
    }

    public void HandlePowerStatusUpdate(bool newPowerStatus)
    {
        if (newPowerStatus)
            transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Adornments/PowerIndicatorOn");
        else
            transform.Find("PowerIndicator").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/Adornments/PowerIndicatorOff");
    }
}
