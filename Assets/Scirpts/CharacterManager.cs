using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private float _gravity = -9.81f;
    [SerializeField] private float _playerSpeed = 0.0f;
    [SerializeField] private float _moveXSpeed = 0.0f;
    public static int score = 0;
    private Vector3 _characterVector;

    private void Awake()
    {
        score = 0;
        _characterVector = new Vector3(0, _gravity, _playerSpeed * Time.deltaTime);
        transform.position = Vector3.up;
        _characterController = GetComponent<CharacterController>();
        //_playerAnimator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        _playerInput = new PlayerInput();
    }

    void Start()
    {
        _playerInput.PlayerController.InputSystem.started += FingerStartSystem;
        _playerInput.PlayerController.InputSystem.performed += FingerMoveSystem;
        _playerInput.PlayerController.InputSystem.canceled += FingerStopSystem;
    }

   
    void Update()
    {
        CharacterMove();
        if (GameManager.instance.isWin)
        {
            _playerSpeed = 0.0f;
            _moveXSpeed = 0.0f;
        }
    }

    private void FingerStartSystem(InputAction.CallbackContext context)
    {
        _characterVector = new Vector3(context.ReadValue<Vector2>().x, _gravity, _playerSpeed * Time.deltaTime);
    }

    private void FingerMoveSystem(InputAction.CallbackContext context)
    {
        _characterVector = new Vector3(context.ReadValue<Vector2>().x, _gravity, _playerSpeed * Time.deltaTime);
    }

    private void FingerStopSystem(InputAction.CallbackContext context)
    {
        _characterVector = new Vector3(0, _gravity, _playerSpeed * Time.deltaTime);
    }
    void OnEnable()
    {
        _playerInput.Enable();
    }
    void OnDisable()
    {
        _playerInput.Disable();
    }

    private void CharacterMove()
    {
        _characterController.Move(_characterVector * _moveXSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GameManager.instance.GameLose();
            _playerSpeed = 0.0f;
            _moveXSpeed = 0.0f;
        }
    }
}
