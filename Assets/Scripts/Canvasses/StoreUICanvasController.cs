using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class StoreUICanvasController : DestroyableCanvasController
{
    private int OXYGEN_PRICE = 10;
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
    public TMP_Text oxygenRechargeButtonText;
    public Button oxygenCapacityButton;
    public TMP_Text oxygenCapacityButtonText;
    public Button harpoonButton;
    public TMP_Text harpoonButtonText;
    public Button flashlightButton;
    public TMP_Text flashlightButtonText;
    public Button mermaidTailButton;
    public TMP_Text mermaidTailButtonText;
    public Image oxygenRechargeImage;
    public Image oxygenCapacityImage;
    public Image mermaidTailImage;
    public Image harpoonImage;
    public Image flashlightImage;
    public TMP_Text oxygenRechargeText;
    public TMP_Text oxygenCapacityText;
    public TMP_Text harpoonText;
    public TMP_Text flashlightText;
    public TMP_Text mermaidTailText;
    public Button exitButton;
    public TMP_Text exitButtonText;

    public GameController gameController;

    private Color enabledColor = new Color();
    private Color disabledColor = new Color();

    void Start()
    {
        ColorUtility.TryParseHtmlString("#BF6DAE", out this.enabledColor);
        ColorUtility.TryParseHtmlString("#664D60", out this.disabledColor);

        this.oxygenRechargeCostText.text = LanguageController.Shared.getStoreCoinsText(OXYGEN_PRICE);
        this.oxygenCapacityCostText.text = LanguageController.Shared.getStoreCoinsText(OXYGEN_CAPACITY_PRICE);
        this.harpoonCostText.text = LanguageController.Shared.getStoreCoinsText(HARPOON_PRICE);
        this.flashlightCostText.text =  LanguageController.Shared.getStoreCoinsText(FLASHLIGHT_PRICE);
        this.mermaidTailCostText.text = LanguageController.Shared.getStoreCoinsText(MERMAID_TAIL_PRICE);

        this.oxygenRechargeText.text = LanguageController.Shared.getStoreOxygenTankRechargeText();
        this.oxygenCapacityText.text = LanguageController.Shared.getStoreOxygenTankCapacityText();
        this.harpoonText.text = LanguageController.Shared.getStoreHarpoonText();
        this.flashlightText.text = LanguageController.Shared.getStoreFlashlightText();

        this.oxygenRechargeButtonText.text = LanguageController.Shared.getStoreBuyButtonText();
        this.oxygenCapacityButtonText.text = LanguageController.Shared.getStoreBuyButtonText();
        this.harpoonButtonText.text = LanguageController.Shared.getStoreBuyButtonText();
        this.flashlightButtonText.text = LanguageController.Shared.getStoreBuyButtonText();
        this.mermaidTailButtonText.text = LanguageController.Shared.getStoreBuyButtonText();

        if (this.gameController.hasMermaidTail)
        {
            this.mermaidTailImage.sprite = this.mermaidTailSprite;
            this.mermaidTailText.text = LanguageController.Shared.getStoreMermaidTaleText();
        }

        this.exitButtonText.text = LanguageController.Shared.getStoreExitButtonText();

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
        this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.oxygenRechargeImage.mainTexture);
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
        this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.harpoonImage.mainTexture);
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
        this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.flashlightImage.mainTexture);
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
        this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.mermaidTailImage.mainTexture);
        this.updateButtonsAvailability();
    }

    public void OnPurchaseOxygenCapacity()
    {
        if (!this.CanPurchaseOxygenCapacity())
        {
            return;
        }

        this.oxygenCapacityImage.transform.DOPunchScale(this.oxygenCapacityImage.transform.localScale * 1.1f, 0.25f);
        this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.oxygenCapacityImage.mainTexture);
        this.gameController.spend(OXYGEN_CAPACITY_PRICE);
        this.gameController.increaseOxygenCapacity();
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
        this.oxygenRechargeButton.image.color = this.colorForState(this.oxygenRechargeButton.enabled);
        this.oxygenCapacityButton.enabled = this.CanPurchaseOxygenCapacity();
        this.oxygenCapacityButton.image.color = this.colorForState(this.oxygenCapacityButton.enabled);
        this.harpoonButton.enabled = this.CanPurchaseHarpoon();
        this.harpoonButton.image.color = this.colorForState(this.harpoonButton.enabled);
        this.flashlightButton.enabled = this.CanPurchaseFlashlight();
        this.flashlightButton.image.color = this.colorForState(this.flashlightButton.enabled);
        this.mermaidTailButton.enabled = this.CanPurchaseMermaidTail();
        this.mermaidTailButton.image.color = this.colorForState(this.mermaidTailButton.enabled);
    }

    private Color colorForState(bool enabled)
    {
        return (enabled ? this.enabledColor : this.disabledColor);
    }
}
