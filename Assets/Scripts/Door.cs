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
    }

    public void SetSize(Tile tile){
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
                otherModEnd = otherModStart + door.yLength;
                return door.doorWall == DoorWall.Right && modStart < otherModEnd && otherModStart < modEnd;
            case DoorWall.Right:
                modStart = yPos % tileOffset;
                modEnd = modStart + yLength;
                otherModStart = door.yPos % tileOffset;
                otherModEnd = otherModStart + door.yLength;
                Debug.Log("door1 wall=" +doorWall.ToString() +" door2 wall="+door.doorWall.ToString() + ", modStart " + modStart.ToString()+" <= oModEnd "+ otherModEnd.ToString() + " & otherModStart " + otherModStart.ToString() + " <= modEnd " + modEnd.ToString());
                return door.doorWall == DoorWall.Left && modStart < otherModEnd && otherModStart < modEnd;
            case DoorWall.Top:
                modStart = xPos % tileOffset;
                modEnd = modStart + xLength;
                otherModStart = door.xPos % tileOffset;
                otherModEnd = otherModStart + door.xLength;
                return door.doorWall == DoorWall.Bottom && modStart < otherModEnd && otherModStart < modEnd;
            case DoorWall.Bottom:
                modStart = yPos % tileOffset;
                modEnd = modStart + yLength;
                otherModStart = door.yPos % tileOffset;
                otherModEnd = otherModStart + door.yLength;
                return door.doorWall == DoorWall.Top && modStart < otherModEnd && otherModStart < modEnd;
            default:
                return false;
        }
    }

    public bool IsOpposite(DoorWall otherDoorWall){
        switch(doorWall){
            case DoorWall.Left:
                return otherDoorWall == DoorWall.Right;
            case DoorWall.Right:
                return otherDoorWall == DoorWall.Left;
            case DoorWall.Top:
                return otherDoorWall == DoorWall.Bottom;
            case DoorWall.Bottom:
                return otherDoorWall == DoorWall.Top;
            default:
                return false;
        }
    }
}