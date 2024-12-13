using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float force = 10;
    [SerializeField] RectTransform uiElement;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject targetGO;
    Rigidbody rb;
    PlayerInput playerInput;
    Vector2 inputMovePlayer;
    Vector2 inputTargetPosition;

    public GameObject instrution;
    public TextMeshProUGUI instrutionText;
    private bool colitionWithInteractiveElement;
    private GameObject currentNpc;
    private int currentDialogIndex;
    public bool onDialog;

    [SerializeField] private GameObject boxToOpen;
    public GameObject boxInstrution;
    public TextMeshProUGUI BoxInstrutionText;

    public GameObject dialog;
    public TextMeshProUGUI dialogText;
    private bool obtenerResurso = false;
    public bool menuAbierto = false;
    public GameObject inventario;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        if (uiElement == null) Debug.LogError("Referencia a UI Element no asignada.");
        if (canvas == null) Debug.LogError("Referencia al Canvas no asignada.");
        if (targetGO == null) Debug.LogError("Referencia al círculo amarillo no asignada.");
    }

    void Update()
    {
        inputMovePlayer = playerInput.actions["Move"].ReadValue<Vector2>();
        inputTargetPosition = playerInput.actions["LookAt"].ReadValue<Vector2>();

        if (onDialog)
        {
            dialog.SetActive(true);
        }
        else
        {
            dialog.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (onDialog) return;
        Vector3 movement = new Vector3(inputMovePlayer.x, 0, inputMovePlayer.y) * force;
        rb.velocity = movement;
        MoveUIElement(inputTargetPosition);
        MoveYellowCircleToUIElement();
    }

    private void MoveUIElement(Vector2 stickInput)
    {
        if (uiElement == null || canvas == null) return;

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 movement = stickInput * Time.fixedDeltaTime * 500;
        Vector2 anchoredPosition = uiElement.anchoredPosition;
        Vector2 newPos = anchoredPosition + movement;
        float halfWidth = canvasRect.rect.width / 2;
        float halfHeight = canvasRect.rect.height / 2;
        newPos.x = Mathf.Clamp(newPos.x, -halfWidth, halfWidth);
        newPos.y = Mathf.Clamp(newPos.y, -halfHeight, halfHeight);
        uiElement.anchoredPosition = newPos;
    }

    private void MoveYellowCircleToUIElement()
    {
        if (targetGO == null || uiElement == null || canvas == null) return;

        Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, uiElement.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 worldPosition = ray.GetPoint(enter);
            targetGO.transform.position = worldPosition;
            Vector3 lookDirection = targetGO.transform.position - transform.position;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.CompareTag("npc") || collision.gameObject.CompareTag("police")) && !onDialog)
        {
            instrution.SetActive(true);
            instrutionText.text = "Hablar";
            colitionWithInteractiveElement = true;
            currentNpc = collision.gameObject;
            currentDialogIndex = 0; // Resetear índice de diálogo

        }

        if (collision.gameObject.CompareTag("Box"))
        {
            //Debug.Log("Debe mostrar Abrir");
            instrution.SetActive(true);
            instrutionText.text = "Abrir";
            colitionWithInteractiveElement = true;
            boxToOpen = collision.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("police")) && onDialog)
        {
           instrution.SetActive(false);
        }

        if(obtenerResurso && other.gameObject.CompareTag("Box"))
        {
            other.gameObject.GetComponent<Box>().obtener();
            obtenerResurso = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("npc"))
        {
            onDialog = false;
            instrution.SetActive(false);
            instrutionText.text = "";
            colitionWithInteractiveElement = false;
            currentNpc = null;
            
        }
        if (collision.gameObject.CompareTag("police"))
        {
            onDialog = false;
            instrution.SetActive(false);
            instrutionText.text = "";
            colitionWithInteractiveElement = false;
            currentNpc.GetComponent<PoliceController>().endDialog = false;
            currentNpc = null;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            //Debug.Log("Debe cerrar la opcion Abrir");
            instrution.SetActive(false);
            instrutionText.text = "";
            colitionWithInteractiveElement = false;
            boxToOpen = null;
        }
    }

    public void InteractWithElements(InputAction.CallbackContext callback)
    {
        if (!callback.performed || !colitionWithInteractiveElement) return;

        if(currentNpc != null)
        {
            onDialog = true;
            string[] dialog = null;

            // Detectar el tipo de controlador y obtener el diálogo correspondiente
            var npcController = currentNpc.GetComponent<NpcController>();
            if (npcController != null)
            {
                dialog = GameManager.instance.poseeNota.Contains(currentNpc)
                    ? npcController.dialogWithNote
                    : npcController.dialogOutNote;
            }
            else
            {
                var policeController = currentNpc.GetComponent<PoliceController>();
                if (policeController != null)
                {
                    dialog = policeController.dialogue;
                }
            }

            // Si no se encontró diálogo, finalizar la interacción
            if (dialog == null) return;

            // Mostrar el diálogo actual
            if (currentDialogIndex < dialog.Length)
            {
                dialogText.text = dialog[currentDialogIndex];
                currentDialogIndex++;
            }

            // Finalizar el diálogo si se alcanza el final
            if (currentDialogIndex >= dialog.Length)
            {
                if (npcController != null)
                {
                    npcController.endDialog = true;
                    currentDialogIndex = npcController.repeat ? 0 : dialog.Length - 1;
                }
                else if (currentNpc.GetComponent<PoliceController>() != null)
                {
                    currentNpc.GetComponent<PoliceController>().endDialog = true;
                }

                onDialog = false;
            }
        }
        else
        {
            obtenerResurso = true;
        }
    }

    public void OpenCloseMenu(InputAction.CallbackContext callback)
    {
        if (callback.performed)
        {
            if (!menuAbierto)
            {
                menuAbierto = true;
                inventario.SetActive(menuAbierto);
            }
            else
            {
                menuAbierto = false;
                inventario.SetActive(menuAbierto);
            }
        }
    }

}
