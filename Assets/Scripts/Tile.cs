using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    public int xSize;
    public int ySize;
    public List<Door> doors;
    public TilePosition tilePosition;
    public TileType tileType = TileType.None;
    public List<Summon> summonRequirements;
    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponentsInChildren<Door>().ToList();
        if(summonRequirements == null){
            summonRequirements = new List<Summon>();
        }
        Debug.Log("Door Count:"+doors.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
