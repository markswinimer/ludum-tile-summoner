using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TilePlacer : MonoBehaviour
{
    public bool isControllable { get; private set; }
    public Player player;
    private Tile currentTile;
    private TileMaster tileMaster;
    public CinemachineVirtualCamera virtualCamera;
    private List<GameObject> tilesToPlace;
    public int numberOfTileOptions = 5;
    public int tilesToPlaceMaxCount = 3;
    public int tilesToPlaceCount = 3;
    private int tileOffset = 20;
    private int yLength = 40;
    private int selectedTileIndex;
    
    public PlayerInputActions playerControls;
    private InputAction placeTileLeft;
    private InputAction placeTileRight;
    private InputAction placeTileDown;
    private InputAction placeTileUp;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        tileMaster = FindFirstObjectByType<TileMaster>();
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
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
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x, currentTile.tilePosition.y + 1), DoorWall.Top);
    }

    private void PlaceTileDown(InputAction.CallbackContext context){
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x, currentTile.tilePosition.y - 1), DoorWall.Bottom);
    }

    private void PlaceTileRight(InputAction.CallbackContext context){
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x + 1, currentTile.tilePosition.y), DoorWall.Right);
    }

    private void PlaceTileLeft(InputAction.CallbackContext context){
        TryCreateNewTile(new TilePosition(currentTile.tilePosition.x - 1, currentTile.tilePosition.y), DoorWall.Left);
    }

    public void ChangeControl(){
        isControllable = !isControllable;
        currentTile = player.currentTile;
        if(!isControllable) return;
        CreateFakeTiles();
    }

    private void TryCreateNewTile(TilePosition newTilePosition, DoorWall doorWall){
        if(tilesToPlaceCount <= 0) return;
        var newTile = tileMaster.CreateTile(currentTile.tilePosition, newTilePosition, doorWall, tilesToPlace[selectedTileIndex]);
        if(newTile == null) return;
        currentTile = newTile.GetComponent<Tile>();
        virtualCamera.Follow = currentTile.transform;
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
        }
        SelectTile(0);
        tilesToPlaceCount = tilesToPlaceMaxCount;
        SetTilePositions();
    }

    private void SetTilePositions(){
        var yOffset = yLength / (tilesToPlace.Count + 1);
        var yPosStart = currentTile.transform.position.y - 20;
        for(int i = 0; i < tilesToPlace.Count; i++){
            var yPos = yPosStart + (yOffset * (i + 1));
            tilesToPlace[i].transform.position = new Vector3(currentTile.transform.position.x - tileOffset, yPos);
        }
    }

    private void SelectTile(int index){
        tilesToPlace[selectedTileIndex].GetComponentInChildren<SelectGlow>().isSelected = false;
        selectedTileIndex = index;
        tilesToPlace[index].GetComponentInChildren<SelectGlow>().isSelected = true;
    }
}
