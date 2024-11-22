using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            ActualizarTextos(1);
        }
    }

    public void ActualizarTextos(int comportamiento){
        convContainer.SetActive(true);
        pregContainer.SetActive(false);
        switch (comportamiento)
        {
            case -1:
                if (localIn > 0)
                {
                    print("Dialogo anterior");
                    localIn--;

                    nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                    //StopAllCoroutines();
                    //StartCoroutine(EscribirTexto());
                    convText.text = conversacion.dialogos[localIn].dialogo;
                    speakIn.sprite = conversacion.dialogos[localIn].personaje.imagen;

                    if (conversacion.dialogos[localIn].sonido != null)
                    {
                        audioSource.Stop();
                        var audio = conversacion.dialogos[localIn].sonido;
                        audioSource.PlayOneShot(audio);
                    }
                    anteriorBTN.gameObject.SetActive(localIn > 0);
                }
                DialogManager.speakerActual.dialLocalIn = localIn;
                break;
            case 0:
                print("Dialogo Actualizado");
                nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                //StopAllCoroutines();
                //StartCoroutine(EscribirTexto());
                convText.text = conversacion.dialogos[localIn].dialogo;
                speakIn.sprite = conversacion.dialogos[localIn].personaje.imagen;

                if (conversacion.dialogos[localIn].sonido != null)
                {
                    audioSource.Stop();
                    var audio = conversacion.dialogos[localIn].sonido;
                    audioSource.PlayOneShot(audio);
                }
                anteriorBTN.gameObject.SetActive(localIn > 0);
                if(localIn >= conversacion.dialogos.Length - 1)
                {
                    continuarBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";
                }
                else
                {
                    continuarBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
                }
                break;
            case 1:
                if(localIn < conversacion.dialogos.Length - 1)
                {
                    print("Dialogo siguiente");
                    localIn++;
                    nombre.text = conversacion.dialogos[localIn].personaje.nombre;
                    //StopAllCoroutines();
                    //StartCoroutine(EscribirTexto());
                    convText.text = conversacion.dialogos[localIn].dialogo;
                    speakIn.sprite = conversacion.dialogos[localIn].personaje.imagen;

                    if (conversacion.dialogos[localIn].sonido != null)
                    {
                        audioSource.Stop();
                        var audio = conversacion.dialogos[localIn].sonido;
                        audioSource.PlayOneShot(audio);
                    }
                    anteriorBTN.gameObject.SetActive(localIn > 0);
                    if (localIn >= conversacion.dialogos.Length - 1)
                    {
                        continuarBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Finalizar";
                    }
                    else
                    {
                        continuarBTN.GetComponentInChildren<TextMeshProUGUI>().text = "Continuar";
                    }
                }
                else
                {
                    print("Dialogo terminado");
                    localIn = 0;
                    DialogManager.speakerActual.dialLocalIn = 0;
                    conversacion.finalizado = true;
                    if (conversacion.pregunta != null)
                    {
                        convContainer.SetActive(false);
                        pregContainer.SetActive(true);
                        var preg = conversacion.pregunta;
                        DialogManager.instance.controladorPreguntas.ActivarBotones(preg.opciones.Length,preg.pregunta,preg.opciones);
                        return;
                    }
                    DialogManager.instance.MostrarUI(false);
                    return;
                }
                DialogManager.speakerActual.dialLocalIn = localIn;
                break;
            default:
                Debug.LogWarning("Estas pasando un valor no admitido (solo se acepta estos valores (-1, 0, 1).)");
                break;
        }
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
