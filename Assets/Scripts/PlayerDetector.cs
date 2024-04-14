using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public Player player;
    private Tile parentTile;
    private KillZoneDetector killZoneDetector;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        parentTile = GetComponentInParent<Tile>();
        killZoneDetector = FindFirstObjectByType<KillZoneDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponentInParent<Player>() != null){
            player.currentTile = parentTile;
            Debug.Log("Changed player tile to " + parentTile.tilePosition.x + "," + parentTile.tilePosition.y);
            if(parentTile.tileType == TileType.Shrine) player.checkpoint = parentTile;
            killZoneDetector.SetNewPosition(transform.position);
        }
    }
}
