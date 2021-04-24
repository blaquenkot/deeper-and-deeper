using UnityEngine;

public class BoardController : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject currentTile;
    public (GameObject, GameObject, GameObject) currentOptions;

    // Start is called before the first frame update
    void Start()
    {
        GenerateNextOptions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChooseTile(TileSelection.Center);
        }
    }

    void ChooseTile(TileSelection selection)
    {
        var nextTile = selection switch
        {
            TileSelection.Left => currentOptions.Item1,
            TileSelection.Center => currentOptions.Item2,
            TileSelection.Right => currentOptions.Item3,
            _ => throw new System.ArgumentOutOfRangeException(nameof(selection))
        };

        Debug.Log(nextTile.name);
        Debug.Log(nextTile.transform.position);
    }

    void GenerateNextOptions()
    {
        var centerTilePosition = currentTile.transform.position;
        var tileWidth = currentTile.transform.localScale.x;
        var tileHeight = currentTile.transform.localScale.z;

        currentOptions = (
            Instantiate(tilePrefab, centerTilePosition + new Vector3(-tileWidth - 0.15f, 0, -tileHeight - 0.15f), Quaternion.identity),
            Instantiate(tilePrefab, centerTilePosition + new Vector3(0, 0, -tileHeight - 0.15f), Quaternion.identity),
            Instantiate(tilePrefab, centerTilePosition + new Vector3(tileWidth + 0.15f, 0, -tileHeight - 0.15f), Quaternion.identity)
        );
    }
}

enum TileSelection {
    Left,
    Center,
    Right
}
