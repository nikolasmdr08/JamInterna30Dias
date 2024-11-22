using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeSlot : MonoBehaviour
{
    private Resources resc;
    private InventoryManager IM;

    public string toolName;
    public TMP_Text toolNameText;
    public int req0;
    public int req1;
    public int req2;
    public int req3;
    public int req4;
    public List<TMP_Text> reqs_texts;
    public Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        resc = GameObject.FindObjectOfType<Resources>();
        IM = GameObject.FindObjectOfType<InventoryManager>();

        toolNameText.text = toolName;
        reqs_texts[0].text = req0.ToString();
        reqs_texts[1].text = req1.ToString();
        reqs_texts[2].text = req2.ToString();
        reqs_texts[3].text = req3.ToString();
        reqs_texts[4].text = req4.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        CheckRequirements();
    }

    public void CheckRequirements() 
    {
        thisButton.interactable = true;

        if (resc.recursos[0] >= req0)
        {
            reqs_texts[0].color = Color.green;
        }
        else { reqs_texts[0].color = Color.black; thisButton.interactable = false; }

        if (resc.recursos[1] >= req1)
        {
            reqs_texts[1].color = Color.green;
        }
        else { reqs_texts[1].color = Color.black; thisButton.interactable = false; }

        if (resc.recursos[2] >= req2)
        {
            reqs_texts[2].color = Color.green;
        }
        else { reqs_texts[2].color = Color.black; thisButton.interactable = false; }

        if (resc.recursos[3] >= req3)
        {
            reqs_texts[3].color = Color.green;
        }
        else { reqs_texts[3].color = Color.black; thisButton.interactable = false; }

        if (resc.recursos[4] >= req4)
        {
            reqs_texts[4].color = Color.green;
        }
        else { reqs_texts[4].color = Color.black; thisButton.interactable = false; }
    }

    public void FabricTool() 
    {
        resc.recursos[0] = resc.recursos[0] - req0;
        resc.recursos[1] = resc.recursos[1] - req1;
        resc.recursos[2] = resc.recursos[2] - req2;
        resc.recursos[3] = resc.recursos[3] - req3;
        resc.recursos[4] = resc.recursos[4] - req4;

        IM.FabricarItem(toolName);
    }
}
