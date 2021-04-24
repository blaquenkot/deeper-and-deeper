using DG.Tweening;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameController gameController;
    public TileController currentTile;
    public (TileController, TileController, TileController) currentOptions;

    public PlayerController player;
    public GameObject background;

    private TilesGenerator tilesGenerator;
    private bool tileActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        this.tilesGenerator = GetComponent<TilesGenerator>();

        this.GenerateNextOptions();
        this.SubscribeToCurrentOptions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChooseTile(this.currentOptions.Item2);
        }
    }

    void UnsubscribeFromCurrentOptions()
    {
        this.currentOptions.Item1.onClick -= this.ChooseTile;
        this.currentOptions.Item2.onClick -= this.ChooseTile;
        this.currentOptions.Item3.onClick -= this.ChooseTile;
    }

    void SubscribeToCurrentOptions()
    {
        this.currentOptions.Item1.onClick += this.ChooseTile;
        this.currentOptions.Item2.onClick += this.ChooseTile;
        this.currentOptions.Item3.onClick += this.ChooseTile;
    }

    void ChooseTile(TileController selection)
    {
        if (!this.tileActivated)
        {
            this.UnsubscribeFromCurrentOptions();
            this.ShiftAndDiscardOptions(selection);
            this.currentTile = selection;
            this.GenerateNextOptions();
            this.SubscribeToCurrentOptions();

            this.MovePlayer();

            foreach(ITile tile in selection.GetComponents<ITile>())
            {
                if (tile.tileActivated(this)) {
                    this.tileActivated = true;
                    tile.onTileDeactivated += this.onTileDeactivated;
                }
            }
        }
    }

    void MovePlayer()
    {
        this.gameController.updateDeepness(100);
        this.gameController.updateOxygen(-10);

        this.currentTile
            .Flip()
            .OnComplete(() => {
                this.player
                    .move(this.currentTile.transform.position)
                    .OnComplete(() => {
                        this.moveBackground();
                    });
            });
    }

    void moveBackground()
    {
        this.background.transform.position = new Vector3(this.player.transform.position.x, this.background.transform.position.y, this.player.transform.position.z);
    }

    void GenerateNextOptions()
    {
        var centerTilePosition = this.currentTile.transform.position;
        centerTilePosition.x = 0;

        var tileWidth = this.currentTile.transform.localScale.x;
        var tileHeight = this.currentTile.transform.localScale.z;
        var newTileRotation = Quaternion.Euler(0, 0, 180);

        var nextTilesPrefabs = this.tilesGenerator.Next();

        currentOptions = (
            Instantiate(
                nextTilesPrefabs[0],
                centerTilePosition + new Vector3(-tileWidth - 0.15f, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[1],
                centerTilePosition + new Vector3(0, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[2],
                centerTilePosition + new Vector3(tileWidth + 0.15f, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>()
        );
    }

    void ShiftAndDiscardOptions(TileController selectedTile)
    {
        var removeToTheLeft = true;

        var options = new TileController[]
        {
            currentOptions.Item1,
            currentOptions.Item2,
            currentOptions.Item3
        };

        foreach (TileController tile in options)
        {
            if (tile == selectedTile)
            {
                removeToTheLeft = false;
                tile.transform.DOMoveX(0, 0.5f);
            } else {
                tile.transform.DOLocalMoveX(
                    removeToTheLeft ? -15 : 15, 1
                ).OnComplete(() => {
                    Destroy(tile.gameObject);
                });
            }
        }
    }

    void onTileDeactivated()
    {
        this.tileActivated = false;
    }
}
