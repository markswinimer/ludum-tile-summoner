using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerControllable;
    // public bool isPlayerControllable { get; private set; }
    public Tile currentTile;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerControllable = true;
        currentTile = FindFirstObjectByType<Tile>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangePlayerControl(){
        isPlayerControllable = !isPlayerControllable;
    }
}
