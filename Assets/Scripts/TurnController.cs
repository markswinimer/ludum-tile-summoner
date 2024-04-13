using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnController : MonoBehaviour
{
    public Player player;
    public TilePlacer tilePlacer;
    public CinemachineVirtualCamera virtualCamera;
    public float playerLensSize = 9;
    public float tilePlacementLensSize = 20;
    public PlayerInputActions playerControls;
    private InputAction switchPlayMode;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
        tilePlacer = FindFirstObjectByType<TilePlacer>();
        virtualCamera = FindFirstObjectByType<CinemachineVirtualCamera>();
        playerControls = new PlayerInputActions();
        switchPlayMode = playerControls.Player.SwitchPlayMode;
        switchPlayMode.Enable();
        switchPlayMode.performed += ChangeControl;
        Debug.Log("FinishedTurnSetup");
    }

    // Update is called once per frame
    void Update()
    {
        if(tilePlacer.isControllable && tilePlacer.tilesToPlaceCount <= 0){
            ChangeControl();
        }
    }

    private void ChangeControl(InputAction.CallbackContext context){
        ChangeControl();
    }

    private void ChangeControl(){
        Debug.Log("HitChangeControl");
        player.ChangePlayerControl();
        tilePlacer.ChangeControl();
        //TODO: Smooth out camera movement;
        virtualCamera.m_Lens.OrthographicSize = player.isPlayerControllable ? playerLensSize : tilePlacementLensSize;
        virtualCamera.Follow = player.isPlayerControllable ? player.transform : player.currentTile.transform;
    }
}
