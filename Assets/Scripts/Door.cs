using UnityEngine;

public class Door : MonoBehaviour {
    public int xPos;
    public int yPos;
    public int xLength;
    public int yLength;
    public DoorWall doorWall;
    public int yTileSize;
    public int xTileSize;
    public int tileOffset = 10;

    private void Start() {
        Debug.Log("Door Start");
        var tile = GetComponentInParent<Tile>();
        yTileSize = tile.ySize;
        xTileSize = tile.xSize;
    }

    public bool IsMatch(Door door){
        //TODO: Need to update to account for length
        var modStart = 0;
        var modEnd = 0;
        var otherModStart = 0;
        var otherModEnd = 0;
        switch(doorWall){
            case DoorWall.Left:
                modStart = yPos % tileOffset;
                modEnd = modStart + yLength;
                otherModStart = door.yPos % tileOffset;
                otherModEnd = otherModStart + yLength;
                return door.doorWall == DoorWall.Right && modStart <= otherModEnd && otherModStart <= modEnd;
            case DoorWall.Right:
                modStart = yPos % tileOffset;
                modEnd = modStart + yLength;
                otherModStart = door.yPos % tileOffset;
                otherModEnd = otherModStart + yLength;
                return door.doorWall == DoorWall.Left && modStart <= otherModEnd && otherModStart <= modEnd;
            case DoorWall.Top:
                modStart = xPos % tileOffset;
                modEnd = modStart + xLength;
                otherModStart = door.xPos % tileOffset;
                otherModEnd = otherModStart + xLength;
                return door.doorWall == DoorWall.Bottom && modStart <= otherModEnd && otherModStart <= modEnd;
            case DoorWall.Bottom:
                modStart = yPos % tileOffset;
                modEnd = modStart + yLength;
                otherModStart = door.yPos % tileOffset;
                otherModEnd = otherModStart + yLength;
                return door.doorWall == DoorWall.Top && modStart <= otherModEnd && otherModStart <= modEnd;
            default:
                return false;
        }
    }
}