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

    public Tween move(Direction direction)
    {
        var z = this.transform.position.z - 2.5f;
        var x = this.transform.position.x;
        switch (direction)
        {
            case Direction.Left:
            {
                x -= 3.5f;
                break;
            }
            case Direction.Central:
            {
                break;
            }
            case Direction.Right:
            {
                x += 3.5f;
                break;
            }
        }

        return DOTween.Sequence()
            .Append(this.transform.DOMoveZ(z, 0.5f))
            .Append(this.transform.DOMoveX(x, 0.5f))
            .Play();
    }
}
