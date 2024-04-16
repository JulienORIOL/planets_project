using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<TextMeshProUGUI> lapTexts;
    public TextMeshProUGUI rankingText;
    // Update is called once per frame

    public void UpdateLapText(string text)
    {
        StartCoroutine(UpdateLapTextCoroutine(text));
    }

    private IEnumerator UpdateLapTextCoroutine(string text)
    {
        foreach (TextMeshProUGUI lapText in lapTexts)
        {
            if (text.Contains("\n") && lapText.tag != "Multiline")
            {
                text = text.Split('\n')[0];
            }

            lapText.text = text;
            yield return null; // Yield to the next frame
        }
    }


    public void UpdateRanking(string text)
    {
        rankingText.text = text;
    }
}

    
    
    
    /*public void UpdateLapText(string text)
    {
        Debug.Log("UpdateLapText " + text);
        foreach (TextMeshProUGUI lapText in lapTexts)
        {
            if (text.Contains("\n") && lapText.tag != "Multiline") {
                lapText.text = text.Split('\n')[0];
            } else {
                lapText.text = text;
            }
        }
    }
}
*/