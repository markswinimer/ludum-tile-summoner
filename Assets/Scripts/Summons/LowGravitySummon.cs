using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class LowGravitySummon : SummonBase
{
    private PlayerMovement playerMovement;

    private bool playedSoundThisJump;

    private bool powerInUse;

    // Start is called before the first frame update
    public override void SummonStart()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        proximity = 5.5f;
        summon = Summon.LowGravity;
    }

    public override void SummonOnDisable()
    {
        //SetNormGrav
        playerMovement.lowGravMultiplier = 1f;
        playedSoundThisJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canUsePower){
            powerInUse = true;
        }
        if(!playedSoundThisJump && canUsePower && playerMovement.isInTheAir) StartCoroutine(HandleSound());

        if(canUsePower || (powerInUse && playerMovement.isInTheAir)){
            playerMovement.lowGravMultiplier = 0.5f;
        }
        else{
            playerMovement.lowGravMultiplier = 1f;
            powerInUse = false;
        }
    }

    private IEnumerator HandleSound(){
        PlaySound();
        playedSoundThisJump = true;
        while(playerMovement.isInTheAir){
            yield return new WaitForEndOfFrame();
        }
        playedSoundThisJump = false;
        audioSource.Stop();
    }
}
