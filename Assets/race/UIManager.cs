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
        foreach (TextMeshProUGUI lapText in lapTexts)
        {
            if (text.Contains("\n") && lapText.tag != "Multiline") {
                lapText.text = text.Split('\n')[0];
            } else {
                lapText.text = text;
            }
        }
    }

    public void UpdateRanking(string text)
    {
        rankingText.text = text;
    }
}
