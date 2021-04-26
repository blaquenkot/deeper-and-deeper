using UnityEngine;
using System;

public class ChestTileController : MonoBehaviour, ITile
{
    public int coins = 10;    
    public event Action onTileDeactivated;

    public bool tileActivated(BoardController parent)
    {
        parent.gameController.updateCoins(this.coins);
        return false;
    }
}
