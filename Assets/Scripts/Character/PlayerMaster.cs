using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ECollectibe
{
    COUCH,
    TABLE,
    PENCIL
}

public class PlayerMaster : MonoBehaviour
{
    public EMovementState movementState;
    public CharacterData characterData;
    public bool godMode;

    [Header("Prefabs")]
    [SerializeField] private GameObject _canvasPrefab;
    [SerializeField] private GameObject _gameProgressPrefab;


    [Header("References")]
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
    public GameProgress gameProgress;

    public Dictionary<ECollectibe, bool> collectiblesFound = new Dictionary<ECollectibe, bool>();

    public PlayerMaster()
    {
        //collectiblesFound.Add(ECollectibe.COUCH, true);
        //collectiblesFound.Add(ECollectibe.TABLE, true);
        //collectiblesFound.Add(ECollectibe.PENCIL, false);
    }

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

        if(_gameProgressPrefab)
        {
            GameObject go = Instantiate(_gameProgressPrefab);
            if(go.GetComponent<GameProgress>() != null)
            {
                gameProgress = go.GetComponent<GameProgress>();
                loadSaveData = GameProgress.shouldLoadSave;
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

    [ContextMenu("Dummy Debug")]
    public void DummyDebug()
    {
        foreach(var item in collectiblesFound)
        {
            print(item);
        }
    }

    #region SaveAndLoadSystem
    public void Save()
    {
        saveSystem.SaveCheckpoint();
        if(canvasMaster)
        {
            canvasMaster.SaveFeedbackIcon();
        }
    }
    public void Load()
    {
        saveSystem.LoadCheckpoint();
    }

    [ContextMenu("save concept art")]
    public void SaveConceptArt()
    {
        saveSystem.SaveConceptArt();
    }


    [ContextMenu("Delete save")]
    public void DeleteSave()
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
                movementState != EMovementState.RIDING && movementState != EMovementState.RAPPEL && movementState != EMovementState.SPRINTING)
            {
                if (_rb.velocity.magnitude != 0f && _charMovement.HitGround() || _rb.velocity.magnitude != 0f && _charMovement.HitBrige())
                {
                    // _ = _charMovement.currentSpeed == _charMovement.sprintSpeed ? movementState = EMovementState.SPRINTING : movementState = EMovementState.WALKING;
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

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            DeleteSave();
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
