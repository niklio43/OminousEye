using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Player")
       {
            Debug.Log("Hello you ominous eye, I am not letting you in");
       }

        if (collision.gameObject.tag == "EnemyRogue")
        {
            Debug.Log("Oh hello dr swansson, did you see that stupid eye? Welcome in btw!");
        }

    }

}
