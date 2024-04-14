using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    public bool isSelected = false;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite;
    private Animator animator;
    public float yOffset = -7f;
    private int ySize = 1;

    private bool isSummon = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if(GetComponentInParent<Tile>() != null){
            isSummon = false;
            ySize = GetComponentInParent<Tile>().ySize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected){
            spriteRenderer.sprite = sprite;
            var pos = transform.position;
            pos.y = yOffset - (0.75f * (ySize - 1));
            transform.position = pos;
            if(isSummon) transform.localScale = new Vector2(0.2f, 0.2f);
            animator.Play("selectGlow_Clip");
            animator.enabled = true;
        }
        else{
            spriteRenderer.sprite = null;
            animator.StopPlayback();
            animator.enabled = false;
        }
    }
}
