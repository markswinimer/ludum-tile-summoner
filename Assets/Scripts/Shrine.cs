using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Choose(ShrineChoice shrineChoice){
        switch (shrineChoice.shrineItemType){
            case ShrineItemType.Guitar:
                inventoryManager.guitar = shrineChoice.guitar;
                break;
            case ShrineItemType.Summon:
                inventoryManager.AddSummon(shrineChoice.summon);
                break;
        }

        Destroy(gameObject);
    }
}
