using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force = 10;
    Rigidbody2D rb2d;
    Rigidbody2D targetRB;
    PlayerInput playerInput;
    Vector2 inputMovePlayer;
    Vector2 inputMousePosition;
    public GameObject target;
    public GameObject panelInfo;
    public TextMeshProUGUI textPanelInfo;
    public bool colitionWithInteractiveElement = false;
    public bool colitionWithTalkeableElement = false;
    public bool buttonPressed = false;
    public bool showInfo = false;
    private string textInfo;
    public bool onDialog = false;

    private enum InputDevice { None, KeyboardMouse, Gamepad }
    private InputDevice currentInputDevice = InputDevice.None;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        targetRB = target.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!onDialog)
        {
            inputMovePlayer = playerInput.actions["Move"].ReadValue<Vector2>();
            inputMousePosition = playerInput.actions["LookAt"].ReadValue<Vector2>();
            if (Gamepad.current != null && Gamepad.current.leftStick.magnitude != 0 && Gamepad.current.rightStick.magnitude != 0)
            {
                currentInputDevice = InputDevice.Gamepad;
            }
            else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame || Keyboard.current.anyKey.wasPressedThisFrame /* || Mouse.current.wasUpdatedThisFrame*/)
            {
                currentInputDevice = InputDevice.KeyboardMouse;
            }

            if (currentInputDevice == InputDevice.KeyboardMouse)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                target.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
                RotatePlayerTowards(mousePosition);
            }
            else if(currentInputDevice == InputDevice.Gamepad) 
            {
                Vector2 movementTarget = new Vector2(inputMousePosition.x, inputMousePosition.y) * force;
                targetRB.velocity = movementTarget;
                RotatePlayerTowards(target.gameObject.transform.position);
            }
        }

        if (showInfo)
        {
            panelInfo.SetActive(true);
            textPanelInfo.text = textInfo;
        }
        else
        {
            panelInfo.SetActive(false);
        }
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
    }

    public void InteractWithElements(InputAction.CallbackContext callback)
    {
        //Debug.Log(callback.phase);
        if (callback.performed)
        {
            if (colitionWithInteractiveElement)
            {
                Debug.Log("Objeto interactuable");
                buttonPressed = true;
                showInfo = false;
            }
            if (colitionWithTalkeableElement)
            {
                Debug.Log("NPC interactuable");
                buttonPressed = true;
                showInfo = false;
            }
        }
        if (callback.canceled)
        {
            buttonPressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colisionar con " + collision.name);
        if (collision.gameObject.tag == "Interactive")
        {
            if(collision.gameObject.layer == 6)
            {
                Debug.Log("NPC true ");
                colitionWithTalkeableElement = true;
                showInfo = true;
                textInfo = "Hablar";
            }
            if (collision.gameObject.layer == 7)
            {
                Debug.Log("OBJETO true ");
                colitionWithInteractiveElement = true;
                showInfo = true;
                textInfo = "Agarrar";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("dejo de colisionar con " + other.name);
        if (other.gameObject.layer == 6)
        {
            Debug.Log("NPC false ");
            colitionWithTalkeableElement = false;
            showInfo = false;
        }
        if (other.gameObject.layer == 7)
        {
            Debug.Log("OBJETO false ");
            colitionWithInteractiveElement = false;
            showInfo = false;
        }
    }
}
