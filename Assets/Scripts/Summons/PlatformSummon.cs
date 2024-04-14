using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class PlatformSummon : SummonBase
{
    private PlayerMovement playerMovement;

    private bool playedSoundThisJump;
    private bool playerInContact;
    // Start is called before the first frame update
    public override void SummonStart()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        proximity = 2f;
        summon = Summon.Platform;
    }

    public override void SummonOnDisable()
    {
        playerMovement.canDoubleJump = false;
        playedSoundThisJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canUsePower){
            playerMovement.canDoubleJump = true;
        }
        else{
            playerMovement.canDoubleJump = false;
        }
        if(playerMovement.cooldownDoubleJump && !playedSoundThisJump) StartCoroutine(HandleSound());
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision with platform summon");
        if(other.gameObject.GetComponent<Player>() != null){
            StartCoroutine(HandleSound());
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.GetComponent<Player>() != null){
            playerInContact = false;
        }
    }

    private IEnumerator HandleSound(){
        PlaySound();
        playerInContact = true;
        while(playerInContact){
            yield return new WaitForEndOfFrame();
        }
        audioSource.Stop();
    }
}
