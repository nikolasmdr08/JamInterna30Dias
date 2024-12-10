using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceController : MonoBehaviour
{
    public bool endDialog = false;
    public bool repeat = false;
    public string[] dialogue;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.countNotas >= GameManager.instance.countNpcs)
        {
            dialogue = new string[] { "Tiene pistas importantes", "Usted es un heroe nos acercara a poner fin al criminal", "(Este hombre sabe demaciado...)","" };
        }
        else
        {
            dialogue = new string[] { "No me molestes", "Estamos muy atareados buscando al criminal", "No tengo tiempo para un pseudodetective...", "" };
        }

        if (GameManager.instance.countNotas >= GameManager.instance.countNpcs && endDialog)
        {
            GameManager.instance.endGame = true;
        }
    }
}
