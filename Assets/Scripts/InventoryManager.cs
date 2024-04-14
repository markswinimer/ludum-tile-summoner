using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<SummonBase> summons;
    public List<SummonBase> summonsAquired { get; private set; }
    public Guitar guitar;
    // Start is called before the first frame update
    void Start()
    {
        summons = FindObjectsByType<SummonBase>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        summonsAquired = new List<SummonBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSummon(Summon summon){
        if(summonsAquired.FirstOrDefault(s => s.summon == summon) == null){
            summonsAquired.Add(summons.First(s => s.summon == summon));
        }
    }
}
