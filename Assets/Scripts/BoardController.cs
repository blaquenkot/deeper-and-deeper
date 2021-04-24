using DG.Tweening;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    public GameController gameController;
    public GameObject tilePrefab;
    public TileController currentTile;
    public (TileController, TileController, TileController) currentOptions;

    public PlayerController player;

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
        this.ShiftAndDiscardOptions(nextTile);
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
            });
    }

    void GenerateNextOptions()
    {
        var centerTilePosition = this.currentTile.transform.position;
        centerTilePosition.x = 0;

        var tileWidth = this.currentTile.transform.localScale.x;
        var tileHeight = this.currentTile.transform.localScale.z;
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
}
