using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTargets : MonoBehaviour
{
    private Transform playerTransform;
    



    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (playerTransform != null)
        {
            Vector3 temp = transform.position;

            temp.x = playerTransform.position.x;

            temp.y = playerTransform.position.y;

            transform.position = temp;
        }
        else if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Possessed").transform;
            Vector3 temp = transform.position;

            temp.x = playerTransform.position.x;

            temp.y = playerTransform.position.y;

            transform.position = temp;
        }
        


        
    }

    
}
