using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour {

    public void LoadScene(String level)
    {
        if (level != "Main")
            Cursor.visible = true;
        else
            Cursor.visible = false;
        SceneManager.LoadScene(level);
    }
}
