using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ItemInteract : MonoBehaviour
{
    public Recursos _recursos;
    public GameObject boxToOpen;

    public GameObject instrution;
    public TextMeshProUGUI instrutionText;
    private bool colitionWithInteractiveElement;
    private GameObject currentNpc;
    private int currentDialogIndex;
    public bool onDialog;

    public GameObject dialog;
    public TextMeshProUGUI dialogText;


    // Start is called before the first frame update
    void Start()
    {
        _recursos = FindAnyObjectByType<Recursos>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box") == true)
        {
            Debug.Log("Debe mostrar Abrir");
            instrution.SetActive(true);
            instrutionText.text = "Abrir";
            colitionWithInteractiveElement = true;
            //currentNpc = collision.gameObject;
            currentDialogIndex = 0; // Resetear índice de diálogo
            boxToOpen = collision.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box") == true)
        {
            //Debug.Log("Stay");
            //instrution.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box") == true)
        {
            Debug.Log("Debe cerrarse Abrir");
            onDialog = false;
            instrution.SetActive(false);
            instrutionText.text = "";
            colitionWithInteractiveElement = false;
            //currentNpc = null;
            boxToOpen = null;
        }
    }

    public void InteractWithElements(InputAction.CallbackContext callback)
    {
        //if (!callback.performed || !colitionWithInteractiveElement || boxToOpen == null) return;

        for (int i = 0; i >= boxToOpen.GetComponent<Box>().itemsDeTipo.Length - 1; i++)
        {
            _recursos.obtenerRecursos(boxToOpen.GetComponent<Box>().itemsDeTipo[i]);
        }

    }
}
