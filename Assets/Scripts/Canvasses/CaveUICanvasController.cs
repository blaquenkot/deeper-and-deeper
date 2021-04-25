using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CaveUICanvasController : DestroyableCanvasController
{
    public Sprite chestSprite;
    public TMP_Text titleText;
    public Image image;
    public Button enterButton;
    public Button exitButton;

    public GameController gameController;

    void Start()
    {
        this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
    }
    
    public void OnEnterButton()
    {
        this.gameController.updateOxygen(-5);

        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        var random = UnityEngine.Random.value;
        if (random >= 0.5)
        {
            this.image.sprite = this.chestSprite;
            this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.titleText.text = "You found a chest!";
            this.gameController.updateCoins(5);
        }
        else if (random >= 0.2)
        {
            this.titleText.text = "You found nothing";
            this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
        }
        else
        {
            // enemy?
            this.titleText.text = "You found nothing";
            this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
        }

        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }
}
