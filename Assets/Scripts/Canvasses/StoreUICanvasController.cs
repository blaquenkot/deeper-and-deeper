using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoreUICanvasController : MonoBehaviour
{
    public int OXYGEN_PRICE = 5;

    public int OXYGEN_CAPACITY_PRICE = 15;

    public int HARPOON_PRICE = 10;

    public int MERMAID_TAIL_PRICE = 50;

    public event Action onDestroy;
    public TMP_Text oxygenRechargeCostText;
    public TMP_Text oxygenCapacityCostText;
    public TMP_Text harpoonCostText;
    public TMP_Text mermaidTailCostText;
    public Button oxygenRechargeButton;
    public Button oxygenCapacityButton;
    public Button harpoonButton;
    public Button mermaidTailButton;
    public Button exitButton;

    public GameController gameController;

    void Start()
    {
        this.oxygenRechargeCostText.text = $"({OXYGEN_PRICE} coins)";
        this.oxygenCapacityCostText.text = $"({OXYGEN_CAPACITY_PRICE} coins)";
        this.harpoonCostText.text = $"({HARPOON_PRICE} coins)";
        this.mermaidTailCostText.text = $"({MERMAID_TAIL_PRICE} coins)";

        this.updateButtonsAvailability();
    }

    public bool CanPurchaseOxygen()
    {
        return !this.gameController.hasMaxOxygen() && this.availableCoins() >= OXYGEN_PRICE;
    }

    public bool CanPurchaseHarpoon()
    {
        return !this.gameController.hasHarpoon && this.availableCoins() >= HARPOON_PRICE;
    }

    public bool CanPurchaseMermaidTail()
    {
        return !this.gameController.hasMermaidTail && this.availableCoins() >= MERMAID_TAIL_PRICE;
    }

    public bool CanPurchaseOxygenCapacity()
    {
        return !this.gameController.hasMaxOxygenCapacity() && this.availableCoins() >= OXYGEN_CAPACITY_PRICE;
    }

    public void OnPurchaseOxygen()
    {
        if (!this.CanPurchaseOxygen())
        {
            return;
        }

        this.gameController.spend(OXYGEN_PRICE);
        this.gameController.rechargeOxygen();
        this.updateButtonsAvailability();
    }

    public void OnPurchaseHarpoon()
    {
        if (!this.CanPurchaseHarpoon())
        {
            return;
        }

        this.gameController.spend(HARPOON_PRICE);
        this.gameController.giveHarpoon();
        this.updateButtonsAvailability();
    }

    public void OnPurchaseMermaidTail()
    {
        if (!this.CanPurchaseMermaidTail())
        {
            return;
        }

        this.gameController.spend(MERMAID_TAIL_PRICE);
        this.gameController.giveMermaidTail();
        this.updateButtonsAvailability();
    }

    public void OnPurchaseOxygenCapacity()
    {
        if (!this.CanPurchaseOxygenCapacity())
        {
            return;
        }

        this.gameController.spend(OXYGEN_CAPACITY_PRICE);
        this.gameController.increaseOxygenCapacity();
        this.gameController.rechargeOxygen();
        this.updateButtonsAvailability();
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }

    private void removeCanvas(float time)
    {
        if (this.onDestroy != null)
        {
            this.onDestroy();
        }
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject, time);
    }

    private int availableCoins()
    {
        return this.gameController.coins;
    }

    private void updateButtonsAvailability()
    {
        this.oxygenRechargeButton.enabled = this.CanPurchaseOxygen();
        this.oxygenCapacityButton.enabled = this.CanPurchaseOxygenCapacity();
        this.harpoonButton.enabled = this.CanPurchaseHarpoon();
        this.mermaidTailButton.enabled = this.CanPurchaseMermaidTail();
    }
}
