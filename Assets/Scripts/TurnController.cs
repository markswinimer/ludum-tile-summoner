using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    public Player player;
    public TilePlacer tilePlacer;
    public SummonController summonController;
    public CinemachineVirtualCamera virtualCamera;
    public float playerLensSize = 4.8f;
    public float tilePlacementLensSize = 20;
    public float summonPlacementLensSize = 9;
    public PlayMode currentPlayMode;
    public PlayerInputActions playerControls;
    private InputAction switchPlayMode;
    private InputAction switchSummonMode;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        tilePlacer = FindFirstObjectByType<TilePlacer>();
        summonController = FindFirstObjectByType<SummonController>();
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        playerControls = new PlayerInputActions();
        switchPlayMode = playerControls.Player.SwitchPlayMode;
        switchPlayMode.Enable();
        switchPlayMode.performed += ChangeControlTile;
        switchSummonMode = playerControls.Player.SwitchSummonMode;
        switchSummonMode.Enable();
        switchSummonMode.performed += ChangeControlSummon;
        currentPlayMode = PlayMode.Player;
        Debug.Log("FinishedTurnSetup");
    }

    // Update is called once per frame
    void Update()
    {
        if(tilePlacer.isControllable && tilePlacer.tilesToPlaceCount <= 0){
            ChangeControl(PlayMode.Player);
        }
        if(summonController.isSummonMode && summonController.HasNoSummons()){
            ChangeControl(PlayMode.Player);
        }
    }

    private void ChangeControlTile(InputAction.CallbackContext context){
        var mode = currentPlayMode == PlayMode.Tile ? PlayMode.Player : PlayMode.Tile;
        ChangeControl(mode);
    }

    private void ChangeControlSummon(InputAction.CallbackContext context){
        var mode = currentPlayMode == PlayMode.Summon ? PlayMode.Player : PlayMode.Summon;
        if(mode == PlayMode.Summon && summonController.HasNoSummonsInInventory()) return;
        ChangeControl(mode);
    }

    private void ChangeControl(PlayMode playMode){
        Debug.Log("HitChangeControl");
        switch(currentPlayMode){
            case PlayMode.Player:
                player.ChangePlayerControl();
                break;
            case PlayMode.Tile:
                tilePlacer.ChangeControl();
                break;
            case PlayMode.Summon:
                summonController.ChangeControl();
                break;
        }

        switch(playMode){
            case PlayMode.Player:
                player.ChangePlayerControl();
                virtualCamera.m_Lens.OrthographicSize = playerLensSize;
                virtualCamera.Follow = player.transform;
                break;
            case PlayMode.Tile:
                tilePlacer.ChangeControl();
                virtualCamera.m_Lens.OrthographicSize = tilePlacementLensSize;
                virtualCamera.Follow = tilePlacer.currentTile.transform;
                break;
            case PlayMode.Summon:
                summonController.ChangeControl();
                virtualCamera.m_Lens.OrthographicSize = summonPlacementLensSize;
                virtualCamera.Follow = player.currentTile.GetComponentInChildren<PlayerDetector>().transform;
                break;
        }
        currentPlayMode = playMode;
        //TODO: Smooth out camera movement;
    }
}
