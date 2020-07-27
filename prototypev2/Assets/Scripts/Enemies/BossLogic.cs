using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    private float bulletbuffer; 
    private float counter; 
    public GameObject proj;
    public GameObject enemy; 
    private float projdamage;
    private int health = 10; 

    void Update()
    {
        bulletbuffer += Time.deltaTime; 
        counter += Time.deltaTime; 
        int attack = Random.Range(0, 2);
        if (attack == 0 && counter > 2)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(this.transform.position, 5f);
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].gameObject.CompareTag("player"))
                {
                    GameObject.FindGameObjectWithTag("player").GetComponent<PlayerManager>().DecreaseHealth(1);
                    //particles? 
                }
            }
            attack = Random.Range(0, 2);
            counter = 0;
        }
        else if (attack == 1 && counter > 2) {
            for (int i = 0; i < 3; i++)
            {
                if (bulletbuffer > 0.25)
                {
                    GameObject instance = Instantiate(proj, transform.position, Quaternion.identity);
                    instance.GetComponent<Rigidbody2D>().velocity = (GameObject.FindGameObjectWithTag("player").transform.position - this.transform.position) * 1.2f;
                    bulletbuffer = 0; 
                }
            }
            attack = Random.Range(0, 2); 
            counter = 0;
        }
             
    }

}
