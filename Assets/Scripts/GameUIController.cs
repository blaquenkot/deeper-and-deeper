using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public Sprite flashlightSprite;
    public Sprite selectedFlashlightSprite;
    public GameController gameController;
    public Image oxygenImage;
    public Slider oxygenSlider;
    public TMP_Text deepnessText;
    public Image coinsImage;
    public TMP_Text coinsText;
    public Button flashlightButton;
    public TMP_Text flashlightText;
    public CanvasRenderer gameOverPanel;

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
            
        this.deepnessText.transform.DOPunchScale(this.deepnessText.transform.localScale * 1.1f, 0.25f);
    }

    public void updateCoins(int coins, bool animated = true)
    {
        if (animated && coins > int.Parse(this.coinsText.text))
        {
            this.coinsImage.transform.DOPunchScale(Vector3.one * 1.05f, 0.25f);
        }

        this.coinsText.text = coins.ToString();
    }

    public void updateFlashlight(int count, bool animated = true)
    {
        this.flashlightText.text = count.ToString();
        
        if (animated)
        {
            this.flashlightButton.transform.DOPunchScale(this.flashlightButton.transform.localScale * 1.05f, 0.25f);
        }
    }

    public void activateFlashlight()
    {
        if (this.gameController.hasFlashlights())
        {
            this.flashlightButton.image.sprite = this.selectedFlashlightSprite;
            this.flashlightButton.transform.DOPunchScale(this.flashlightButton.transform.localScale * 1.1f, 0.25f);
            this.gameController.activateFlashlight();
        }
    }

    public void deactivateFlashlight()
    {
        this.flashlightButton.image.sprite = this.flashlightSprite;
        this.gameController.deactivateFlashlight();
    }

    public void gameFinished()
    {
        this.oxygenImage.gameObject.SetActive(false);
        this.oxygenSlider.gameObject.SetActive(false);
        this.coinsImage.gameObject.SetActive(false);
        this.coinsText.gameObject.SetActive(false);
        this.flashlightButton.gameObject.SetActive(false);
        this.gameOverPanel.gameObject.SetActive(true);
    }

    public void retryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
