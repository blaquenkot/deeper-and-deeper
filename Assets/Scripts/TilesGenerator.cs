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

        // TODO: Different random value per tile
        var shouldHaveEnemy = (this.generation > 3 && UnityEngine.Random.value >= 0.6);

        return new GameObject[]
        {
            shouldHaveEnemy ? this.getEnemy() : this.getBasicTile(),
            shouldHaveEnemy ? this.getEnemy() : this.getBasicTile(),
            shouldHaveEnemy ? this.getEnemy() : this.getBasicTile()
        };
    }

    // We want to always start with a treasure
    private GameObject[] FirstGeneration()
    {
        return new GameObject[]
        {
            this.chestTilePrefab,
            this.storeTilePrefab,
            this.chestTilePrefab
        };
    }

    private GameObject getEnemy()
    {
        if (this.generation >= 7)
        {
            return this.enemySharkTilePrefab;
        }
        else if (this.generation >= 5)
        {
            return this.enemyAnguilaTilePrefab;
        }
        else if (this.generation >= 4)
        {
            return this.enemyPiranhaTilePrefab;
        }
        else
        {
            return this.enemyMedusaTilePrefab;
        }
    }

    private GameObject getBasicTile()
    {
        var random = UnityEngine.Random.value;
        if (random >= 0.75)
        {
            return this.lootTilePrefab;
        }
        else if (random >= 0.5)
        {
            return this.chestTilePrefab;
        }
        else
        {
            return this.caveTilePrefab;
        }
    }
}
