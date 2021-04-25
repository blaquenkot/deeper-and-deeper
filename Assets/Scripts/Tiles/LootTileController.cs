using UnityEngine;
using System;

public class LootTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;

    public bool tileActivated(BoardController parent)
    {
        // TODO: Show some UI indication that the the player got oxygen from this tile
        if (UnityEngine.Random.value >= 0.5) 
        {
            parent.gameController.updateOxygen(10);
        }
        else
        {
            parent.gameController.updateOxygen(5);
        }
        
        return false;
    }
}