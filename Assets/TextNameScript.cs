using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextNameScript : MonoBehaviour
{
    public string pref;

    void Update()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString(pref);
    }
}
