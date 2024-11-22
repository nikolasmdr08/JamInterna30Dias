using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

public class DialogSpeaker : MonoBehaviour
{
    /*[ReorderableList]*/[SerializeField] public List<Conversacion> conversacionesDisponibles = new List<Conversacion>();
    [SerializeField] private int indexConversaciones = 0;
    public int dialLocalIn = 0;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)){
            Conversar();
        }
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z)){
            DialogManager.instance.CambiarEstadoDeReausable(conversacionesDisponibles[indexConversaciones], !conversacionesDisponibles[indexConversaciones].reUsar);
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
            }
        }
        else
        {
            Debug.Log("Fin de dialogo.");
            DialogManager.instance.MostrarUI(false);
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
