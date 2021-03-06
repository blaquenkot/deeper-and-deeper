using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUIController : MonoBehaviour
{
    public Sprite flashlightSprite;
    public Sprite selectedFlashlightSprite;
    public GameController gameController;
    public Image oxygenImage;
    public Slider oxygenSlider;
    public Image oxygenSliderImage;
    public TMP_Text deepnessText;
    public TMP_Text deepnessTextFinal;
    public Image coinsImage;
    public TMP_Text coinsText;
    public Button flashlightButton;
    public TMP_Text flashlightText;
    public CanvasRenderer gameOverPanel;
    public TMP_Text gameOverText;
    public TMP_Text gameOverRetryText;
    public CanvasRenderer helpPanel;
    public Button helpButton;
    public TMP_Text helpText;
    public TMP_Text helpTitle;
    public TMP_Text helpCloseText;
    public TMP_Text infoText;

    private Color oxygenSliderColor = new Color();
    private float currentOxygen = 1f;
    private int currentDeepness = 0;
    private bool isAnimatingFlashlight = false;

    void Start()
    {
        ColorUtility.TryParseHtmlString("#00A1FF", out this.oxygenSliderColor);
        this.oxygenSliderImage.color = this.oxygenSliderColor;
        this.helpText.text = LanguageController.Shared.getHelpText();
        this.helpTitle.text = LanguageController.Shared.getHelpTitle();
        this.helpCloseText.text = LanguageController.Shared.getHelpCloseButton();
        this.gameOverText.text = LanguageController.Shared.getGameOver();
        this.gameOverRetryText.text = LanguageController.Shared.getRetryButtonText();
    }

    public void updateOxygen(float oxygen, bool animated = true)
    {
        if (animated) 
        {
            if (oxygen > this.currentOxygen)
            {
                this.oxygenImage.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.05f, 0.25f);
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

        if (oxygen <= 0.35f)
        {
            this.oxygenSliderImage.color = Color.red;
        }
        else
        {
            this.oxygenSliderImage.color = this.oxygenSliderColor;
        }
    }

    public void updateMaxOxygen(int maxOxygen) 
    {
        this.currentOxygen = 1f;
        this.oxygenSlider.value = 1f;

        this.oxygenSliderImage.color = this.oxygenSliderColor;

        var x = Mathf.Clamp(this.oxygenSlider.transform.localScale.x + 0.25f, 1f, 2.7f);
        DOTween.Sequence()
                .Join(this.oxygenImage.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.05f, 0.25f))
                .Join(this.oxygenSlider.transform.DOScaleX(x, 0.25f))
                .Play();
    }

    public void updateDeepness(int deepness, bool animated = true)
    {
        if (animated)
        {
            this.updateDeepnessAnimated(deepness).Play();
        }
        else
        {
            this.currentDeepness = deepness;
            this.deepnessText.text = LanguageController.Shared.getDeepnessText(this.currentDeepness);
        }
    }

    public Sequence updateDeepnessAnimated(int deepness) {
        return DOTween.Sequence()
            .Join(DOTween
                .To(() => this.currentDeepness, x => this.currentDeepness = x, deepness, 0.5f)
                .OnUpdate(() => {
                    this.deepnessText.text = LanguageController.Shared.getDeepnessText(this.currentDeepness);
                }))
            .Join(this.deepnessText.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.1f, 0.25f));
    }

    public void updateCoins(int coins, bool animated = true)
    {
        if (animated && coins > int.Parse(this.coinsText.text))
        {
            DOTween.Sequence()
                    .Join(this.coinsImage.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.05f, 0.25f))
                    .Join(this.coinsText.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.05f, 0.25f));
        }

        this.coinsText.text = coins.ToString();
    }

    public void updateFlashlight(int count, bool animated = true)
    {
        this.flashlightText.text = count.ToString();

        if (count == 3 && this.shouldPlayFlashlightAnimation())
        {
            this.flashlightAnimation();
        }
        else if (animated)
        {
            DOTween.Sequence()
                .Join(this.flashlightButton.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f))
                .Join(this.flashlightText.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f));
        }
    }

    public void activateFlashlight()
    {
        if (this.gameController.hasFlashlights())
        {
            if (!this.gameController.flashlightActivated)
            {
                this.flashlightButton.image.sprite = this.selectedFlashlightSprite;

                DOTween.Sequence()
                    .Join(this.flashlightButton.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f))
                    .Join(this.flashlightText.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f));

                this.gameController.activateFlashlight();
            }
            else
            {
                this.deactivateFlashlight();
            }
        }
    }

    public void deactivateFlashlight()
    {
        this.flashlightButton.image.sprite = this.flashlightSprite;
        this.flashlightButton.transform.localScale = Vector3.one * 0.15f;
        this.flashlightText.transform.localScale = Vector3.one;
        this.gameController.deactivateFlashlight();
    }

    public void gameFinished(bool won)
    {
        if (won)
        {
            this.gameOverText.text = LanguageController.Shared.getYouWon();
        }
        else
        {
            this.gameOverText.text = LanguageController.Shared.getGameOver();
        }

        this.oxygenImage.gameObject.SetActive(false);
        this.oxygenSlider.gameObject.SetActive(false);
        this.coinsImage.gameObject.SetActive(false);
        this.coinsText.gameObject.SetActive(false);
        this.flashlightButton.gameObject.SetActive(false);
        this.helpButton.gameObject.SetActive(false);
        this.deepnessText.gameObject.SetActive(false);

        this.deepnessTextFinal.text = this.deepnessText.text;
        this.deepnessTextFinal.gameObject.SetActive(true);
        this.gameOverPanel.gameObject.SetActive(true);
    }

    public void retryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void showHelp()
    {
        this.gameController.boardController.updateCanMove(false);
        this.helpPanel.gameObject.SetActive(true);
    }

    public void hideHelp()
    {
        this.helpPanel.gameObject.SetActive(false);
        this.gameController.boardController.updateCanMove(true);
    }

    public void updateTutorialText(string text) 
    {
        if (this.infoText.text != text)
        {
            this.infoText.text = text;

            DOTween.Sequence()
                    .AppendInterval(0.25f)
                    .Append(this.infoText.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.15f, 0.3f));
        }
    }

    private void flashlightAnimation()
    {
        if (this.shouldPlayFlashlightAnimation() && !this.isAnimatingFlashlight) 
        {
            this.isAnimatingFlashlight = true;
            DOTween.Sequence()
                    .Join(this.flashlightButton.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f))
                    .Join(this.flashlightText.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f))
                    .AppendInterval(0.5f)
                    .OnComplete(() => {
                        this.isAnimatingFlashlight = false;
                        if (this.shouldPlayFlashlightAnimation())
                        {
                            this.flashlightAnimation();
                        }
                });
        }
    }

    private bool shouldPlayFlashlightAnimation()
    {
        return !this.gameController.flashlightActivated && this.gameController.flashlights > 0;
    }
}
