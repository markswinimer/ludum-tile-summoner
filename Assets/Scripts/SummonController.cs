using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class SummonController : MonoBehaviour
{
    public bool isSummonMode; 
    private SummonPlacer summonPlacer;
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        summonPlacer = FindFirstObjectByType<SummonPlacer>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSummonMode){
            if(Input.GetMouseButtonDown(0)){
                var summon = summonPlacer.GetSummon();
                summon.SetActive(true);
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var summonComp = summon.GetComponent<SummonBase>();
                if(summonComp.summon == Summon.Platform){
                    summon.layer = 3;
                    summon.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Ground";
                }
                pos.z = 0;
                summon.transform.position = pos;
                summonComp.SetPlaced();
            }
        }
    }

    public void ChangeControl(){
        if(isSummonMode){
            //turn off
            isSummonMode = false;
            summonPlacer.Disable();
        }
        else{
            //turn on
            isSummonMode = true;
            summonPlacer.Enable();
        }
    }

    public bool HasNoSummons(){
        return summonPlacer.HasNoSummons();
    }

    public bool HasNoSummonsInInventory(){
        return inventoryManager.summonsAquired.Count == 0;
    }
}
