using UnityEngine;

public class Door : MonoBehaviour {
    public int xPos;
    public int yPos;
    public int xLength;
    public int yLength;
    public DoorWall doorWall;

    private void Start() {
        Debug.Log("Door Start");
    }

    public bool IsMatch(Door door){
        //TODO: Need to update to account for length
        switch(doorWall){
            case DoorWall.Left:
                return door.doorWall == DoorWall.Right && door.yPos == yPos;
            case DoorWall.Right:
                return door.doorWall == DoorWall.Left && door.yPos == yPos;
            case DoorWall.Top:
                return door.doorWall == DoorWall.Bottom && door.xPos == xPos;
            case DoorWall.Bottom:
                return door.doorWall == DoorWall.Top && door.xPos == xPos;
            default:
                return false;
        }
    }
}