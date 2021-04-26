using UnityEngine;
using System;

public class ChestTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;

    public bool tileActivated(BoardController parent)
    {
        parent.gameController.updateCoins(10);
        return false;
    }
}
