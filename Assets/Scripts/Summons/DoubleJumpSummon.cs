using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR;

public class DoubleJumpSummon : SummonBase
{
    private PlayerMovement playerMovement;

    private bool playedSoundThisJump;
    // Start is called before the first frame update
    public override void SummonStart()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        proximity = 2f;
        summon = Summon.DoubleJump;
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

    private IEnumerator HandleSound(){
        PlaySound();
        playedSoundThisJump = true;
        while(playerMovement.cooldownDoubleJump){
            yield return new WaitForEndOfFrame();
        }
        playedSoundThisJump = false;
    }
}
