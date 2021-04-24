using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    int generation = 0;

    public GameObject caveTilePrefab;
    public GameObject chestTilePrefab;

    public GameObject[] Next()
    {
        if (this.generation == 0) {
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
            UnityEngine.Random.value > 0.5 ? this.chestTilePrefab : this.caveTilePrefab,
            UnityEngine.Random.value > 0.5 ? this.chestTilePrefab : this.caveTilePrefab,
            UnityEngine.Random.value > 0.5 ? this.chestTilePrefab : this.caveTilePrefab
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
}
