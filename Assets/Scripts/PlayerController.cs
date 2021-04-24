using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Tween move(Vector3 destination)
    {
        return DOTween.Sequence()
            .Append(this.transform.DOMoveZ(destination.z, 0.3f))
            .Append(this.transform.DOMoveX(destination.x, 0.3f))
            .Play();
    }
}
