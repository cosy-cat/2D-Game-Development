using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // public InputAction moveAction;
    private PlayerControl _inputAction;
    
    // public InputAction moveAction = new InputAction("Move");
    private Vector2 _moveDirection = new Vector2();
    private Vector3 _direction = new Vector3();

    [SerializeField] 
    private float _speed = 5.0f;

    private void Awake()
    {
        _inputAction = new PlayerControl();
        _inputAction.Player.Move.performed += context => _moveDirection = context.ReadValue<Vector2>();
    }
    

    // Start is called before the first frame update
    void Start()
    {
        // moveAction.Enable();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // _moveDirection = moveAction.ReadValue<Vector2>();
        Debug.Log(_moveDirection);
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _inputAction.Enable();
    }
    private void OnDisable()
    {
        _inputAction.Disable();
    }
}
