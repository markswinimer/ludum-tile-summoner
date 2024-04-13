using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int xSize;
    public int ySize;
    public List<Door> doors;
    // Start is called before the first frame update
    void Start()
    {
        doors = GetComponentsInChildren<Door>().ToList();
        Debug.Log("Door Count:"+doors.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
