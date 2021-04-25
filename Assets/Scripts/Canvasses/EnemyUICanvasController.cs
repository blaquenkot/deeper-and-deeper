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
    public Button exitButton;
    public int damage;
    public GameController gameController;

    public void OnFightButton()
    {
        this.fightButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        if (UnityEngine.Random.value >= 0.25)
        {
            this.titleText.text = "It attacked you and run away!";
            this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-this.damage);
        }
        else
        {
            this.image.sprite = this.chestTexture;
            this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.titleText.text = "You won and found a chest!";
            this.gameController.updateCoins(10);
        }
                    
        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        if (UnityEngine.Random.value >= 0.35)
        {
            this.titleText.text = "You ran away!";
            this.gameController.updateOxygen(-5);
        }
        else
        {
            this.titleText.text = "It attacked you and run away!";
            this.gameController.updateOxygen(-this.damage);
        }

        this.removeCanvas(1.5f);
    }

    public void updateImage(Texture texture)
    {
        this.image.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.height, texture.width), new Vector2(0.5f, 0.5f));
        this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
    }
}
