using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SummonController : MonoBehaviour
{
    public bool isSummonMode; 
    private SummonPlacer summonPlacer;
    // Start is called before the first frame update
    void Start()
    {
        summonPlacer = FindFirstObjectByType<SummonPlacer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSummonMode){
            if(Input.GetMouseButtonDown(0)){
                var summon = summonPlacer.GetSummon();
                summon.SetActive(true);
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                summon.transform.position = pos;
                summon.GetComponent<SummonBase>().isPlaced = true;
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
}
