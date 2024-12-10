using System.Collections.Generic;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Configuración")]
    [SerializeField] private List<Transform> posiciones; // Listado de posiciones Transform
    [SerializeField] private List<GameObject> npcs; // Listado de NPCs
    [SerializeField] private GameObject npcPolicia; // NPC Policia

    [SerializeField] private List<GameObject> poseeNota = new List<GameObject>();
    [SerializeField] public int conversacionFinalizadaCount = 0;

    void Awake()
    {
        
    }

    void Start()
    {
        DesordenarLista(npcs);
        AsignarPosicionesYNombres();
        ActualizarConversacionesFinalizadas();
    }

    private void Update()
    {
        ActualizarConversacionesFinalizadas();
    }

    private void DesordenarLista<T>(List<T> lista)
    {
        for (int i = lista.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = lista[i];
            lista[i] = lista[randomIndex];
            lista[randomIndex] = temp;
        }
    }

    private void AsignarPosicionesYNombres()
    {
        List<Transform> posicionesDisponibles = new List<Transform>(posiciones);
        for (int i = 0; i < npcs.Count; i++)
        {
            if (posicionesDisponibles.Count == 0)
            {
                Debug.LogError("No hay suficientes posiciones para todos los NPCs.");
                return;
            }
            int randomIndex = Random.Range(0, posicionesDisponibles.Count);
            Transform posicionAsignada = posicionesDisponibles[randomIndex];
            posicionesDisponibles.RemoveAt(randomIndex);
            npcs[i].transform.position = posicionAsignada.position; // Usar la posición del Transform
            if (i < 4)
            {
                poseeNota.Add(npcs[i]);
            }
        }
    }

    public void ActualizarConversacionesFinalizadas()
    {
        conversacionFinalizadaCount = 0;

        foreach (var npc in poseeNota)
        {
            DialogSpeaker dialogSpeaker = npc.GetComponent<DialogSpeaker>();
            if (dialogSpeaker != null)
            {
                foreach (var conversacion in dialogSpeaker.conversacionesDisponibles)
                {
                    if (conversacion.finalizado)
                    {
                        conversacionFinalizadaCount++;
                        break;
                    }
                }
            }
        }

        Debug.Log($"NPCs con conversaciones finalizadas: {conversacionFinalizadaCount}");
    }
}

