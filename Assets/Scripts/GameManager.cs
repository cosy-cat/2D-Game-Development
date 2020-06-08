using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver = false;
    private PlayerControl _inputAction;

    private void Awake()
    {
        _inputAction = new PlayerControl();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isGameOver)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single); // current game scene
        }
    }

    public void GameOver()
    {
        _inputAction.Player.RestartGame.performed += context => _isGameOver = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        if (_inputAction != null)
        {
            _inputAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (_inputAction != null)
        {
            _inputAction.Disable();
        }
    }
}
