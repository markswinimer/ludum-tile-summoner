using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonPlacer : MonoBehaviour
{
    public List<SpriteRenderer> renderers;
    public List<GameObject> summonsToPlace;
    public List<GameObject> summonPrefabs;
    private Player player;
    private int tileOffset = 10;
    private int xOffset = -14;
    private float yOffset = -3;
    private float xWidth;
    private int selectedSummonIndex;
    private GhostRenderer ghostRenderer;
    private InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
        ghostRenderer = GetComponentInChildren<GhostRenderer>();
        renderers.ForEach(r => r.enabled = false);
        summonPrefabs = FindFirstObjectByType<SummonPrefabs>().GetComponentsInChildren<SummonBase>(true).Select(r => r.gameObject).ToList();
        player = FindFirstObjectByType<Player>();
        var rect = GetComponent<RectTransform>();
        xWidth = rect.sizeDelta.x;
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private List<GameObject> GetSummons(){
        return summonPrefabs.Where(s => inventoryManager.summonsAquiredEnum.Contains(s.GetComponent<SummonBase>().summon)).ToList();
    }

    public void Enable(){
        renderers.ForEach(r => r.enabled = true);
        foreach(var summon in GetSummons()){
            summon.GetComponent<SummonBase>().isPlaced = false;
        }
        SetupSummons();
    }

    public void Disable(){
        renderers.ForEach(r => r.enabled = false);
        foreach(var summon in GetSummons()){
            if(!summon.GetComponent<SummonBase>().isPlaced){
                summon.SetActive(false);
            }
        }
    }

    private void SetupSummons(){
        foreach(var summon in GetSummons()){
            summon.SetActive(true);
            var pos = player.currentTile.transform.position;
            pos.x -= tileOffset;
            summon.transform.position = pos;
            summon.transform.localScale = new Vector3(1, 1, 1);
            summon.SetActive(true);
            summonsToPlace.Add(summon);
            summon.layer = 5;
            summon.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "UI";
        }
        SelectSummon(0);
        SetSummonPositions();
    }

    private void SetSummonPositions(){
        transform.position = new Vector3(player.currentTile.transform.position.x + xOffset, player.currentTile.transform.position.y + yOffset);
        var startingX = player.currentTile.transform.position.x - 16;
        var summonsToPlace = GetSummons().Where(s => !s.GetComponent<SummonBase>().isPlaced).ToList();
        var yPosition = transform.position.y;
        var xTileOffset = xWidth / (summonsToPlace.Count() + 1);
        for(int i = 0; i < summonsToPlace.Count(); i++){
            var xPos = startingX + (xTileOffset * (i + 1));
            summonsToPlace[i].transform.position = new Vector3(xPos, yPosition);
        }
    }

    private void SelectSummon(int index){
        Debug.Log("index:" + selectedSummonIndex);
        var summonsToPlace = GetSummons().Where(s => !s.GetComponent<SummonBase>().isPlaced).ToList();
        if(summonsToPlace.Count - 1 >= selectedSummonIndex){
            summonsToPlace[selectedSummonIndex].GetComponentInChildren<SelectGlow>().isSelected = false;
        }
        selectedSummonIndex = index;
        summonsToPlace[index].GetComponentInChildren<SelectGlow>().isSelected = true;
        ghostRenderer.SetGhost(summonsToPlace[index].GetComponent<SummonBase>().sprite);
    }

    public GameObject GetSummon(){
        return GetSummons().Where(s => !s.GetComponent<SummonBase>().isPlaced).ToList()[selectedSummonIndex];
    }

    public bool HasNoSummons(){
        var hasNoSummons = GetSummons().Where(s => !s.GetComponent<SummonBase>().isPlaced).Count() <= 0;
        if(hasNoSummons){
            foreach(var summon in GetSummons()){
                summon.GetComponentInChildren<SelectGlow>().isSelected = false;
            }
        }
        return hasNoSummons;
    }
}
