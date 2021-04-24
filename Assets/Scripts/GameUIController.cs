using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    public TMP_Text oxygenText;
    public TMP_Text deepnessText;

    public void updateOxygen(int oxygen) 
    {
        this.oxygenText.text = "Oxygen: " + oxygen;
    }

    public void updateDeepness(int deepness) 
    {
        this.deepnessText.text = "Deepness: " + deepness;
    }
}
