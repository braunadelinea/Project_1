using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icefragment : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().DecreaseHealth(1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Boss")) {
            Destroy(this.gameObject); 
        }
    }
}
