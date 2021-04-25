using UnityEngine;
using System;

public class AtlantisTileController : MonoBehaviour, ITile
{
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public bool tileActivated(BoardController parent)
    {
        AtlantisUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponents<AtlantisUICanvasController>()[0];
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        return true;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
