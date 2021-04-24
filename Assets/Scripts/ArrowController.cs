using System;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Direction direction;
    public event Action<Direction> onClick;

    void OnMouseDown()
    {
        this.onClick(this.direction);
    }
}
