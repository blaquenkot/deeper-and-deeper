using UnityEngine;
using System;

public class LootTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;

    public bool tileActivated(BoardController parent)
    {
        if (UnityEngine.Random.value >= 0.5) 
        {
            parent.gameController.updateOxygen(40);
        }
        else
        {
            parent.gameController.updateOxygen(20);
        }
        
        return false;
    }
}