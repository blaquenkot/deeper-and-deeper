using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AtlantisUICanvasController : DestroyableCanvasController
{
    public Sprite atlantisEnteredSprite;
    public TMP_Text titleText;
    public TMP_Text subtitleText;
    public Image image;
    public Button enterButton;
    public TMP_Text enterButtonText;
    public Button exitButton;
    public TMP_Text exitButtonText;
    public GameController gameController;

    void Start()
    {
        this.subtitleText.text = LanguageController.Shared.getAtlantisFoundText();
        this.enterButtonText.text = LanguageController.Shared.getAtlantisTryToEnterText();
        this.exitButtonText.text = LanguageController.Shared.getGoAwayText();
    }

    public void OnEnterButton()
    {
        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        this.titleText.text = "Atlantis!";
        this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);

        if (gameController.hasMermaidTail)
        {
            this.image.sprite = this.atlantisEnteredSprite;
            this.subtitleText.text = LanguageController.Shared.getAtlantisEnteredText();
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
        }
        else
        {
            this.subtitleText.text = LanguageController.Shared.getAtlantisNotEnteredText();
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-25);
        }
                    
        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }
}
