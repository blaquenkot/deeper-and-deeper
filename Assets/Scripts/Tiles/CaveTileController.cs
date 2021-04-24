using UnityEngine;
using System;

public class CaveTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public void tileActivated(BoardController parent)
    {
        CaveUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponents<CaveUICanvasController>()[0];
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
