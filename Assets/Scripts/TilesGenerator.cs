using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    int generation = 0;

    public GameObject caveTilePrefab;
    public GameObject chestTilePrefab;
    public GameObject lootTilePrefab;
    public GameObject enemyMedusaTilePrefab;
    public GameObject enemySharkTilePrefab;
    public GameObject enemyAnguilaTilePrefab;
    public GameObject enemyPiranhaTilePrefab;
    public GameObject storeTilePrefab;
    public GameObject atlantisTilePrefab;

    public GameController gameController;

    public GameObject[] Next()
    {
        if (this.generation == 0)
        {
            this.generation++;
            return this.FirstGeneration();
        }

        this.generation++;

        // Ideas:
        // * If almost out of O2, and with enough coin, make it more likely to find a store
        // * If almost out of O2, make it more likely to find a card that gifts O2
        // * But make everything less likely the deeper it gets
        // * Ensure enough coins before running out of oxygen the first time, and force a store

        return new GameObject[]
        {
            this.getTile(),
            this.getTile(),
            this.getTile()
        };
    }

    // We want to always start with a treasure
    private GameObject[] FirstGeneration()
    {
        return new GameObject[]
        {
            this.chestTilePrefab,
            this.chestTilePrefab,
            this.chestTilePrefab
        };
    }

    private GameObject getEnemy()
    {
        var random = UnityEngine.Random.value;
        if (random >= 0.75)
        {
            return this.enemySharkTilePrefab;
        }
        else if (random >= 0.6)
        {
            return this.enemyAnguilaTilePrefab;
        }
        else if (random >= 0.4)
        {
            return this.enemyPiranhaTilePrefab;
        }
        else
        {
            return this.enemyMedusaTilePrefab;
        }
    }

    private GameObject getTile()
    {
        var random = UnityEngine.Random.value;
        if (random >= 0.75)
        {
            return this.lootTilePrefab;
        }
        else if (random >= 0.6)
        {
            return this.getEnemy();
        }
        else if (random >= 0.5)
        {
            return this.chestTilePrefab;
        }
        else if (random >= 0.4)
        {
            return this.storeTilePrefab;
        }
        else if (random >= 0.3)
        {
            return this.atlantisTilePrefab;
        }
        else
        {
            return this.caveTilePrefab;
        }
    }
}
