using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Recursos _recursos;
    public int[] itemsDeTipo;
    private bool isOpen = false;
    private SpriteRenderer sprite;
    private SphereCollider sc;

    private void Start()
    {
        _recursos = FindAnyObjectByType<Recursos>();
        sprite = GetComponent<SpriteRenderer>();
        sc = GetComponent<SphereCollider>();
    }

    public void obtener() 
    {
        if (isOpen == false)
        {
            Debug.Log("loot");
            for (int i = 0; i <= itemsDeTipo.Length - 1; i++)
            {
                _recursos.obtenerRecursos(itemsDeTipo[i]);
            }
            sc.enabled = false;
            sprite.color = Color.black;
            isOpen = true;
        }
    }
}
