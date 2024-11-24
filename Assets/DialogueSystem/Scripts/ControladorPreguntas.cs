using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControladorPreguntas : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TextMeshProUGUI pregText;
    [SerializeField] private Transform opcionContainer;
    private List<Button> poolButtons = new List<Button>();

    public void ActivarBotones(int cantidad, string title, Opciones[] opciones)
    {
        pregText.text = title;

        if (poolButtons.Count >= cantidad)
        {
            for (int i = 0; i < poolButtons.Count; i++) 
            { 
                if(i < cantidad)
                {
                    poolButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = opciones[i].opcion;
                    poolButtons[i].onClick.RemoveAllListeners();
                    Conversacion co = opciones[i].convResultante;
                    poolButtons[i].onClick.AddListener(() => DarFuncionBoton(co));
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            int cantidadRestante = cantidad - poolButtons.Count;
            for (int i = 0; i < cantidadRestante; i++)
            {
                var newButton = Instantiate(buttonPrefab, opcionContainer).GetComponent<Button>();
                newButton.gameObject.SetActive(true);
                poolButtons.Add(newButton);
            }
            ActivarBotones(cantidad, title, opciones);
        }
    }

    private void DarFuncionBoton(Conversacion co)
    {
        DialogManager.instance.SetConversacion(co, null);
    }
}
