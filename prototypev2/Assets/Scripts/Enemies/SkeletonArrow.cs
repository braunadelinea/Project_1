using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    private Vector3 direction;
    private float timer = 0;
    private float lifespan = 2;
    // Start is called before the first frame update
    void Start()
    {
        direction = (GameObject.FindGameObjectWithTag("player").transform.position - transform.position).normalized;
        Debug.Log("Arrow Direction: " + direction);
    }

    void Update()
    {
        gameObject.transform.position += direction * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > lifespan)
        {
            Destroy(gameObject);
        }
    }
}
