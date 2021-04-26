using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class StoreUICanvasController : DestroyableCanvasController
{
    private int OXYGEN_PRICE = 20;
    private int OXYGEN_CAPACITY_PRICE = 60;
    private int HARPOON_PRICE = 70;
    private int FLASHLIGHT_PRICE = 30;
    private int MERMAID_TAIL_PRICE = 150;

    public Sprite mermaidTailSprite;
    public TMP_Text oxygenRechargeCostText;
    public TMP_Text oxygenCapacityCostText;
    public TMP_Text harpoonCostText;
    public TMP_Text flashlightCostText;
    public TMP_Text mermaidTailCostText;
    public Button oxygenRechargeButton;
    public Button oxygenCapacityButton;
    public Button harpoonButton;
    public Button flashlightButton;
    public Button mermaidTailButton;
    public Image oxygenRechargeImage;
    public Image oxygenCapacityImage;
    public Image mermaidTailImage;
    public Image harpoonImage;
    public Image flashlightImage;
    public TMP_Text mermaidTailText;
    public Button exitButton;

    public GameController gameController;

    void Start()
    {
        this.oxygenRechargeCostText.text = $"({OXYGEN_PRICE} coins)";
        this.oxygenCapacityCostText.text = $"({OXYGEN_CAPACITY_PRICE} coins)";
        this.harpoonCostText.text = $"({HARPOON_PRICE} coins)";
        this.flashlightCostText.text =  $"({FLASHLIGHT_PRICE} coins)";
        this.mermaidTailCostText.text = $"({MERMAID_TAIL_PRICE} coins)";

        if (this.gameController.hasMermaidTail)
        {
            this.mermaidTailImage.sprite = this.mermaidTailSprite;
            this.mermaidTailText.text = "Mermaid tail";
        }

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

    public bool CanPurchaseFlashlight()
    {
        return (this.gameController.flashlights != this.gameController.MAX_FLASHLIGHTS) && this.availableCoins() >= FLASHLIGHT_PRICE;
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

        this.oxygenRechargeImage.transform.DOPunchScale(this.oxygenRechargeImage.transform.localScale * 1.1f, 0.25f);
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

        this.harpoonImage.transform.DOPunchScale(this.harpoonImage.transform.localScale * 1.1f, 0.25f);
        this.gameController.spend(HARPOON_PRICE);
        this.gameController.giveHarpoon();
        this.updateButtonsAvailability();
    }

    public void OnPurchaseFlashlight()
    {
        if (!this.CanPurchaseFlashlight())
        {
            return;
        }

        this.flashlightImage.transform.DOPunchScale(this.flashlightImage.transform.localScale * 1.1f, 0.25f);
        this.gameController.spend(FLASHLIGHT_PRICE);
        this.gameController.rechargeFlashlight();
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
        this.mermaidTailImage.sprite = this.mermaidTailSprite;
        this.mermaidTailImage.transform.DOPunchScale(this.mermaidTailImage.transform.localScale * 1.1f, 0.25f);
        this.updateButtonsAvailability();
    }

    public void OnPurchaseOxygenCapacity()
    {
        if (!this.CanPurchaseOxygenCapacity())
        {
            return;
        }

        this.oxygenCapacityButton.transform.DOPunchScale(this.oxygenCapacityButton.transform.localScale * 1.1f, 0.25f);
        this.gameController.spend(OXYGEN_CAPACITY_PRICE);
        this.gameController.increaseOxygenCapacity();
        this.gameController.rechargeOxygen();
        this.updateButtonsAvailability();
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
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
        this.flashlightButton.enabled = this.CanPurchaseFlashlight();
        this.mermaidTailButton.enabled = this.CanPurchaseMermaidTail();
    }
}
