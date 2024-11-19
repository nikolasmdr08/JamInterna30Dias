using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Resources playerResources;

    public UsableTools playerTools;


    // Estructura para representar un item
    [System.Serializable]
    public struct Tool
    {
        public string nombre;
        public int[] recursosRequeridos; // Indices corresponden a recursos en el diccionario
        public int cantidadRequerida; // Cantidad requerida de cada recurso
        public GameObject prefab; // Prefab del item
    }

    // Método para agregar recursos al inventario
    public void AgregarRecurso(string nombre, int cantidad)
    {
        //if (recursos.ContainsKey(nombre))
        //{
        //    recursos[nombre] += cantidad;
        //}
        //else
        //{
        //    recursos.Add(nombre, cantidad);
        //}
    }

    // Método para fabricar un item
    public bool FabricarItem(string nombreItem)
    {
        //if (items.ContainsKey(nombreItem))
        //{
        //    Tool item = items[nombreItem];
        //    bool tieneRecursos = true;

        //    // Verificar si se tienen los recursos necesarios
        //    for (int i = 0; i < item.recursosRequeridos.Length; i++)
        //    {
        //        string recurso = item.recursosRequeridos[i].ToString();
        //        if (!recursos.ContainsKey(recurso) || recursos[recurso] < item.cantidadRequerida)
        //        {
        //            tieneRecursos = false;
        //            break;
        //        }
        //    }

        //    // Si se tienen los recursos, restarlos y crear el item
        //    if (tieneRecursos)
        //    {
        //        for (int i = 0; i < item.recursosRequeridos.Length; i++)
        //        {
        //            string recurso = item.recursosRequeridos[i].ToString();
        //           // recursos[recurso] -= item.cantidadRequerida;
        //        }

        //        // Instanciar el prefab del item
        //        GameObject itemInstanciado = Instantiate(item.prefab);
        //        return true;
        //    }
        //}

        return false;
    }
}