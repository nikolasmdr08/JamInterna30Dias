using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public InventoryManager inventario;

    void Start()
    {
        // Agregar recursos al inventario
        //inventario.AgregarRecurso("Madera", 10);
        //inventario.AgregarRecurso("Piedra", 5);

        // Fabricar un item
        //bool fabricado = inventario.FabricarItem("Espada");
        //Debug.Log(fabricado ? "Item fabricado" : "No se tienen recursos suficientes");
    }
}