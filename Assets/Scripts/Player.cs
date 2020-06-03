using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

// public delegate void OnPlayerDeathDelegate(object sender, EventArgs args);
// public delegate void OnPlayerScoreDelegate(object sender, EventArgs args);

public class Player : MonoBehaviour
{
    private PlayerControl _inputAction;
    private Vector3 _direction;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject[] _lasers = null;
    private enum ActiveLaser
    {
        Default,        // shall corresponds to _lasers[0]
        TrippleShot     // shall corresponds to _lasers[1]
    }
    private ActiveLaser activeLaser = ActiveLaser.Default;
    [SerializeField] private Vector3 _spawnLaserOffset = new Vector3(0f, 0.8f, 0f);
    private bool _playerFire = false;
    private float _fireRate = 0f;
    [SerializeField] float _fireCoolDownDelay = 0.2f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private float _powerupDelay = 5f;
    private PowerUpTimeout _powerUpTimeout = new PowerUpTimeout();
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private int _score = 0;
    private UIManager _uiManager;
    // public event OnPlayerScoreDelegate OnPlayerScore;
    // public event OnPlayerDeathDelegate OnDeathEvent;

    private void Awake()
    {
        _direction = new Vector3();
        _inputAction = new PlayerControl();

        // A Vector2 can be implicitly converted into a Vector3. (The z is set to zero in the result).
        // https://docs.unity3d.com/ScriptReference/Vector2-operator_Vector2.html
        _inputAction.Player.Move.performed += context => _direction = context.ReadValue<Vector2>();
        _inputAction.Player.Fire.performed += context => _playerFire = context.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            throw new SystemException("SpawnManager component of Spawn_Manager Gameobject not found");
        }

        if (_lasers.Length == 0)
        {
            throw new System.Exception("Please assign laser prefabs into the corresponding field in Unity Editor");
        }

        // _shieldVisualizer = this.gameObject.transform.GetChild(0).gameObject;
        if (_shieldVisualizer == null)
        {
            throw new System.Exception("Please assign shield visualiser (child of the player) in the corresponding field in Unity Editor");
        }
        _shieldVisualizer.SetActive(false);
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Canvas object is not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_playerFire)
            Fire();

    }

    private void CalculateMovement()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        float xPosition = transform.position.x;
        if (xPosition > 11f)
        {
            xPosition = -11f;
        }
        else if (xPosition < -11f)
        {
            xPosition = 11f;
        }

        transform.position = new Vector3(
            xPosition,
            Mathf.Clamp(transform.position.y, -3.8f, 0f), 
            0f
        );
    }

    private void Fire()
    {
        if (Time.time > _fireRate)
        {
            int laserIndex = 0;
            switch (activeLaser)
            {
                case ActiveLaser.TrippleShot:
                    laserIndex = 1;
                    break;
            }
            
            Instantiate(_lasers[laserIndex], transform.position + _spawnLaserOffset, Quaternion.identity);
            _fireRate = Time.time + _fireCoolDownDelay;
        }
    }

    public void Damage()
    {
        if (_shieldVisualizer.activeSelf)
        {
            _shieldVisualizer.SetActive(false);
            return;
        }
            
        _lives--;
        _uiManager.UpdateLives(_lives);
        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        if (Time.time > _powerUpTimeout.TrippleShot)
        {
            _powerUpTimeout.TrippleShot = Time.time + _powerupDelay;
            StartCoroutine(TrippleShotActiveCoroutine());
        }
        else
            _powerUpTimeout.TrippleShot = Time.time + _powerupDelay;
    }

    private IEnumerator TrippleShotActiveCoroutine()
    {
        activeLaser = ActiveLaser.TrippleShot;
        for (float timer = Time.time; timer < _powerUpTimeout.TrippleShot; timer += Time.deltaTime)
            yield return null;
        activeLaser = ActiveLaser.Default;
    }

    public void BoostSpeedActive()
    {
        if (Time.time > _powerUpTimeout.BoostSpeed)
        {
            _powerUpTimeout.BoostSpeed = Time.time + _powerupDelay;
            StartCoroutine(BoostSpeedActiveCoroutine());
        }
        else
            _powerUpTimeout.BoostSpeed = Time.time + _powerupDelay;
    }

    private IEnumerator BoostSpeedActiveCoroutine()
    {
        _speed = 8.5f;
        for (float timer = Time.time; timer < _powerUpTimeout.BoostSpeed; timer += Time.deltaTime)
            yield return null;
        _speed = 5.0f;
    }

    public void ShieldActive()
    {
        if (Time.time > _powerUpTimeout.Shield)
        {
            _powerUpTimeout.Shield = Time.time + _powerupDelay;
            StartCoroutine(ShieldActiveCoroutine());
        }
        else
            _powerUpTimeout.Shield = Time.time + _powerupDelay;
    }

    private IEnumerator ShieldActiveCoroutine()
    {
        _shieldVisualizer.SetActive(true);
        for (float timer = Time.time; timer < _powerUpTimeout.Shield; timer += Time.deltaTime)
            yield return null;
        _shieldVisualizer.SetActive(false);
    }

    public void AddScore(int score)
    {
        _score += score;
        _uiManager.UpdateScore(_score);
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
