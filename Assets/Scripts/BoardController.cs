using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Cinemachine;

public class BoardController : MonoBehaviour
{
    public GameController gameController;
    public TileController currentTile;
    public CinemachineVirtualCamera virtualCamera;
    public (TileController, TileController, TileController) currentOptions;

    public PlayerController player;
    public GameObject background;

    private TilesGenerator tilesGenerator;
    private bool canMove = true;
    private bool tutorialShopAppeared = false;

    void Start()
    {
        this.virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = -1.5f;

        this.tilesGenerator = GetComponent<TilesGenerator>();

        this.GenerateNextOptions();
        this.SubscribeToCurrentOptions();
        
        this.background.transform.position = new Vector3(this.player.transform.position.x, 
                                                        this.background.transform.position.y, 
                                                        this.player.transform.position.z);
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
            if (selection.canFlip() && this.canMove)
            {
                selection.Flip();
                this.gameController.flashlightUsed();
            }
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
                this.currentTile.playSound();

                this.gameController.updateOxygen(-5);

                var z = this.currentTile.transform.position.z - 0.75f;

                this.gameController
                    .updateDeepness(25)                                          
                    .Join(this.moveBackground(z))
                    .Join(this.player.move(z))
                    .Play().OnComplete(() => {
                        if (this.gameController.isAlive()) {
                            var tileActivated = false;
                            foreach(ITile tile in this.currentTile.GetComponents<ITile>())
                            {
                                if (tile.tileActivated(this)) {
                                    tileActivated = true;
                                    tile.onTileDeactivated += this.onTileDeactivated;
                                }
                            }

                            if (!tileActivated)
                            {
                                this.canMove = true;

                                if (this.tutorialShopAppeared)
                                {
                                    this.getRandomOption().Flip();
                                }
                            }
                        }
                    });
            });
    }

    Tween moveBackground(float z)
    {
        return this.background.transform.DOMove(new Vector3(this.player.transform.position.x, 
                                                        this.background.transform.position.y, 
                                                        z), 0.25f);
    }

    void GenerateNextOptions()
    {
        var centerTilePosition = this.currentTile.transform.position;
        centerTilePosition.x = 0;

        var tileWidth = this.currentTile.transform.localScale.x;
        var tileHeight = this.currentTile.transform.localScale.z;
        var newTileRotation = Quaternion.Euler(0, 0, 180);

        var nextTilesPrefabs = this.tilesGenerator.Next();

        Vector3[] finalPositions = {
            centerTilePosition + new Vector3(-tileWidth - 0.15f, 0, -tileHeight - 0.15f),
            centerTilePosition + new Vector3(0, 0, -tileHeight - 0.15f),
            centerTilePosition + new Vector3(tileWidth + 0.15f, 0, -tileHeight - 0.15f)
        };

        var originOffset = new Vector3(0f, 0f, 5f);

        currentOptions = (
            Instantiate(
                nextTilesPrefabs[0],
                finalPositions[0] - originOffset,
                newTileRotation,
                this.transform
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[1],
                finalPositions[1] - originOffset,
                newTileRotation,
                this.transform
            ).GetComponent<TileController>(),
            Instantiate(
                nextTilesPrefabs[2],
                finalPositions[2] - originOffset,
                newTileRotation,
                this.transform
            ).GetComponent<TileController>()
        );

        DOTween.Sequence()
                .Join(currentOptions.Item1.transform.DOLocalMoveZ(finalPositions[0].z, 0.25f))
                .Join(currentOptions.Item2.transform.DOLocalMoveZ(finalPositions[1].z, 0.25f))
                .Join(currentOptions.Item3.transform.DOLocalMoveZ(finalPositions[2].z, 0.25f));

        this.gameController.gameUIController.updateTutorialText(this.tilesGenerator.getFixedGenerationText());
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
        var needsFlipping = false;

        if (this.tutorialShopAppeared && this.allTilesAreFaceDown())
        {
            needsFlipping = true;
            this.getRandomOption().Flip().OnComplete(() => {
                this.canMove = true;
            });
        }

        if (!needsFlipping)
        {
            this.canMove = true;
        }
    }

    bool allTilesAreFaceDown()
    {
        return this.currentOptions.Item1.canFlip() && 
                this.currentOptions.Item2.canFlip() && 
                this.currentOptions.Item3.canFlip();
    }

    TileController getRandomOption()
    {
        var random = Random.value;
        TileController option = null;
        if (random <= 0.33f)
        {
            option = currentOptions.Item1;
        }
        else if (random <= 0.66f)
        {
            option = currentOptions.Item2;
        }
        else
        {
            option = currentOptions.Item3;
        }

        if (option.tag != "atlantisTilePrefab")
        {
            return option;
        }
        else
        {
            return this.getRandomOption();
        }
    }

    public void onStoreTileDeactivated()
    {
        if (!this.tutorialShopAppeared)
        {
            this.tutorialShopAppeared = true;
            this.gameController.gameUIController.infoText.text = LanguageController.Shared.getTutorialFinalStep();
        }
    }

    public void updateCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void updateCurrentTileMiniTile(Texture2D texture)
    {
        this.currentTile.addAditionalTile(texture);
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

        var zPosition = z - 0.75f;
        this.player.transform.DOMoveZ(zPosition, 3f);
        this.moveBackground(zPosition);

        this.virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = 2f;
    }
}
