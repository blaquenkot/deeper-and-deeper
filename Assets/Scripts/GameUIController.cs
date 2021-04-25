using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Slider oxygenSlider;
    public TMP_Text deepnessText;
    public TMP_Text coinsText;

    private float currentOxygen = 1f;
    private int currentDeepness = 0;

    public void updateOxygen(float oxygen)
    {
        DOTween
            .To(() => this.currentOxygen, x => this.currentOxygen = x, oxygen, 0.5f)
            .OnUpdate(() => {
                this.oxygenSlider.value = this.currentOxygen;
            });
    }

    public void updateDeepness(int deepness)
    {
        DOTween
            .To(() => this.currentDeepness, x => this.currentDeepness = x, deepness, 0.5f)
            .OnUpdate(() => {
                this.deepnessText.text = this.currentDeepness + " METERS DEEP";
            });
    }

    public void updateCoins(int coins)
    {
        this.coinsText.text = "Coins: " + coins;
    }
}
