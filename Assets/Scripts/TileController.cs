using System;
using DG.Tweening;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public event Action<Direction> onMove;

    void Start()
    {
        var arrows = GetComponentsInChildren<ArrowController>();
        foreach (ArrowController arrow in arrows)
        {
            arrow.onClick += this.onArrowClicked;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Flip()
    {
        this.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f);
    }

    private void onArrowClicked(Direction direction)
    {
        if (this.onMove != null) {
            this.onMove(direction);
        }
    }
}
