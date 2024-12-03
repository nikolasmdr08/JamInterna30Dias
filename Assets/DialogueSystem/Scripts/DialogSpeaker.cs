using System.Collections.Generic;
using UnityEngine;

public class DialogSpeaker : MonoBehaviour
{
    /*[ReorderableList]*/[SerializeField] public List<Conversacion> conversacionesDisponibles = new List<Conversacion>();
    [SerializeField] private int indexConversaciones = 0;
    public int dialLocalIn = 0;
    bool enConversacion = false;

    void Start()
    {
        indexConversaciones = 0;
        dialLocalIn = 0;

        /* solo testing no en codigo final */
        foreach(var conv in conversacionesDisponibles)
        {
            conv.finalizado = false;
            var preg = conv.pregunta;
            if (preg != null)
            {
                foreach(var opcion in preg.opciones)
                {
                    opcion.convResultante.finalizado = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        Debug.Log("colicion: " + collision);
        if (collision.CompareTag("Player")){
            var player = collision.gameObject.GetComponent<PlayerController>();
            Debug.Log("colitionWithTalkeableElement: " + player.colitionWithTalkeableElement);
            Debug.Log("buttonPressed: " + player.buttonPressed);
            if (player.colitionWithTalkeableElement && player.buttonPressed)
            {
                Debug.Log("enConversacion: " + enConversacion);
                if (!enConversacion)
                {
                    enConversacion = true;
                    Conversar();
                }
                else
                {
                    DialogManager.instance.nextDialog();
                    player.buttonPressed = false;
                    //DialogManager.instance.CambiarEstadoDeReausable(conversacionesDisponibles[indexConversaciones], !conversacionesDisponibles[indexConversaciones].reUsar);
                }
            }
        }

    }

    public void Conversar()
    {
        if(indexConversaciones <= conversacionesDisponibles.Count - 1)
        {
            if (conversacionesDisponibles[indexConversaciones].desbloqueada)
            {
                if (conversacionesDisponibles[indexConversaciones].finalizado)
                {
                    if (ActualizarConversacion())
                    {
                        DialogManager.instance.MostrarUI(true);
                        DialogManager.instance.SetConversacion(conversacionesDisponibles[indexConversaciones], this);
                    }
                    DialogManager.instance.SetConversacion(conversacionesDisponibles[indexConversaciones], this);
                    return;
                }
                DialogManager.instance.MostrarUI(true);
                DialogManager.instance.SetConversacion(conversacionesDisponibles[indexConversaciones], this);
            }
            else
            {
                Debug.LogWarning("La conversacion esta bloqueada.");
                DialogManager.instance.MostrarUI(false);
                enConversacion = false;
                
            }
        }
        else
        {
            Debug.Log("Fin de dialogo.");
            DialogManager.instance.MostrarUI(false);
            enConversacion = false;
        }
    }

    private bool ActualizarConversacion()
    {
        if (!conversacionesDisponibles[indexConversaciones].reUsar)
        {
            if(indexConversaciones < conversacionesDisponibles.Count - 1)
            {
                indexConversaciones++;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}
