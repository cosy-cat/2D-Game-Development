using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _restartGameText;
    private GameManager _gameManager;

    void Start()
    {
        UpdateScore(0);
        _gameOverText.gameObject.SetActive(false);
        _restartGameText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if ( _gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    // void Update()
    // {
    // }

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
        _gameManager.GameOver();
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
}
