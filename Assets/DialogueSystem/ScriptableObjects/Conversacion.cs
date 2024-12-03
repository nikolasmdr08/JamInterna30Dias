using UnityEngine;

[CreateAssetMenu(fileName ="Conversacion",menuName ="Sistema de Dialogos/Nueva Conversacion")]
public class Conversacion : ScriptableObject
{
    [System.Serializable]
    public struct Linea{
        public Personaje personaje;
        [TextArea(3,5)]
        public string dialogo;
        public AudioClip sonido;
    }

    public bool desbloqueada;
    public bool finalizado;
    public bool reUsar;

    /*[ReorderableList]*/
    [SerializeField]
    public Linea[] dialogos;

    public Pregunta pregunta;

}