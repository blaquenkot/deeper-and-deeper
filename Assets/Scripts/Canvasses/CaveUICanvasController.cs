using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CaveUICanvasController : DestroyableCanvasController
{
    public Sprite chestSprite;
    public Sprite caveDeathSprite;
    public Sprite caveLootSprite;
    public TMP_Text titleText;
    public Image image;
    public Button enterButton;
    public TMP_Text enterButtonText;
    public Button exitButton;
    public TMP_Text exitButtonText;
    public GameController gameController;

    private EnemyTileController enemy = null;

    override public void Start()
    {
        base.Start();
        this.titleText.text = LanguageController.Shared.getCaveFoundText();
        this.enterButtonText.text = LanguageController.Shared.getEnterText();
        this.exitButtonText.text = LanguageController.Shared.getGoAwayText();
        this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
    }
    
    public void OnEnterButton()
    {
        if (this.enemy == null)
        {
            this.enterCave();
        }
        else
        {
            this.fightEnemy();
        }
    }

    public void OnExitButton()
    {        
        if (this.enemy == null)
        {
            this.removeCanvas(0f);
        }
        else
        {
            this.enterButton.gameObject.SetActive(false);
            this.exitButton.gameObject.SetActive(false);

            if (UnityEngine.Random.value >= 0.35)
            {
                this.titleText.text = LanguageController.Shared.getEnemyEscapeText();
            }
            else
            {
                this.titleText.text = LanguageController.Shared.getEnemyLostText();
                this.gameController.updateOxygen(-this.enemy.damage);
            }

            this.removeCanvas(3f);
        }

    }

    private void enterCave()
    {
        this.gameController.updateOxygen(-5);

        if (this.gameController.isAlive()) 
        {
            var random = UnityEngine.Random.value;
            if (random >= 0.5)
            {
                this.image.sprite = this.chestSprite;
                this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
                this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
                this.titleText.text = LanguageController.Shared.getChestFoundText();
                this.gameController.updateCoins(50);
            }
            else if (random >= 0.35)
            {
                this.image.sprite = this.caveLootSprite;
                this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
                this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
                this.titleText.text = LanguageController.Shared.getLootFoundText();
                this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            }
            else if (random >= 0.1)
            {
                this.enterButtonText.text = LanguageController.Shared.getFightButtonText();
                this.enterButton.transform.DOMoveY(this.enterButton.transform.position.y - 85f, 0.2f);
                this.exitButton.transform.DOMoveY(this.exitButton.transform.position.y - 85f, 0.2f);

                var tileGenerator = Object.FindObjectOfType<TilesGenerator>();
                GameObject enemy = tileGenerator.getEnemy();
                while(enemy == null)
                {
                    enemy = tileGenerator.getEnemy();
                }

                this.enemy = enemy.GetComponent<EnemyTileController>();

                var texture = (Texture2D) enemy.GetComponent<TileController>().FrontTexture;
                this.image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                this.gameController.boardController.updateCurrentTileMiniTile(texture);
                this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);

                this.titleText.text = LanguageController.Shared.getEnemyName(this.enemy.enemyName).ToUpper() + "\n" + LanguageController.Shared.getEnemyText();
                this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            }
            else
            {
                this.titleText.text = LanguageController.Shared.getNothingFoundText();
                this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            }
        }
        else
        {
            this.image.sprite = this.caveDeathSprite;
            this.titleText.text = LanguageController.Shared.getYouDiedText();
        }

        if (this.enemy == null) 
        {
            this.enterButton.gameObject.SetActive(false);
            this.exitButton.gameObject.SetActive(false);
            this.removeCanvas(3f);
        }
    }

    private void fightEnemy()
    {
        this.enterButton.gameObject.SetActive(false);
        this.exitButton.gameObject.SetActive(false);

        var winThreshold = this.gameController.hasHarpoon ? 0.2f : 0.8f;
        if (UnityEngine.Random.value >= winThreshold)
        {
            this.image.sprite = this.chestSprite;
            this.gameController.boardController.updateCurrentTileMiniTile((Texture2D)this.image.mainTexture);
            this.image.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.titleText.text = LanguageController.Shared.getEnemyWonText();
            this.gameController.updateCoins(25);
        }
        else
        {
            this.titleText.text = LanguageController.Shared.getEnemyLostText();
            this.titleText.transform.DOPunchScale(Vector3.one * 1.05f, 0.5f);
            this.gameController.updateOxygen(-this.enemy.damage);
        }

        this.removeCanvas(3f);
    }
}
