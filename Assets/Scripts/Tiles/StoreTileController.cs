using UnityEngine;
using System;

public class StoreTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public bool tileActivated(BoardController parent)
    {
        StoreUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponent<StoreUICanvasController>();
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        return true;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
