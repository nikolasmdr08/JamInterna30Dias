using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricableTool : MonoBehaviour
{
    public string nombre;
    public int[] recursosRequeridos; // Indices corresponden a recursos en el diccionario
    public int cantidadRequerida; // Cantidad requerida de cada recurso
    public GameObject prefab; // Prefab del item

    // Referencia al inventario
    public InventoryManager inventario;

    void Start()
    {
        //// Registrar el item en el inventario
        //inventario.items.Add(nombre, new InventoryManager.Tool
        //{
        //    nombre = nombre,
        //    recursosRequeridos = recursosRequeridos,
        //    cantidadRequerida = cantidadRequerida,
        //    prefab = prefab
        //});
    }
}