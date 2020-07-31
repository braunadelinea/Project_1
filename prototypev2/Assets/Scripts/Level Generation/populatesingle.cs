using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populatesingle : MonoBehaviour
{
    public int spawn; //0 spawn boss 1 spawn tile 2 spawn enemy
    public GameObject[] bosses;
    public GameObject[] tiles;
    public GameObject[] enemies; 
    // Start is called before the first frame update
    void Start()
    {
        if (spawn == 0)
        {
            Instantiate(bosses[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity);
        }
        else if (spawn == 1)
        {
            Instantiate(tiles[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity);
        }
        else if (spawn == 2)
        {
            Instantiate(enemies[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
  
}
