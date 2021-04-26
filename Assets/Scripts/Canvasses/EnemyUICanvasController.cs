using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EnemyUICanvasController : DestroyableCanvasController
{
    public Sprite chestTexture;
    public TMP_Text enemyText;
    public TMP_Text titleText;
    public Image image;
    public Button fightButton;
    public TMP_Text fightButtonText;
    public Button exitButton;
    public TMP_Text exitButtonText;
    public int damage;
    public GameController gameController;

    void Start()
    {
        this.fightButtonText.text = LanguageController.Shared.getFightButtonText();
        this.exitButtonText.text = LanguageController.Shared.getGoAwayText();
    }

    public void OnFightButton()
    {
        this.fightButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        var winThreshold = this.gameController.hasHarpoon ? 0.2f : 0.8f;
        if (UnityEngine.Random.value >= winThreshold)
        {
            this.image.sprite = this.chestTexture;
            this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.titleText.text = LanguageController.Shared.getEnemyWonText();
            this.gameController.updateCoins(25);
        }
        else
        {
            this.titleText.text = LanguageController.Shared.getEnemyLostText();
            this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-this.damage);
        }
                    
        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        this.fightButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        if (UnityEngine.Random.value >= 0.35)
        {
            this.titleText.text = LanguageController.Shared.getEnemyEscapeText();
            this.gameController.updateOxygen(-5);
        }
        else
        {
            this.titleText.text = LanguageController.Shared.getEnemyLostText();
            this.gameController.updateOxygen(-this.damage);
        }

        this.removeCanvas(1.5f);
    }

    public void updateImage(Texture texture)
    {
        this.image.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
    }
}
