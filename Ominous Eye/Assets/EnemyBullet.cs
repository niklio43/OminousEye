using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Possessed" || collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }

}
