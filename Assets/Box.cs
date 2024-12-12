using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Recursos _recursos;
    public int[] itemsDeTipo;

    private void Start()
    {
        _recursos = FindAnyObjectByType<Recursos>();
    }
}
