using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;

public class SummonBase : MonoBehaviour
{
    public float expirationTimer { get; set; }
    private float currentExpirationTimer;
    public int interactionCount { get; set; }
    public bool isPlaced;
    public Sprite sprite;

    private SpriteRenderer spriteRenderer;
    public Player player;
    public virtual float proximity { get; set;}

    public bool canUsePower;

    private TurnController turnController;

    public Summon summon;

    public AudioResource playOnUseSound;
    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        isPlaced = false;
        player = FindFirstObjectByType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        turnController = FindFirstObjectByType<TurnController>();
        spriteRenderer.sprite = sprite;
        proximity = 1f;
        expirationTimer = 10;
        audioSource = GetComponent<AudioSource>();
        audioSource.resource = playOnUseSound;
        SummonStart();
        gameObject.SetActive(false);
    }

    public virtual void SummonStart(){

    }

    private void OnEnable() {
        StartCoroutine(CheckProximity());
        StartCoroutine(ExpireAfterTime());
    }

    private void OnDisable() {
        StopCoroutine(CheckProximity());
        SummonOnDisable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SummonOnDisable(){

    }

    public virtual IEnumerator CheckProximity(){
        while(true){
            var distance = Vector2.Distance(player.gameObject.transform.position, transform.position);
            if(distance < proximity){
                canUsePower = true;
            }
            else{
                canUsePower = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ExpireAfterTime(){
        //wait for player to finish summoning
        currentExpirationTimer = 0;
        while(turnController.currentPlayMode == PlayMode.Summon){
            yield return new WaitForEndOfFrame();
        }
        while(currentExpirationTimer <= expirationTimer){
            currentExpirationTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        canUsePower = false;
        gameObject.SetActive(false);

    }

    public void SetPlaced(){
        isPlaced = true;
        currentExpirationTimer = 0;
    }

    public virtual void PlaySound(){
        audioSource.Play();
    }
}
