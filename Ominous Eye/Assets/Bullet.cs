using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ScientistMovement scientistMovement;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, 5);
    }

}
