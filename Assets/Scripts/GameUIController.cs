using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameUIController : MonoBehaviour
{
    public TMP_Text oxygenText;
    public TMP_Text deepnessText;
    public TMP_Text coinsText;

    private int currentDeepness = 0;

    public void updateOxygen(int oxygen)
    {
        this.oxygenText.text = "Oxygen: " + oxygen;
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
