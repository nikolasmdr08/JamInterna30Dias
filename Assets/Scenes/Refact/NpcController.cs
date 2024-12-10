using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public bool endDialog = false;
    public bool repeat = false;
    public string[] dialogWithNote = { "Hola, se lo que estas buscando", "Tengo informacion que puede ayudarte", "Espero que puedas atraparlo" };
    public string[] dialogOutNote = {"No me molestes", "Las calles ya no son seguras", "Espero que alguien detenga a ese monstruo"};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
