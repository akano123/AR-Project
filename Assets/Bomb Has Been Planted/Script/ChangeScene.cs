﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
