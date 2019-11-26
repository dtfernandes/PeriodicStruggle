using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour {

    public string scenename;

    public void buttonpress()
    {
        SceneManager.LoadScene(scenename);
    } 
}
