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
        for(int i = 1; i < 10; i++){
            CreateTile(new TilePosition(i, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateTile(TilePosition tilePosition){
        //TODO: Check all surrounding tiles, not just left tile
        var exists = existingTiles.TryGetValue(new TilePosition(tilePosition.x - 1, tilePosition.y), out var touchingTile);
        if(exists){
            Debug.Log("First");
            GameObject tileToCreate = null;
            if(exists){
                var foundTile = false;
                while(!foundTile){
                    Debug.Log("Third");
                    var potentialTile = tilePrefabs[Random.Range(0, tilePrefabs.Count)];
                    if(CanTilesTouch(touchingTile, potentialTile.GetComponent<Tile>())){
                        Debug.Log("Second");
                        tileToCreate = potentialTile;
                        foundTile = true;
                    }
                }
            }
            CreateTile(tilePosition, tileToCreate);
        }
    }

    private void CreateTile(TilePosition tilePosition, GameObject tileToCreate){
        var pos = transform.position;
        pos.x += tileOffset * tilePosition.x;
        pos.y += tileOffset * tilePosition.y;
        var tile1 = Instantiate(tileToCreate, tileParent.transform);
        tile1.transform.position = pos;
        tile1.SetActive(true);
        existingTiles.Add(tilePosition, tile1.GetComponent<Tile>());
    }

    private bool CanTilesTouch(Tile tileOne, Tile tileTwo){
        foreach(var door in tileOne.doors){
            foreach(var doorTwo in tileTwo.doors){
                var isMatch = door.IsMatch(doorTwo);
                if(isMatch) return true;
            }
        }
        return false;
    }
}
