using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CaveUICanvasController : MonoBehaviour
{
    public event Action onDestroy;
    public TMP_Text titleText;
    public Image chestImage;
    public Button enterButton;
    public Button exitButton;

    public GameController gameController;

    public void OnEnterButton()
    {
        this.gameController.updateOxygen(-5);

        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        var random = UnityEngine.Random.value;
        if (random >= 0.5)
        {
            this.chestImage.gameObject.SetActive(true);
            this.titleText.text = "You found a chest!";
            // this.gameController.updateMoney(-5);
        } 
        else if (random >= 0.2)
        {
            this.titleText.text = "You found nothing";
        } 
        else
        {
            // enemy?
            this.titleText.text = "You found nothing";
        }

        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }

    private void removeCanvas(float time)
    {
        if(this.onDestroy != null)
        {
            this.onDestroy();
        }
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject, time);
    }
}
