using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public TMP_Text oxygenText;
    public TMP_Text deepnessText;

    public TMP_Text coinsText;

    public void updateOxygen(int oxygen)
    {
        this.oxygenText.text = "Oxygen: " + oxygen;
    }

    public void updateDeepness(int deepness)
    {
        this.deepnessText.text = "Deepness: " + deepness;
    }

    public void updateCoins(int coins)
    {
        this.coinsText.text = "Coins: " + coins;
    }
}
