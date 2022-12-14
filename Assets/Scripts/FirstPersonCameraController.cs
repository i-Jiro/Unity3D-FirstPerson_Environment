using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Cinemachine;

public class FirstPersonCameraController : MonoBehaviour
{
    public float Sensitivity = 1.0f;
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    private PlayerInputHandler _input;
    [SerializeField] private Transform _cameraPosition;
    private float _xRotation;
    private float _yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (PlayerInputHandler.Instance != null)
        {
            _input = PlayerInputHandler.Instance;
        }
        else
        {
            Debug.LogError("Player Input Handler not found on scene!");
        }
    }
    
    void LateUpdate()
    {
        GetInput();
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        _playerCamera.transform.position = _cameraPosition.position;
        //Rotate Camera
        _playerCamera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        //Rotate Player
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    private void GetInput()
    {
        _xRotation -= _input.LookInput.y * Sensitivity;
        _yRotation += _input.LookInput.x * Sensitivity;
    }
    
    //Called on by Unity Event in pause menu UI.
    public void ChangeSensitivity(float value)
    {
        Sensitivity = value;
    }
}
