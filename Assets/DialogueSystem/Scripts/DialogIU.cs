using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogIU : MonoBehaviour
{
    public Conversacion conversacion;
    [SerializeField] private float textSpeed = 10;
    [SerializeField] private GameObject convContainer;
    [SerializeField] private GameObject pregContainer;
    [SerializeField] private Image speakIn;
    [SerializeField] private TextMeshProUGUI nombre;
    [SerializeField] private TextMeshProUGUI convText;
    [SerializeField] private Button continuarBTN;
    [SerializeField] private Button anteriorBTN;

    private AudioSource audioSource;
    public int localIn = 1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        convContainer.SetActive(true);
        pregContainer.SetActive(false);
        continuarBTN.gameObject.SetActive(true);
        anteriorBTN.gameObject.SetActive(false);
    }

    /*void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            ActualizarTextos(1);
        }
    }*/

    public void ActualizarTextos(int comportamiento)
    {
        if (comportamiento < -1 || comportamiento > 1)
        {
            Debug.LogWarning("Valor no admitido (solo se acepta -1, 0, 1).");
            return;
        }

        convContainer.SetActive(true);
        pregContainer.SetActive(false);

        if (comportamiento == -1 && localIn > 0)
        {
            print("Diálogo anterior");
            localIn--;
        }
        else if (comportamiento == 1 && localIn < conversacion.dialogos.Length - 1)
        {
            print("Diálogo siguiente");
            localIn++;
        }
        else if (comportamiento == 1)
        {
            print("Diálogo terminado");
            FinalizarDialogo();
            return;
        }
        else if (comportamiento == 0)
        {
            print("Diálogo Actualizado");
        }

        ActualizarDialogo();
        anteriorBTN.gameObject.SetActive(localIn > 0);
        continuarBTN.GetComponentInChildren<TextMeshProUGUI>().text =
            localIn >= conversacion.dialogos.Length - 1 ? "Finalizar" : "Continuar";

        DialogManager.speakerActual.dialLocalIn = localIn;
    }

    private void ActualizarDialogo()
    {
        var dialogoActual = conversacion.dialogos[localIn];
        nombre.text = dialogoActual.personaje.nombre;
        convText.text = dialogoActual.dialogo;
        speakIn.sprite = dialogoActual.personaje.imagen;

        // Actualizar opacidad de la imagen del personaje
        Color color = speakIn.color;
        color.a = dialogoActual.personaje.imagen != null ? 1f : 0f;
        speakIn.color = color;

        // Reproducir sonido del diálogo
        if (dialogoActual.sonido != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(dialogoActual.sonido);
        }
    }

    private void FinalizarDialogo()
    {
        localIn = 0;
        DialogManager.speakerActual.dialLocalIn = 0;
        conversacion.finalizado = true;

        if (conversacion.pregunta != null)
        {
            convContainer.SetActive(false);
            pregContainer.SetActive(true);
            var preg = conversacion.pregunta;
            DialogManager.instance.controladorPreguntas.ActivarBotones(preg.opciones.Length, preg.pregunta, preg.opciones);
            return;
        }

        DialogManager.instance.MostrarUI(false);
    }

    IEnumerator EscribirTexto()
    {
        convText.maxVisibleCharacters = 0;
        convText.text = conversacion.dialogos[localIn].dialogo;
        convText.richText = true;

        for (int i = 0; i < conversacion.dialogos.Length; i++)
        {
            convText.maxVisibleCharacters++;
            yield return new WaitForSeconds(1f/textSpeed);
        }
    }
}
