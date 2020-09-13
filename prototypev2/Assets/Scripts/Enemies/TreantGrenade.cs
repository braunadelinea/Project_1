using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreantGrenade : MonoBehaviour
{
    private float explosionTimer;
    private float growthTimer; 
    private float explosionRadius = 2;
    private GameObject indicator; 
    private void Update()
    {
        Vector3 scaleChange = new Vector3(0.8f, 0.8f, 0);
        explosionTimer += Time.deltaTime;
        if (growthTimer > 0.25) {
            indicator.transform.localScale += scaleChange;
            growthTimer = 0; 
        }
        if (explosionTimer > 1.5f) {
            Collider2D[] collider = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);
            for (int i = 0; i < collider.Length; i++) {
                if (collider[i].gameObject.tag == "player") {
                    collider[i].gameObject.GetComponent<PlayerManager>().DecreaseHealth(2);
                }
            }
            Destroy(this.gameObject);
            Destroy(indicator.gameObject); 
        }
    }
}
