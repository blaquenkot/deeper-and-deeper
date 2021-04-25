using System.Collections.Generic;
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
    private bool canMove = true;

    void Start()
    {
        this.tilesGenerator = GetComponent<TilesGenerator>();

        this.GenerateNextOptions();
        this.SubscribeToCurrentOptions();
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
        if (this.gameController.flashlightActivated)
        {
            selection.Flip();
            this.gameController.flashlightUsed();
        }
        else if (this.canMove)
        {
            this.canMove = false;
            this.UnsubscribeFromCurrentOptions();
            this.ShiftAndDiscardOptions(selection);
            this.currentTile = selection;
            this.GenerateNextOptions();
            this.SubscribeToCurrentOptions();

            this.MovePlayer();
        }
    }

    void MovePlayer()
    {
        this.currentTile
            .Flip()
            .OnComplete(() => {
                this.gameController.updateOxygen(-10);
                this.gameController.updateDeepness(100);
                this.player
                    .move(this.currentTile.transform.position)
                    .OnComplete(() => {
                        var tileActivated = false;
                        foreach(ITile tile in this.currentTile.GetComponents<ITile>())
                        {
                            if (tile.tileActivated(this)) {
                                tileActivated = true;
                                tile.onTileDeactivated += this.onTileDeactivated;
                            }
                        }
                        this.moveBackground();

                        if (!tileActivated)
                        {
                            this.canMove = true;
                        }
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
                newTileRotation,
                this.transform
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[1],
                centerTilePosition + new Vector3(0, 0, -tileHeight - 0.15f),
                newTileRotation,
                this.transform
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[2],
                centerTilePosition + new Vector3(tileWidth + 0.15f, 0, -tileHeight - 0.15f),
                newTileRotation,
                this.transform
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
        this.canMove = true;
    }

    public void gameFinished()
    {
        this.canMove = false;
        var z = this.player.transform.position.z;
        var x = 0;
        List<int> childrenToRemove = new List<int>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var child = this.transform.GetChild(i);
            if (child.transform.localRotation.z == 0) 
            {
                child.localScale = new Vector3(1.1f, 0.1f, 1.1f);
                child.localPosition = new Vector3(x * 1.25f - 4.4f, 0.5f, z);

                if (x != 0 && x % 7 == 0) 
                {
                    z -= 1.2f;
                    x = 0;
                }
                else 
                {
                    x += 1;
                }
            }
            else
            {
                childrenToRemove.Add(i);
            }
        }

        foreach(int i in childrenToRemove)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }
}
