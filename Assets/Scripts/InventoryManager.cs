using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Resources playerResources;

    public UsableTools playerTools;

    public GameObject fabricarPanel;

    public GameObject toolSlotPrefab;

    public List<Sprite> toolIcons;

    // Método para fabricar un item
    public void FabricarItem(string nombreTool)
    {

        GameObject toolUI = Instantiate(toolSlotPrefab, playerTools.InventoryUIPanel.transform);
        toolUI.GetComponent<ToolSlot>().toolName = nombreTool;
        //toolUI.GetComponent<ToolSlot>().toolImage.color = Color.red;
        if (nombreTool == "Granada") 
        {
            toolUI.GetComponent<ToolSlot>().toolImage.sprite = toolIcons[0];
        }
        if (nombreTool == "Humo")
        {
            toolUI.GetComponent<ToolSlot>().toolImage.sprite = toolIcons[1];
        }

        if (nombreTool == "Flash")
        {
            toolUI.GetComponent<ToolSlot>().toolImage.sprite = toolIcons[2];
        }

        playerResources.ActualizarRecursosEnUI();

    }
}