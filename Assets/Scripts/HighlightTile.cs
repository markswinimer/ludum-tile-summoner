using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTile : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public TilePlacer tilePlacer;
    public DoorWall doorWall;
    public TilePosition tilePosition;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        tilePlacer = FindFirstObjectByType<TilePlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (boxCollider2D.OverlapPoint(mousePosition))
            {
                tilePlacer.TryCreateNewTile(tilePosition, doorWall);
            }
        }
    }
}
