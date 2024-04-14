using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneDetector : MonoBehaviour
{

    public Player player;
    public float yOffset = -12;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponentInParent<Player>() != null){
            Debug.Log("Player fell and is dead");
            player.isDead = true;
        }
    }

    public void SetNewPosition(Vector3 position){
        position.y += yOffset;
        transform.position = position;
    }
}
