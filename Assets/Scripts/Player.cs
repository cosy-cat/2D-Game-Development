using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

// public delegate void OnPlayerDeathDelegate(object sender, EventArgs args);

public class Player : MonoBehaviour
{
    private PlayerControl _inputAction;
    private Vector3 _direction;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private GameObject[] _lasers = null;
    [SerializeField] private Vector3 _spawnLaserOffset = new Vector3(0f, 0.8f, 0f);
    private bool _playerFire = false;
    private float _fireRate = 0f;
    [SerializeField] float _fireCoolDownDelay = 0.2f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    // public event OnPlayerDeathDelegate OnDeathEvent;
    [SerializeField] private bool _isTrippleShotActive = false;

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
            if (_isTrippleShotActive)
                laserIndex = 1;
            
            Instantiate(_lasers[laserIndex], transform.position + _spawnLaserOffset, Quaternion.identity);
            _fireRate = Time.time + _fireCoolDownDelay;
        }
    }

    public void Damage()
    {
        _lives--;
        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TrippleShotActive()
    {
        _isTrippleShotActive = true;
        StartCoroutine(TrippleShotActiveCoroutine());
    }

    private IEnumerator TrippleShotActiveCoroutine()
    {
        yield return new WaitForSeconds(5f);
        _isTrippleShotActive = false;
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
