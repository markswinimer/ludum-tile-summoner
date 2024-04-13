using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.enabled){
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            transform.position = pos;
        }
    }

    public void SetGhost(Sprite sprite){
        spriteRenderer.sprite = sprite;
    }
}
