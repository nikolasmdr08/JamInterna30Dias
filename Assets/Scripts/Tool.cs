using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewTool", menuName = "Nueva Herramienta")]
public class Tool : ScriptableObject
{
    public string toolName;

    [Header("Requisitos")]
    public int engranajes;
    public int tubosDeCobre;
    public int combustible;
    public int bombilla;
    public int resorteDeAcero;

    [TextArea (2,2)]
    public string description;

}
