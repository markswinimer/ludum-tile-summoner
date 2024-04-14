using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class DoubleJumpSummon : SummonBase
{
    private PlayerMovement playerMovement;
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
    }
}
