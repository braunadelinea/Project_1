using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class populatesingle : MonoBehaviour
{
    public int spawn; //0 spawn boss 1 spawn enemy 2 right bottom tile 3 left bottom tile 4 left top tile 5 right top tile 6 left wall tile 7 right wall tile 8 floor tile
    public GameObject[] bosses;
    public GameObject[] enemies;
    public GameObject[] cornerwalltiles;
    public GameObject[] leftwalltile;
    public GameObject[] rightwalltile;
    public GameObject[] topwalltile;
    public GameObject[] bottomwalltile;
    public GameObject[] floortile;
    public GameObject[] righttopcornerfloor;
    public GameObject[] lefttopcornerfloor;
    public GameObject[] leftbottomcornerfloor;
    public GameObject[] rightbottomcornerfloor;
    public GameObject[] topfloortile;
    public GameObject[] bottomfloortile;
    public GameObject[] rightfloortile;
    public GameObject[] leftfloortile;
    public GameObject[] rocks; 
    // Start is called before the first frame update
    void Start()
    {
        if (spawn == 0)
        {
           Instantiate(bosses[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity);
        }
        else if (spawn == 1)
        {
           Instantiate(enemies[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity);
        }
        if (spawn == 2)
        {
            Instantiate(cornerwalltiles[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 3)
        {
            Instantiate(leftwalltile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 4)
        {
            Instantiate(rightwalltile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 5)
        {
            Instantiate(floortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 6)
        {
            Instantiate(topwalltile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 7)
        {
            Instantiate(bottomwalltile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 8)
        {
            Instantiate(rightbottomcornerfloor[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 9)
        {
            Instantiate(leftbottomcornerfloor[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 10)
        {
            Instantiate(righttopcornerfloor[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 11)
        {
            Instantiate(lefttopcornerfloor[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 12)
        {
            Instantiate(topfloortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 13)
        {
            Instantiate(rightfloortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 14)
        {
            Instantiate(leftfloortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 15)
        {
            Instantiate(bottomfloortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
        }
        else if (spawn == 16) {
            int spawnrock = Random.Range(0, 2);
            if (spawnrock == 1)
            {
                int rand = Random.Range(0, rocks.Length);
                Instantiate(rocks[rand], this.transform.position, Quaternion.identity, this.transform);
                Instantiate(floortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
            }
            else {
                Instantiate(floortile[GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().type], this.transform.position, Quaternion.identity, this.transform);
            }
        }
    }

    // Update is called once per frame
  
}
