using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public GameController gameController;
    public Image oxygenImage;
    public Slider oxygenSlider;
    public TMP_Text deepnessText;
    public Image coinsImage;
    public TMP_Text coinsText;

    private float currentOxygen = 1f;
    private int currentDeepness = 0;

    public void updateOxygen(float oxygen, bool animated = true)
    {
        if (animated) {
            if (oxygen > this.currentOxygen)
            {
                this.oxygenImage.transform.DOPunchScale(Vector3.one * 1.05f, 0.25f);
            }

            DOTween
                .To(() => this.currentOxygen, x => this.currentOxygen = x, oxygen, 0.5f)
                .OnUpdate(() => {
                    this.oxygenSlider.value = this.currentOxygen;
                });
        }
        else
        {
            this.currentOxygen = oxygen;
            this.oxygenSlider.value = this.currentOxygen;
        }
    }

    public void updateDeepness(int deepness)
    {
        DOTween
            .To(() => this.currentDeepness, x => this.currentDeepness = x, deepness, 0.5f)
            .OnUpdate(() => {
                this.deepnessText.text = this.currentDeepness + " METERS DEEP";
            });
    }

    public void updateCoins(int coins, bool animated = true)
    {
        if (animated && coins > int.Parse(this.coinsText.text))
        {
            this.coinsImage.transform.DOPunchScale(Vector3.one * 1.05f, 0.25f);
        }

        this.coinsText.text = coins.ToString();
    }

    public void activateFlashlight()
    {
        this.gameController.activateFlashlight();
    }

    public void deactivateFlashlight()
    {
        print("this.gameController.deactivateFlashlight");
        this.gameController.deactivateFlashlight();
    }
}
