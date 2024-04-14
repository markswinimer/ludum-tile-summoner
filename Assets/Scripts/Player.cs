using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isPlayerControllable;
    // public bool isPlayerControllable { get; private set; }
    public Tile currentTile;
    public Tile checkpoint;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerControllable = true;
        currentTile = FindFirstObjectByType<Tile>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead){
            //respawn at last shrine
            transform.position = checkpoint.GetComponentInChildren<SpawnPoint>().transform.position;
            isDead = false;
        }
        if(checkpoint == null) checkpoint = currentTile;
    }

    public void ChangePlayerControl(){
        isPlayerControllable = !isPlayerControllable;
    }
}
