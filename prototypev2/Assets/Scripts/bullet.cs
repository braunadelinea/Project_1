using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().DecreaseHealth(1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero; 
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Tile")) {
            Destroy(this.gameObject); 
        }
    }
}
