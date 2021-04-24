using UnityEngine;

public class TileController : MonoBehaviour
{
    private PlayerController player;
    void Start()
    {
        this.player = Object.FindObjectOfType<PlayerController>();
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

    private void onArrowClicked(ArrowDirection direction)
    {
        this.player.move(direction);
    }
}
