using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    private int generation = -1;

    public GameObject caveTilePrefab;
    public GameObject bigChestTilePrefab;
    public GameObject chestTilePrefab;
    public GameObject lootTilePrefab;
    public GameObject enemyMedusaTilePrefab;
    public GameObject enemySharkTilePrefab;
    public GameObject enemyAnguilaTilePrefab;
    public GameObject enemyPiranhaTilePrefab;
    public GameObject enemyStingrayTilePrefab;
    public GameObject enemySquidTilePrefab;
    public GameObject storeTilePrefab;
    public GameObject atlantisTilePrefab;

    public GameController gameController;

    void Start()
    {
        this.caveTilePrefab.tag = "caveTilePrefab";
        this.bigChestTilePrefab.tag = "bigChestTilePrefab";
        this.chestTilePrefab.tag = "chestTilePrefab";
        this.lootTilePrefab.tag = "lootTilePrefab";
        this.enemyMedusaTilePrefab.tag = "enemyMedusaTilePrefab";
        this.enemySharkTilePrefab.tag = "enemySharkTilePrefab";
        this.enemyAnguilaTilePrefab.tag = "enemyAnguilaTilePrefab";
        this.enemyPiranhaTilePrefab.tag = "enemyPiranhaTilePrefab";
        this.enemyStingrayTilePrefab.tag = "enemyStingrayTilePrefab";
        this.enemySquidTilePrefab.tag = "enemySquidTilePrefab";
        this.storeTilePrefab.tag = "storeTilePrefab";
        this.atlantisTilePrefab.tag = "atlantisTilePrefab";
    }

    public List<GameObject> Next()
    {
        this.generation++;

        if (this.isFixedGeneration())
        {
            return this.getFixedGeneration();
        }

        // Ideas:
        // * If almost out of O2, and with enough coin, make it more likely to find a store
        // * If almost out of O2, make it more likely to find a card that gifts O2
        // * But make everything less likely the deeper it gets
        // * Ensure enough coins before running out of oxygen the first time, and force a store

        var tiles = new List<GameObject>();

        tiles.Add(this.getTile(tiles));
        tiles.Add(this.getTile(tiles));
        tiles.Add(this.getTile(tiles));

        return tiles;
    }

    public bool isFixedGeneration()
    {
        return this.generation <= 4;
    }

    public string getFixedGenerationText()
    {
        if (this.generation == 1)
        {
            return LanguageController.Shared.getTutorialChest();
        }
        else if (this.generation == 2)
        {
            return LanguageController.Shared.getTutorialEnemy();
        }
        else if (this.generation == 3)
        {
            return LanguageController.Shared.getTutorialLoot();
        }
        else if (this.generation == 4)
        {
            return LanguageController.Shared.getTutorialCave();
        }
        else if (this.generation == 5)
        {
            return LanguageController.Shared.getTutorialShop();
        }
        else
        {
            return "";
        }
    }

    private List<GameObject> getFixedGeneration()
    {
        if (this.generation == 0)
        {
            return new List<GameObject> { this.chestTilePrefab, this.chestTilePrefab, this.chestTilePrefab };
        }
        else if (this.generation == 1)
        {
            return new List<GameObject> { this.enemyMedusaTilePrefab, this.enemyMedusaTilePrefab, this.enemyMedusaTilePrefab };
        }
        else if (this.generation == 2)
        {
            return new List<GameObject> { this.lootTilePrefab, this.lootTilePrefab, this.lootTilePrefab };
        }
        else if (this.generation == 3)
        {
            return new List<GameObject> { this.caveTilePrefab, this.caveTilePrefab, this.caveTilePrefab };
        }
        else
        {
            return new List<GameObject> { this.storeTilePrefab, this.storeTilePrefab, this.storeTilePrefab };
        }
    }

    public GameObject getEnemy()
    {
        var random = UnityEngine.Random.value;
        var currentTag = this.gameController.boardController.currentTile.tag;
        if (random >= 0.85 && currentTag != this.enemySquidTilePrefab.tag) {
            return this.enemySquidTilePrefab;
        }
        else if (random >= 0.8 && currentTag != this.enemyStingrayTilePrefab.tag)
        {
            return this.enemyStingrayTilePrefab;
        }
        else if (random >= 0.75 && currentTag != this.enemySharkTilePrefab.tag)
        {
            return this.enemySharkTilePrefab;
        }
        else if (random >= 0.6 && currentTag != this.enemyAnguilaTilePrefab.tag)
        {
            return this.enemyAnguilaTilePrefab;
        }
        else if (random >= 0.4 && currentTag != this.enemyPiranhaTilePrefab.tag)
        {
            return this.enemyPiranhaTilePrefab;
        }
        else if (currentTag != this.enemyMedusaTilePrefab.tag)
        {
            return this.enemyMedusaTilePrefab;
        }

        return null;
    }

    private GameObject getChest()
    {
        var random = UnityEngine.Random.value;
        var currentTag = this.gameController.boardController.currentTile.tag;
        if (random >= 0.65 && currentTag != this.bigChestTilePrefab.tag)
        {
            return this.bigChestTilePrefab;
        }
        else if (currentTag != this.chestTilePrefab.tag)
        {
            return this.chestTilePrefab;
        }

        return null;
    }

    private GameObject getTile(List<GameObject> currentTiles)
    {
        var random = UnityEngine.Random.value;
        var currentTag = this.gameController.boardController.currentTile.tag;
        GameObject tile = null;
        if (random >= 0.75 && currentTag != this.lootTilePrefab.tag)
        {
            tile = this.lootTilePrefab;
        }
        else if (random >= 0.6)
        {
            tile = this.getEnemy();
        }
        else if (random >= 0.5)
        {
            tile = this.getChest();
        }
        else if (random >= 0.4 && currentTag != this.storeTilePrefab.tag)
        {
            tile = this.storeTilePrefab;
        }
        else if (this.generation > 10 && random >= 0.3 && currentTag != this.atlantisTilePrefab.tag)
        {
            tile = this.atlantisTilePrefab;
        }
        else if (currentTag != this.caveTilePrefab.tag)
        {
            tile = this.caveTilePrefab;
        }

        if (tile != null)
        {
            foreach(GameObject currentTile in currentTiles)
            {
                if(tile.tag == currentTile.tag)
                {
                    return this.getTile(currentTiles);
                }
            }

            return tile;
        } 
        else 
        {
            return this.getTile(currentTiles);
        }
    }
}
