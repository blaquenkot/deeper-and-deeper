using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnemyUICanvasController : DestroyableCanvasController
{
    public TMP_Text titleText;
    public Image chestImage;
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
            this.gameController.updateOxygen(-this.damage);
        }
        else
        {
            this.chestImage.gameObject.SetActive(true);
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
}
