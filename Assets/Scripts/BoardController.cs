using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject currentTile;
    public (GameObject, GameObject, GameObject) currentOptions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChooseTile(TileSelection selection) {
        // switch(selection) {
        //     case TileSelection.Left
        // }
    }
}

enum TileSelection {
    Left,
    Center,
    Right
}
