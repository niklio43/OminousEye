using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Possessed")
        {
            if (Mathf.Abs(this.gameObject.transform.position.x - collision.gameObject.transform.position.x) <= 1)
            {
                if (Mathf.Abs(this.gameObject.transform.position.y - collision.gameObject.transform.position.y) <= 1)
                {
                    if (Mathf.Abs(this.gameObject.transform.position.z - collision.gameObject.transform.position.z) <= 1)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            if(collision.gameObject.tag == "Floor")
            {
                Destroy(gameObject);
            }
        }

    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }

}
