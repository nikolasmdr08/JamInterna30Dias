using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    
    [Header("Configuración")]
    [SerializeField] private List<Transform> posiciones; // Listado de posiciones Transform
    [SerializeField] private List<GameObject> npcs; // Listado de NPCs
    [SerializeField] private GameObject npcPolicia; // NPC Policia
    [SerializeField] private TextMeshProUGUI textMeshPro; // Referencia al TextMeshPro
    
    [SerializeField] public List<GameObject> poseeNota = new List<GameObject>();
    public int countNotas;
    public int countNpcs;
    internal bool endGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DesordenarLista(npcs);
        AsignarPosicionesYNotas();
        ActualizarTextoPoseeNota();
    }

    void Update()
    {
        VerificarNPCsEliminados();

        if (endGame)
        {
            Debug.Log("terminado");
        }
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

    private void AsignarPosicionesYNotas()
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
                npcs[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    private void VerificarNPCsEliminados()
    {
        for (int i = poseeNota.Count - 1; i >= 0; i--)
        {
            if (poseeNota[i] == null) // Si el NPC fue eliminado del mapa
            {
                poseeNota.RemoveAt(i);
            }
        }
        ActualizarTextoPoseeNota();
    }

    public void ActualizarTextoPoseeNota()
    {
        int npcEndDialogCount = 0;

        foreach (var npc in poseeNota)
        {
            var npcController = npc.GetComponent<NpcController>();
            if (npcController != null && npcController.endDialog)
            {
                npcEndDialogCount++;
            }
        }

        countNotas = npcEndDialogCount;
        countNpcs = poseeNota.Count;

        textMeshPro.text = $"Notas: {npcEndDialogCount} / {poseeNota.Count}";
    }

    public void EliminarNPC(GameObject npc)
    {
        if (poseeNota.Contains(npc))
        {
            poseeNota.Remove(npc);
        }
        Destroy(npc);
        ActualizarTextoPoseeNota();
    }
}
