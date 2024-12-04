using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance { get; private set; }
    public static DialogSpeaker speakerActual;
    [SerializeField] DialogIU dialUI;
    [SerializeField] GameObject player;
    [SerializeField] public ControladorPreguntas controladorPreguntas;

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

        GameObject uiObject = GameObject.Find("DialogueUI");
        if (uiObject != null)
        {
            dialUI = uiObject.GetComponent<DialogIU>();
        }

        GameObject preguntasObject = GameObject.Find("Preguntas");
        if (preguntasObject != null)
        {
            controladorPreguntas = preguntasObject.GetComponent<ControladorPreguntas>();
        }

    }

    void Start()
    {
        MostrarUI(false);
    }

    public void MostrarUI(bool mostrar)
    {
        dialUI.gameObject.SetActive(mostrar);
        if (!mostrar) 
        { 
            dialUI.localIn = 0;
            player.GetComponent<PlayerController>().onDialog = false; //TESTEAR Y DESCOMENTAR LÍNEA
        }
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
            //dialUI.localIn = 0;
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

    public void nextDialog()
    {
        dialUI.ActualizarTextos(1);
    }

}
