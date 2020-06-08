using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartGameText;
    private PlayerControl _inputAction;
    private bool _restartGame = false;

    private void Awake() {
        _inputAction = new PlayerControl();
        // _inputAction.Player.RestartGame.performed += context => _restartGame = context.ReadValueAsButton();
    }

    void Start()
    {
        UpdateScore(0);
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_restartGame)
        {
            SceneManager.LoadScene("Scenes/SampleScene", LoadSceneMode.Single);
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _restartGameText.gameObject.SetActive(true);
        StartCoroutine(DisplayGameOverRoutine());
        _gameOverText.gameObject.SetActive(true);
        _inputAction.Player.RestartGame.performed += context => _restartGame = context.ReadValueAsButton();
    }

    private IEnumerator DisplayGameOverRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.5f);
        }
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
