using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force = 10;
    Rigidbody rb2d;
    Rigidbody targetRB;
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


    //AGREGADO DE RILOUD, agrego una variable para tener acceso los recursos del jugador
    public Recursos _recursos;



    private enum InputDevice { None, KeyboardMouse, Gamepad }

    private InputDevice currentInputDevice = InputDevice.None;

    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        targetRB = target.gameObject.GetComponent<Rigidbody>();
        RotatePlayerTowards(target.gameObject.transform.position);

        //AGREGADO DE RILOUD, se asigna la variable de los recursos
        _recursos = FindObjectOfType<Recursos>();

    }

    void Update()
    {
        if (!onDialog)
        {
            inputMovePlayer = playerInput.actions["Move"].ReadValue<Vector2>();
            inputMousePosition = playerInput.actions["LookAt"].ReadValue<Vector2>();
            if (Gamepad.current != null && (Gamepad.current.leftStick.magnitude != 0 || Gamepad.current.rightStick.magnitude != 0) && Gamepad.current.wasUpdatedThisFrame)
            {
                currentInputDevice = InputDevice.Gamepad;
            }
            if(currentInputDevice == InputDevice.Gamepad) 
            {
                Vector3 movementTarget = new Vector3(inputMousePosition.x, 0, inputMousePosition.y) * force;
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
    {;
        transform.LookAt(targetPosition);
    }

    private void FixedUpdate()
    {
        //move player
        Vector3 movement = new Vector3(inputMovePlayer.x, 0,inputMovePlayer.y) * force;
        rb2d.velocity = movement;
        //move target
        Vector3 movementTarget = new Vector3(inputMousePosition.x, 0, inputMousePosition.y) * force;
        Vector3 newPosition = targetRB.position + movementTarget * Time.fixedDeltaTime;
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        newPosition.x = Mathf.Clamp(newPosition.x, screenBottomLeft.x, screenTopRight.x);
        newPosition.z = Mathf.Clamp(newPosition.z, screenBottomLeft.z, screenTopRight.z);
        targetRB.velocity = (newPosition - targetRB.position) / Time.fixedDeltaTime;
        //Debug.Log($"New Position: {newPosition}");
    }

    public void InteractWithElements(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (colitionWithInteractiveElement)
            {//
                //Debug.Log("Objeto interactuable");
                buttonPressed = true;
                showInfo = false;

                //AGREGADO DE RILOUD
                _recursos.obtenerRecursos(Random.Range(0, 4)); // Obtiene una cantidad entre 1 y 3, de uno solo de los 5 tipos de recursos
            }
            if (colitionWithTalkeableElement)
            {
                //Debug.Log("NPC interactuable");
                buttonPressed = true;
                showInfo = false;
                onDialog = true; //TESTEAR Y DESCOMENTAR LÍNEA
            }
        }
        if (callback.canceled)
        {
            buttonPressed = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("colisionar con " + collision.name);
        if (collision.gameObject.tag == "Interactive")
        {
            if(collision.gameObject.layer == 6)
            {
                //Debug.Log("NPC true ");
                colitionWithTalkeableElement = true;
                showInfo = true;
                textInfo = "Hablar";
            }
            if (collision.gameObject.layer == 7)
            {
                //Debug.Log("OBJETO true ");
                colitionWithInteractiveElement = true;
                showInfo = true;
                textInfo = "Agarrar";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("dejo de colisionar con " + other.name);
        if (other.gameObject.layer == 6)
        {
            //Debug.Log("NPC false ");
            colitionWithTalkeableElement = false;
            showInfo = false;
        }
        if (other.gameObject.layer == 7)
        {
            //Debug.Log("OBJETO false ");
            colitionWithInteractiveElement = false;
            showInfo = false;
        }
    }
}
