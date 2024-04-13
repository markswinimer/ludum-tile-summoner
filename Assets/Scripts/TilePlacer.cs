using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    public bool isControllable { get; private set; }
    public Player player;
    public Tile currentTile;
    private TileMaster tileMaster;
    public CinemachineVirtualCamera virtualCamera;
    private List<GameObject> tilesToPlace;
    public int numberOfTileOptions = 5;
    public int tilesToPlaceMaxCount = 3;
    public int tilesToPlaceCount = 3;
    private int tileOffset = 10;
    private int xOffset = -14;
    private int yOffset = -8;
    private int selectedTileIndex;
    
    public PlayerInputActions playerControls;
    private InputAction placeTileLeft;
    private InputAction placeTileRight;
    private InputAction placeTileDown;
    private InputAction placeTileUp;
    private SpriteRenderer spriteRenderer;
    private float xWidth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        tileMaster = FindFirstObjectByType<TileMaster>();
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.enabled = false;
        var rect = GetComponent<RectTransform>();
        xWidth = rect.sizeDelta.x;
        tilesToPlace = new List<GameObject>();
        playerControls = new PlayerInputActions();
        placeTileLeft = playerControls.Player.PlaceTileLeft;
        placeTileLeft.Enable();
        placeTileLeft.performed += PlaceTileLeft;
        placeTileRight = playerControls.Player.PlaceTileRight;
        placeTileRight.Enable();
        placeTileRight.performed += PlaceTileRight;
        placeTileDown = playerControls.Player.PlaceTileDown;
        placeTileDown.Enable();
        placeTileDown.performed += PlaceTileDown;
        placeTileUp = playerControls.Player.PlaceTileUp;
        placeTileUp.Enable();
        placeTileUp.performed += PlaceTileUp;
    }

    // Update is called once per frame
    void Update()
    {
        if(isControllable){
            for(int i = 0; i < tilesToPlace.Count; i++){
                if(Input.GetKeyDown((i + 1).ToString())){
                    SelectTile(i);
                }
            }
        }
        if(tilesToPlace.Count > 0 && tilesToPlaceCount <= 0){
            for(int i = 0; i < tilesToPlace.Count; i++){
                Destroy(tilesToPlace[i].gameObject);
            }
            tilesToPlace = new List<GameObject>();
        }
    }

    private void PlaceTileUp(InputAction.CallbackContext context){
        if(!isControllable) return;
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x, currentTile.tilePosition.y + 1), DoorWall.Top);
    }

    private void PlaceTileDown(InputAction.CallbackContext context){
        if(!isControllable) return;
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x, currentTile.tilePosition.y - 1), DoorWall.Bottom);
    }

    private void PlaceTileRight(InputAction.CallbackContext context){
        if(!isControllable) return;
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x + 1, currentTile.tilePosition.y), DoorWall.Right);
    }

    private void PlaceTileLeft(InputAction.CallbackContext context){
        if(!isControllable) return;
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x - 1, currentTile.tilePosition.y), DoorWall.Left);
    }

    public void ChangeControl(){
        isControllable = !isControllable;
        if(currentTile == null){
            currentTile = player.currentTile;
        }
        
        if(tilesToPlace.Count > 0){
            for(int i = 0; i < tilesToPlace.Count; i++){
                Destroy(tilesToPlace[i].gameObject);
            }
            tilesToPlace = new List<GameObject>();
        }
        if(!isControllable){
            spriteRenderer.enabled = false;
            return;
        } 
        CreateFakeTiles();
        spriteRenderer.enabled = true;
    }

    private void TryCreateNewTile(TilePosition newTilePosition, DoorWall doorWall){
        if(tilesToPlaceCount <= 0) return;
        var newTile = tileMaster.CreateTile(currentTile.tilePosition, newTilePosition, doorWall, tilesToPlace[selectedTileIndex]);
        if(newTile == null) return;
        currentTile = newTile.GetComponent<Tile>();
        virtualCamera.Follow = currentTile.GetComponentInChildren<PlayerDetector>().transform;
        tilesToPlaceCount--;
        var tileToDestroy = tilesToPlace[selectedTileIndex];
        Destroy(tileToDestroy);
        tilesToPlace.Remove(tileToDestroy);
        SelectTile(0);
        SetTilePositions();
    }

    private void CreateFakeTiles(){
        for(int i = 0; i < numberOfTileOptions; i++){
            var tileToCreate = tileMaster.tilePrefabs[Random.Range(0, tileMaster.tilePrefabs.Count)];
            var pos = currentTile.transform.position;
            pos.x -= tileOffset;
            var tile1 = Instantiate(tileToCreate, transform);
            tile1.transform.position = pos;
            tile1.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            tile1.SetActive(true);
            Debug.Log("tileCount="+tileMaster.tilePrefabs.Count);
            tilesToPlace.Add(tile1);
            tile1.layer = 5;
            tile1.GetComponentInChildren<TilemapRenderer>().sortingLayerName = "UI";
        }
        SelectTile(0);
        tilesToPlaceCount = tilesToPlaceMaxCount;
        SetTilePositions();
    }

    private void SetTilePositions(){
        transform.position = new Vector3(currentTile.transform.position.x + xOffset, currentTile.transform.position.y + yOffset);
        var startingX = currentTile.transform.position.x - 16;
        var tileCount = tilesToPlace.Count;
        var yPosition = transform.position.y;
        var xTileOffset = xWidth / (tileCount + 1);
        for(int i = 0; i < tileCount; i++){
            var xPos = startingX + (xTileOffset * (i + 1));
            tilesToPlace[i].transform.position = new Vector3(xPos, yPosition - ((tilesToPlace[i].GetComponent<Tile>().ySize - 1) * 1.5f));
        }
    }

    private void SelectTile(int index){
        Debug.Log("index:" + selectedTileIndex);
        if(tilesToPlace.Count - 1 >= selectedTileIndex){
            tilesToPlace[selectedTileIndex].GetComponentInChildren<SelectGlow>().isSelected = false;
        }
        selectedTileIndex = index;
        tilesToPlace[index].GetComponentInChildren<SelectGlow>().isSelected = true;
    }
}
