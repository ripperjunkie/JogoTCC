using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMaster : MonoBehaviour
{
    public EMovementState movementState;

    [SerializeField] private GameObject _canvasPrefab;
    public CanvasMaster canvasMaster;


    [SerializeField] private bool _isDebug;
    private Rigidbody _rb;
    private CharacterMovement _charMovement;

    private bool _isYoyoActive;
    private bool _canEquipYoyo = true;
    public bool GetIsYoyoActive { get => _isYoyoActive; }


    public SaveSystem saveSystem = new SaveSystem();
    public bool loadSaveData;
    public Vector3 checkpointLocation;
    public GameObject yoyoMesh;


    private void Awake()
    {
        if (_canvasPrefab)
        {
            GameObject go = Instantiate(_canvasPrefab);
            CanvasMaster canvas = go.GetComponent<CanvasMaster>(); 
            if(canvas)
            {
                canvas.pauseMenu = GetComponent<PauseMenu>();
                canvasMaster = canvas;
            }
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _charMovement = GetComponent<CharacterMovement>();
        saveSystem.playerMasterRef = this;
        Time.timeScale = 1f;
        if(loadSaveData)
        {
            Load();
        }
    }

    #region SaveAndLoadSystem
    public void Save()
    {
        saveSystem.SaveCheckpoint();
    }
    public void Load()
    {
        saveSystem.LoadCheckpoint();
    }
    [ContextMenu("Delete save")]
    public void Delete()
    {
        saveSystem.DeleteSave();
    }
    #endregion

    private void Update()
    {
        if(_rb)
        {
            if(movementState != EMovementState.SWINGING && movementState != EMovementState.CLIMBING && 
                movementState != EMovementState.CROUCHING && movementState != EMovementState.BALANCING && 
                movementState != EMovementState.RIDING && movementState != EMovementState.RAPPEL)
            {
                if (_rb.velocity.magnitude != 0f && _charMovement.HitGround())
                {
                    movementState = EMovementState.WALKING;
                }
                if (_rb.velocity.magnitude != 0f && _charMovement.HitBrige())
                {
                    movementState = EMovementState.WALKING;
                }
                if (_rb.velocity.y != 0f && !_charMovement.HitGround())
                {
                    movementState = EMovementState.INAIR;
                }
            } 
        }
        ToggleEquipYoyo();

        if(_isDebug)
        {
            print("Movement state: " + movementState);
            print("Yoyo active: " + GetIsYoyoActive);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());  
        }

    }

    public void ToggleEquipYoyo()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !_isYoyoActive)
        {
            EquipYoyo();
        }
        if(Input.GetKeyUp(KeyCode.Mouse1) && _isYoyoActive)
        {
            UnequipYoyo();
        }
    }

    public void EquipYoyo()
    {
        if(_canEquipYoyo)
        {
            _isYoyoActive = true;
            if(yoyoMesh)
            {
                yoyoMesh.SetActive(true);
            }
        }
    }
    public void UnequipYoyo()
    {
        _isYoyoActive = false;
        if(yoyoMesh)
        {
            yoyoMesh.SetActive(false);
        }
    }

    public void ResetMovementState()
    {
        movementState = EMovementState.NONE;
    }

    public void SetSwinging()
    {
        movementState = EMovementState.SWINGING;
    }

    public void SetCrouching()
    {
        movementState = EMovementState.CROUCHING;
    }

    public void SetClimbing()
    {
        movementState = EMovementState.CLIMBING;
    }

    public void SetBalacing()
    {
        movementState = EMovementState.BALANCING;
    }

    public void SetRiding()
    {
        movementState = EMovementState.RIDING;
    }

    //Só está aqui para o j2, pode ser retirado dps do j2
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndLevel"))
        {
            if(canvasMaster)
            {
                canvasMaster.endGamePanel.SetActive(true);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}
