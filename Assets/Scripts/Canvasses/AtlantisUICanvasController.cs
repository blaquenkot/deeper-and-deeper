using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AtlantisUICanvasController : DestroyableCanvasController
{
    public Sprite atlantisEnteredSprite;
    public Sprite atlantisNotEnteredSprite;

    public TMP_Text titleText;
    public TMP_Text subtitleText;
    public Image image;
    public Button enterButton;
    public TMP_Text enterButtonText;
    public Button exitButton;
    public TMP_Text exitButtonText;
    public Button finishButton;
    public TMP_Text finishButtonText;
    public GameController gameController;

    override public void Start()
    {
        base.Start();
        this.subtitleText.text = LanguageController.Shared.getAtlantisFoundText();
        this.enterButtonText.text = LanguageController.Shared.getAtlantisTryToEnterText();
        this.finishButtonText.text = LanguageController.Shared.getFinishButtonText();
        this.exitButtonText.text = LanguageController.Shared.getGoAwayText();
    }

    public void OnEnterButton()
    {
        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        this.titleText.text = LanguageController.Shared.getAtlantisText();
        this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);

        if (gameController.hasMermaidTail)
        {
            this.image.sprite = this.atlantisEnteredSprite;
            this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
            this.subtitleText.text = LanguageController.Shared.getAtlantisEnteredText();
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.finishButton.gameObject.SetActive(true);
            this.gameController.enteredAtlantis();
        }
        else
        {
            this.image.sprite = this.atlantisNotEnteredSprite;
            this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
            this.subtitleText.text = LanguageController.Shared.getAtlantisNotEnteredText();
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-20);
            this.removeCanvas(3f);
        }           
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }
}
