using UnityEngine;
using System;

public class EnemyTileController : MonoBehaviour, ITile
{
    public int damage = 10;
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public bool tileActivated(BoardController parent)
    {
        EnemyUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponents<EnemyUICanvasController>()[0];
        canvas.damage = this.damage;
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        return true;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
