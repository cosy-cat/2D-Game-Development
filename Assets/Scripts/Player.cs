﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerControl _inputAction;
    private Vector3 _direction;

    [SerializeField] 
    private float _speed = 5.0f;

    private void Awake()
    {
        _direction = new Vector3();
        _inputAction = new PlayerControl();

        // A Vector2 can be implicitly converted into a Vector3. (The z is set to zero in the result).
        // https://docs.unity3d.com/ScriptReference/Vector2-operator_Vector2.html
        _inputAction.Player.Move.performed += context => _direction = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);

        // according to new position, check and make some adjustments
        // > clamp y position
        float yPosition = Mathf.Clamp(transform.position.y, -3.8f, 0f);

        // > wrap x position
        float xPosition = transform.position.x;
        switch (xPosition)
        {
            case var x when x > 11f:
                xPosition = -11f;
                break;
            case var x when x < -11f:
                xPosition = 11f;
                break;
        }

        // Finally, update final position to display
        transform.position = new Vector3(xPosition, yPosition, 0f);
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
