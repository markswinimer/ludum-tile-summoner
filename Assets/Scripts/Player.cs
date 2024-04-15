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
    private Animator animator;

    public RuntimeAnimatorController basicAnimator;
    public RuntimeAnimatorController redAnimator;
    public RuntimeAnimatorController yellowAnimator;
    public RuntimeAnimatorController tealAnimator;
    // Start is called before the first frame update
    void Start()
    {
        isPlayerControllable = true;
        currentTile = FindFirstObjectByType<Tile>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = basicAnimator;
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

    public void SetAnimator(Guitar guitar){
        switch(guitar){
            case Guitar.Bass:
                animator.runtimeAnimatorController = redAnimator;
                break;
            case Guitar.Acoustic:
                animator.runtimeAnimatorController = yellowAnimator;
                break;
            case Guitar.Electric:
                animator.runtimeAnimatorController = tealAnimator;
                break;
            case Guitar.None:
                animator.runtimeAnimatorController = basicAnimator;
                break;
            default:
                animator.runtimeAnimatorController = basicAnimator;
                break;
        }
    }
}
