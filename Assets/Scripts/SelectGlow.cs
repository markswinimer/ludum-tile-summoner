using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGlow : MonoBehaviour
{
    public bool isSelected = false;
    private SpriteRenderer spriteRenderer;

    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected){
            spriteRenderer.sprite = sprite;
        }
        else{
            spriteRenderer.sprite = null;
        }
    }
}
