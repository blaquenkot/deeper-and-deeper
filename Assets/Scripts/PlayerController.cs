using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Texture harpoonTexture;
    public Texture mermaidTailTexture;
    public Texture harpoonWithMermaidTailTexture;
    public MeshRenderer FrontPlaneRenderer;
    public ParticleSystem wonCoinsParticles;
    public ParticleSystem wonBubblesParticles;
    public ParticleSystem lostBubblesParticles;

    public Tween move(float z)
    {
        return this.transform.DOMoveZ(z, 0.3f);
    }

    public void updateSprite(bool hasHarpoon, bool hasMermaidTail)
    {
        if (hasHarpoon)
        {
            if (hasMermaidTail)
            {
                this.FrontPlaneRenderer.material.mainTexture = this.harpoonWithMermaidTailTexture;
            }
            else
            {
                this.FrontPlaneRenderer.material.mainTexture = this.harpoonTexture;
            }
        }
        else if (hasMermaidTail)
        {
            this.FrontPlaneRenderer.material.mainTexture = this.mermaidTailTexture;
        }

        this.transform.DOPunchScale(this.transform.localScale * 1.1f, 0.25f);
    }

    public void wonCoins()
    {
        this.wonCoinsParticles.Play();
    }
    
    public void lostOxygen()
    {
        this.lostBubblesParticles.Play();
    }

    public void wonOxygen()
    {
        this.wonBubblesParticles.Play();
    }
}
