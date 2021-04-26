using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AtlantisUICanvasController : DestroyableCanvasController
{
    public TMP_Text titleText;
    public TMP_Text subtitleText;
    public Image image;
    public Button enterButton;
    public Button exitButton;
    public GameController gameController;

    public void OnEnterButton()
    {
        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        this.titleText.text = "Atlantis!";
        this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);

        if (gameController.hasMermaidTail)
        {
            this.subtitleText.text = "You entered!";
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
        }
        else
        {
            this.subtitleText.text = "They attacked you!";
            this.subtitleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-25);
        }
                    
        this.removeCanvas(1.5f);
    }

    public void OnExitButton()
    {
        this.removeCanvas(0f);
    }
}
