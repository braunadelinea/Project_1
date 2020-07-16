using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Debug.Log("DDOL " + gameObject.name);
        SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
    }
}
