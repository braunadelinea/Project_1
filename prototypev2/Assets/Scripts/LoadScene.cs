﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class LoadScene : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(1); 
    }
    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(3); 
    }

}
