using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameScript : MonoBehaviour
{
    public GameObject thisPanel;
    public GameObject inputField;
    public GameObject canvas;

    public void FinishEdit()
    {
        PlayerPrefs.SetString("CurrentPlayerName",inputField.GetComponent<InputField>().text);
        canvas.GetComponent<GiveScoreAndName>().GiveValues();
        thisPanel.SetActive(false);
    }
}
