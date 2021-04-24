using System;
using UnityEngine;

public enum ArrowDirection
{
    Left,
    Central,
    Right
}

public class ArrowController : MonoBehaviour
{
    public ArrowDirection direction;
    public event Action<ArrowDirection> onClick;
    
    void OnMouseDown()
    {
        onClick(this.direction);
    }
}
