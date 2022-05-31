using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused;

    [SerializeField] private PlayerMaster _playerMaster;


    private void Start()
    {
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void OnDestroy()
    {
        isGamePaused = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    [ContextMenu("Pause")]
    public void Pause()
    {
        if (_playerMaster.canvasMaster.pausePanel.activeSelf)
        {
            return;
        }

        isGamePaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if(_playerMaster && _playerMaster.canvasMaster)
        {
            _playerMaster.canvasMaster.ResetPanels();
            _playerMaster.canvasMaster.pausePanel.SetActive(true);
        }
    }

    [ContextMenu("Unpause")]
    public void Unpause()
    {
        if (!_playerMaster.canvasMaster.pausePanel.activeSelf)
            return;
        isGamePaused = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (_playerMaster && _playerMaster.canvasMaster)
        {
            _playerMaster.canvasMaster.pausePanel.SetActive(false);
        }

    }
}
