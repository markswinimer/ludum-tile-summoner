using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class SummonBase : MonoBehaviour
{
    public float expirationTimer { get; set; }
    public int interactionCount { get; set; }
    public bool isPlaced;
    public Sprite sprite;

    private SpriteRenderer spriteRenderer;
    public Player player;
    public virtual float proximity { get; set;}

    public bool canUsePower;


    // Start is called before the first frame update
    void Start()
    {
        isPlaced = false;
        player = FindFirstObjectByType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        proximity = 1f;
        gameObject.SetActive(false);
    }

    public virtual void SummonStart(){

    }

    private void OnEnable() {
        StartCoroutine(CheckProximity());
    }

    private void OnDisable() {
        StopCoroutine(CheckProximity());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual IEnumerator CheckProximity(){
        while(true){
            var distance = Vector2.Distance(player.gameObject.transform.position, transform.position);
            if(distance < proximity){
                canUsePower = true;
                Debug.Log("Can use power");
            }
            else{
                canUsePower = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
