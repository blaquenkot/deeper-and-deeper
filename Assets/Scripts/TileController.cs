using System;
using DG.Tweening;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Texture FrontTexture;
    public MeshRenderer FrontPlaneRenderer;
    public MeshRenderer MiniFrontPlaneRenderer;
    public event Action<TileController> onClick;

    private AudioSource audioSource;

    void Start()
    {
        if (this.FrontTexture != null)
        {
            this.FrontPlaneRenderer.material.mainTexture = this.FrontTexture;
        }

        this.audioSource = this.GetComponent<AudioSource>();
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
    
    public Tween Flip(bool animated = true)
    {
        var duration = animated ? 0.5f : 0f;
        return this.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), duration);
    }

    public void playSound()
    {
        this.audioSource.Play();
    }

    public void addAditionalTile(Texture2D texture)
    {
        this.MiniFrontPlaneRenderer.gameObject.SetActive(true);
        this.MiniFrontPlaneRenderer.material.mainTexture = texture;
    }
}
