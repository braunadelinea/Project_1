using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float pWs = 1.5f; 
    private bool patternSplit = false;
    public GameObject iceFragment; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().DecreaseHealth(1);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = UnityEngine.Vector2.zero; 
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall")) {
            Destroy(this.gameObject); 
        }
    }

    private void Update()
    {
        if (!patternSplit)
        {
            int pattern = Random.Range(0, 3); 
            switch (pattern)
            {
                case 0: //wall
                    if (this.GetComponent<Rigidbody2D>().velocity == Vector2.left)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        patternSplit = true; 
                    } else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.right)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        patternSplit = true;
                    } else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.up)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + pWs, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x - pWs, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        patternSplit = true;
                    } else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.down)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        patternSplit = true;
                    }
                    Destroy(this.gameObject);
                    break;
                case 1: //diamond
                    if (this.GetComponent<Rigidbody2D>().velocity == Vector2.left)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragfour = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
                        fragfour.GetComponent<Rigidbody2D>().velocity = Vector2.left; 
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.right)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragfour = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
                        fragfour.GetComponent<Rigidbody2D>().velocity = Vector2.right; 
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.up)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragfour = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
                        fragfour.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.down)
                    {
                        GameObject fragone = Instantiate(iceFragment, this.transform.position, Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragfour = Instantiate(iceFragment, new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
                        fragfour.GetComponent<Rigidbody2D>().velocity = Vector2.down; 
                        patternSplit = true;
                    }
                    Destroy(this.gameObject);
                    break;
                case 2:
                    if (this.GetComponent<Rigidbody2D>().velocity == Vector2.left)
                    {
                        GameObject fragone = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.left;
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.right)
                    {
                        GameObject fragone = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.right;
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.up)
                    {
                        GameObject fragone = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x + pWs, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x - pWs, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.up;
                        patternSplit = true;
                    }
                    else if (this.GetComponent<Rigidbody2D>().velocity == Vector2.down)
                    {
                        GameObject fragone = Instantiate(iceFragment, new Vector3(this.transform.position.x + 2, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                        fragone.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragtwo = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y + pWs, this.transform.position.z), Quaternion.identity);
                        fragtwo.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        GameObject fragthree = Instantiate(iceFragment, new Vector3(this.transform.position.x, this.transform.position.y - pWs, this.transform.position.z), Quaternion.identity);
                        fragthree.GetComponent<Rigidbody2D>().velocity = Vector2.down;
                        patternSplit = true;
                    }
                    Destroy(this.gameObject);
                    break;
            }
        }
    }
}
