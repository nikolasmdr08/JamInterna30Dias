using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{

    public List<int> recursos;
    public List<GameObject> recursosUI;

    private void Start()
    {
        recursos.Add(0);
        recursos.Add(0);
        recursos.Add(0);
        recursos.Add(0);
        recursos.Add(0);
    }


    public void obtenerRecursos(int tipo) 
    {
        int i = Random.Range(1, 4);
        Debug.Log("Se han obtenido " + i + " unidades de tipo" + tipo.ToString());
        recursos[tipo] = recursos[tipo] + i;
        recursosUI[tipo].GetComponent<TextMeshProUGUI>().text = recursos[tipo].ToString();
    }

    public void ActualizarRecursosEnUI() 
    {
        recursosUI[0].GetComponent<TextMeshProUGUI>().text = recursos[0].ToString();
        recursosUI[1].GetComponent<TextMeshProUGUI>().text = recursos[1].ToString();
        recursosUI[2].GetComponent<TextMeshProUGUI>().text = recursos[2].ToString();
        recursosUI[3].GetComponent<TextMeshProUGUI>().text = recursos[3].ToString();
        recursosUI[4].GetComponent<TextMeshProUGUI>().text = recursos[4].ToString();
    }
}
