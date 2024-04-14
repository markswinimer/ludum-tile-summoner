using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineChoice : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Shrine shrine;
    public ShrineItemType shrineItemType;
    public Guitar guitar;
    public Summon summon;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        shrine = GetComponentInParent<Shrine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Handle mouse click on this object
                shrine.Choose(this);
            }
        }
    }
}
