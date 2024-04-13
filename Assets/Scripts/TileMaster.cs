using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TileMaster : MonoBehaviour
{
    public Dictionary<TilePosition, Tile> existingTiles;

    public List<GameObject> tilePrefabs;

    public int tileOffset = 100;

    private TileParent tileParent;

    // Start is called before the first frame update
    void Start()
    {
        tileParent = FindFirstObjectByType<TileParent>();
        existingTiles = new Dictionary<TilePosition, Tile>();
        tilePrefabs = FindFirstObjectByType<TilePrefabs>().GetComponentsInChildren<Tile>(true).Select(t => t.gameObject).ToList();
        tilePrefabs.ForEach(t => t.SetActive(false));
        CreateTile(new TilePosition(0, 0), tilePrefabs[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateTile(TilePosition currentTilePosition, TilePosition newTilePoisition, DoorWall doorWall, GameObject selectedTile){
        //TODO: Check all surrounding tiles, not just left tile
        var exists = existingTiles.TryGetValue(currentTilePosition, out var touchingTile);
        //Get all possible tile locations based on touchingTile
        var borderingPlacements = new List<TilePosition>();
        for(int i = 0; i < touchingTile.xSize; i++){
            var xPos = touchingTile.tilePosition.x + i;
            borderingPlacements.Add(new TilePosition(xPos, touchingTile.tilePosition.y - 1));
            borderingPlacements.Add(new TilePosition(xPos, touchingTile.tilePosition.y + touchingTile.ySize));
        }
        for(int i = 0; i < touchingTile.ySize; i++){
            var yPos = touchingTile.tilePosition.y + i;
            borderingPlacements.Add(new TilePosition(touchingTile.tilePosition.x - 1, yPos));
            borderingPlacements.Add(new TilePosition(touchingTile.tilePosition.x + touchingTile.xSize, yPos));
        }

        //remove locations taken in existing tiles
        var possiblePlacements = new List<TilePosition>();
        foreach(var borderingPlacement in borderingPlacements){
            if(!existingTiles.TryGetValue(borderingPlacement, out var _)){
                possiblePlacements.Add(borderingPlacement);
            }
        }
        //leaves us with possible locations

        //try to "place" each tile on each possible location, and check with size if full tile can fit
        if(!exists){
            return CreateTile(currentTilePosition, tilePrefabs[Random.Range(0, tilePrefabs.Count)]);
        }
        else{
            if(CanTilesTouch(touchingTile, selectedTile.GetComponent<Tile>(), doorWall, newTilePoisition, out var trueTilePosition)){
                return CreateTile(trueTilePosition, selectedTile);
            }
        }
        return null;
    }

    private GameObject CreateTile(TilePosition tilePosition, GameObject tileToCreate){
        var pos = transform.position;
        pos.x += tileOffset * tilePosition.x;
        pos.y += tileOffset * tilePosition.y;
        var tile1 = Instantiate(tileToCreate, tileParent.transform);
        tile1.transform.position = pos;
        tile1.transform.localScale = new Vector3(1, 1, 1);
        tile1.SetActive(true);
        tile1.GetComponent<Tile>().tilePosition = tilePosition;
        tile1.GetComponentInChildren<SelectGlow>().isSelected = false;
        var tileComp = tile1.GetComponent<Tile>();
        Debug.Log("xTile:"+tilePosition.x +" yTile:"+ tilePosition.y);
        for(int i = 0; i < tileComp.xSize; i++){
            for(int j = 0; j < tileComp.ySize; j++){
                var newTilePosition = new TilePosition(tilePosition.x + i, tilePosition.y + j);
                existingTiles.Add(newTilePosition, tileComp);
            }
        }
        
        return tile1;
    }

    private bool CanTilesTouch(Tile tileOne, Tile tileTwo, DoorWall doorWall, TilePosition newTilePosition, out TilePosition tilePosition){
        var cont = false;
        foreach(var door in tileOne.doors.Where(d => d.doorWall == doorWall)){
            foreach(var doorTwo in tileTwo.doors){
                cont = false;
                var isMatch = door.IsMatch(doorTwo);
                if(isMatch){
                    var tileY = doorTwo.yPos / doorTwo.tileOffset;
                    var tileX = doorTwo.xPos / doorTwo.tileOffset;
                    var doorOnetileY = door.yPos / door.tileOffset;
                    var doorOnetileX = door.xPos / door.tileOffset;
                    Debug.Log("tileOnePosX:" + newTilePosition.x + " - tile x: " + tileX + " - d1tileX: "+ doorOnetileX);
                    Debug.Log("tileOnePosY:" + newTilePosition.y + " - tile y: " + tileY + " - d1tileY: "+ doorOnetileY);
                    tilePosition = new TilePosition(newTilePosition.x - tileX + doorOnetileX, newTilePosition.y - tileY + doorOnetileY);
                    Debug.Log("tileposx:" +tilePosition.x + " - tileposy:" + tilePosition.y);
                    for(int i = 0; i < tileTwo.xSize; i++){
                        for(int j = 0; j < tileTwo.ySize; j++){
                            if(existingTiles.TryGetValue(new TilePosition(tilePosition.x + i, tilePosition.y + j), out var _)){
                                Debug.Log("tile found at " + (tilePosition.x + i)+ "," + (tilePosition.y + j));
                                cont = true;
                                continue;
                            }
                        }
                        if(cont) continue;
                    }
                    if(cont) continue;
                    return true;
                } 
            }
        }
        tilePosition = null;
        return false;
    }
}
