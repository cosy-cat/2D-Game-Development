﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    private PlayerControl _inputAction;
    private Vector3 _direction;

    [SerializeField] 
    private float _speed = 5.0f;

    private bool _fire = false;

    private void Awake()
    {
        _direction = new Vector3();
        _inputAction = new PlayerControl();

        // A Vector2 can be implicitly converted into a Vector3. (The z is set to zero in the result).
        // https://docs.unity3d.com/ScriptReference/Vector2-operator_Vector2.html
        _inputAction.Player.Move.performed += context => _direction = context.ReadValue<Vector2>();
        _inputAction.Player.Fire.performed += context => _fire = context.ReadValueAsButton();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_fire)
        {
            Debug.Log("Firing !!!!");
        }
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

    private void OnEnable()
    {
        _inputAction.Enable();
    }
    private void OnDisable()
    {
        _inputAction.Disable();
    }
}
