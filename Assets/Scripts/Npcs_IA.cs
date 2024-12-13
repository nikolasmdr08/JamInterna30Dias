using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npcs_IA : MonoBehaviour
{
    public float Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
