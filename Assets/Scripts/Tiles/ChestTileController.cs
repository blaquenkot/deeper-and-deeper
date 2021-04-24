using UnityEngine;
using System;

public class ChestTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;

    public bool tileActivated(BoardController parent)
    {
        // TODO: Show some UI indication that the the player got coins from this tile
        parent.gameController.updateCoins(1);
        return false;
    }
}
