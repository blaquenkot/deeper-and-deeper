using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem wonBubbles;
    public ParticleSystem lostBubbles;

    public Tween move(Vector3 destination)
    {
        return this.transform.DOMoveZ(destination.z - 0.75f, 0.3f);
    }

    public void lostOxygen()
    {
        this.lostBubbles.Play();
    }

    public void wonOxygen()
    {
        this.wonBubbles.Play();
    }
}
