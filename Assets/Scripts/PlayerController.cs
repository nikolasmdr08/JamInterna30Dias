using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    bool colitionWithInteractiveElement = false;
    [SerializeField] float force = 10;
    Rigidbody2D rb2d;
    Rigidbody2D targetRB;
    PlayerInput playerInput;
    Vector2 inputMovePlayer;
    Vector2 inputMousePosition;
    public GameObject target;

    private enum InputDevice { None, KeyboardMouse, Gamepad }
    private InputDevice currentInputDevice = InputDevice.None;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        targetRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputMovePlayer = playerInput.actions["Move"].ReadValue<Vector2>();
        inputMousePosition = playerInput.actions["LookAt"].ReadValue<Vector2>();
        // Detectar si se realiza una acción con el mouse o el teclado
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.anyKey.wasPressedThisFrame)
        {
            currentInputDevice = InputDevice.KeyboardMouse;
            Debug.Log("Dispositivo actual: Teclado y Mouse");
        }
        // Detectar si se realiza una acción con el joystick
        else if (Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame)
        {
            currentInputDevice = InputDevice.Gamepad;
            Debug.Log("Dispositivo actual:Gamepad");
        }

        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        target.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
        RotatePlayerTowards(mousePosition);
    }

    void RotatePlayerTowards(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(inputMovePlayer.x, inputMovePlayer.y) * force;
        rb2d.velocity = movement;
        if (currentInputDevice == InputDevice.Gamepad)
        {
            Vector2 movementTarget = new Vector2(inputMousePosition.x, inputMousePosition.y) * force;
            targetRB.velocity = movementTarget;
        }
    }

    public void InteractWithElements(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (colitionWithInteractiveElement)
            {
                Debug.Log("Objeto interactuable");
            }
            else
            {
                Debug.Log("Objeto no interactuable");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("colisionar con " + col.name);
        if (col.gameObject.tag == "Interactive")
        {
            colitionWithInteractiveElement = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("dejo de colisionar con " + other.name);
        if (other.gameObject.tag == "Interactive")
        {
            colitionWithInteractiveElement = false;
        }
    }
}
