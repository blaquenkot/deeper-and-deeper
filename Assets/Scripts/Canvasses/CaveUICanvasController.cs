using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CaveUICanvasController : DestroyableCanvasController
{
    public Sprite chestSprite;
    public Sprite caveDeathSprite;
    public Sprite caveLootSprite;
    public TMP_Text titleText;
    public Image image;
    public Button enterButton;
    public TMP_Text enterButtonText;
    public Button exitButton;
    public TMP_Text exitButtonText;
    public GameController gameController;

    void Start()
    {
        this.titleText.text = LanguageController.Shared.getCaveFoundText();
        this.enterButtonText.text = LanguageController.Shared.getEnterText();
        this.exitButtonText.text = LanguageController.Shared.getGoAwayText();
        this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
    }
    
    public void OnEnterButton()
    {
        this.gameController.updateOxygen(-5);

        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        if (this.gameController.isAlive()) 
        {
            var random = UnityEngine.Random.value;
            if (random >= 0.5)
            {
                this.image.sprite = this.chestSprite;
                this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
                this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
                this.titleText.text = LanguageController.Shared.getChestFoundText();
                this.gameController.updateCoins(50);
            }
            else if (random >= 0.35)
            {
                this.image.sprite = this.caveLootSprite;
                this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
                this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
                this.titleText.text = LanguageController.Shared.getLootFoundText();
                this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            }
            else
            {
                // enemy?
                this.titleText.text = LanguageController.Shared.getNothingFoundText();
                this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            }
        }
        else
        {
            this.image.sprite = this.caveDeathSprite;
            this.titleText.text = LanguageController.Shared.getYouDiedText();
        }

        this.removeCanvas(3f);
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }
}
