using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    private bool lbOpen;
    // Start is called before the first frame update
    void Start()
    {
        lbOpen = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.tag == "Card")
       {
            anim.SetBool("lbOpen", true);
            Destroy(this.gameObject, 1);
        }

    }

}
