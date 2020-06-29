using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class panelmanager : MonoBehaviour
{
    public GameObject inventorypanel;
    // Start is called before the first frame update
    void Start()
    {
        inventorypanel.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (inventorypanel.activeSelf)
            {
                inventorypanel.SetActive(false);
            }
            else {
                inventorypanel.SetActive(true);
            }
        }
    }
}
