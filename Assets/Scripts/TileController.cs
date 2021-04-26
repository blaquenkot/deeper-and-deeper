using System;
using DG.Tweening;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Texture FrontTexture;
    public MeshRenderer FrontPlaneRenderer;
    public event Action<TileController> onClick;

    void Start()
    {
        if (this.FrontTexture != null)
        {
            this.FrontPlaneRenderer.material.mainTexture = this.FrontTexture;
        }
    }

    void OnMouseDown()
    {
        if (this.onClick != null) {
            this.onClick(this);
        }
    }

    public bool canFlip()
    {
        return this.transform.localRotation.z != 0;
    }
    
    public Tween Flip()
    {
        return this.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 0.5f);
    }
}
