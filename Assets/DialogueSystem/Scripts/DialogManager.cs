using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance { get; private set; }
    public static DialogSpeaker speakerActual;
    [SerializeField] DialogIU dialUI;
    [SerializeField] GameObject player;
    public ControladorPreguntas controladorPreguntas;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dialUI = FindObjectOfType<DialogIU>();
        controladorPreguntas = FindObjectOfType<ControladorPreguntas>();
    }

    void Start()
    {
        MostrarUI(false);
        //player.GetComponent<DialogSpeaker>().Conversar();
    }

    public void MostrarUI(bool mostrar)
    {
        dialUI.gameObject.SetActive(mostrar);
        if (!mostrar) dialUI.localIn = 0;
    }

    public void SetConversacion(Conversacion conv, DialogSpeaker speaker)
    {
        if (speaker != null)
        {
            speakerActual = speaker;
        }
        else
        {
            dialUI.conversacion = conv;
            dialUI.localIn = 0;
            dialUI.ActualizarTextos(0);
        }

        if (conv.finalizado && !conv.reUsar)
        {
            dialUI.conversacion = conv;
            dialUI.localIn = conv.dialogos.Length;
            dialUI.ActualizarTextos(1);
        }
        else
        {
            dialUI.conversacion = conv;
            dialUI.localIn = speakerActual.dialLocalIn;
            dialUI.ActualizarTextos(0);
        }
    }

    public void CambiarEstadoDeReausable(Conversacion conv, bool estado)
    {
        conv.reUsar = estado;
    }

    public void BloqueoYDesbloqueoDeConversacion(Conversacion conv, bool desbloquear)
    {
        conv.desbloqueada = desbloquear;
    }

}
