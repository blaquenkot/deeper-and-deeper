using UnityEngine;
using System;

public class CaveTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public bool tileActivated(BoardController parent)
    {
        CaveUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponent<CaveUICanvasController>();
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        return true;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
