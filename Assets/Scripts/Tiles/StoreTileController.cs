using UnityEngine;
using System;

public class StoreTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    private BoardController boardController;

    public bool tileActivated(BoardController parent)
    {
        this.boardController = parent;
        StoreUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponent<StoreUICanvasController>();
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        return true;
    }

    private void OnTileDeactivated()
    {
        this.boardController.onStoreTileDeactivated();
        this.onTileDeactivated();
    }
}
