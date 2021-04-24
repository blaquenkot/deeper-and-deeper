using DG.Tweening;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameController gameController;    
    public GameObject tilePrefab;
    public TileController currentTile;
    public (TileController, TileController, TileController) currentOptions;

    public PlayerController player;
    public GameObject background;

    // Start is called before the first frame update
    void Start()
    {
        SubscribeToCurrentTile();
        GenerateNextOptions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChooseTile(Direction.Central);
        }
    }

    void UnsubscribeFromCurrentTile()
    {
        this.currentTile.onMove -= this.ChooseTile;
    }

    void SubscribeToCurrentTile()
    {
        this.currentTile.onMove += this.ChooseTile;
    }

    void ChooseTile(Direction selection)
    {
        var nextTile = selection switch
        {
            Direction.Left => currentOptions.Item1,
            Direction.Central => currentOptions.Item2,
            Direction.Right => currentOptions.Item3,
            _ => throw new System.ArgumentOutOfRangeException(nameof(selection))
        };

        this.UnsubscribeFromCurrentTile();
        this.currentTile = nextTile;
        this.SubscribeToCurrentTile();
        this.GenerateNextOptions();

        this.MovePlayer();
    }

    void MovePlayer()
    {
        this.gameController.updateDeepness(100);
        this.gameController.updateOxygen(-10);

        this
            .player
            .move(this.currentTile.transform.position)
            .OnComplete(() => {
                this.currentTile.Flip();
                this.moveBackground();
            });
    }

    void moveBackground()
    {
        this.background.transform.position = new Vector3(this.player.transform.position.x, this.background.transform.position.y, this.player.transform.position.z);
    }

    void GenerateNextOptions()
    {
        var centerTilePosition = currentTile.transform.position;
        var tileWidth = currentTile.transform.localScale.x;
        var tileHeight = currentTile.transform.localScale.z;
        var newTileRotation = Quaternion.Euler(0, 0, 180);

        currentOptions = (
            Instantiate(
                tilePrefab,
                centerTilePosition + new Vector3(-tileWidth - 0.15f, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>(),
            Instantiate(
                tilePrefab,
                centerTilePosition + new Vector3(0, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>(),
            Instantiate(
                tilePrefab,
                centerTilePosition + new Vector3(tileWidth + 0.15f, 0, -tileHeight - 0.15f),
                newTileRotation
            ).GetComponent<TileController>()
        );
    }
}
