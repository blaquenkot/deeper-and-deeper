using System;
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

    private void onArrowClicked(Direction direction)
    {
        if (this.onMove != null) {
            this.onMove(direction);
        }
    }
}
