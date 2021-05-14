using UnityEngine;
using System;

public class EnemyTileController : MonoBehaviour, ITile
{
    public int damage = 10;
    public string enemyName = "";
    public event Action onTileDeactivated;
    public GameObject canvasPrefab;

    public bool tileActivated(BoardController parent)
    {
        EnemyUICanvasController canvas = Instantiate(this.canvasPrefab, parent.transform.parent).GetComponent<EnemyUICanvasController>();
        canvas.damage = this.damage;
        canvas.gameController = parent.gameController;
        canvas.onDestroy += this.OnTileDeactivated;
        canvas.enemyText.text = LanguageController.Shared.getEnemyName(enemyName);
        canvas.updateImage(this.GetComponent<TileController>().FrontTexture);
        return true;
    }

    private void OnTileDeactivated()
    {
        this.onTileDeactivated();
    }
}
