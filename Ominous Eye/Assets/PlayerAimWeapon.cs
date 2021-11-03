using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    EnemyMovement playerHasControl = new EnemyMovement();

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        
    }

    private void Update()
    {
       if (playerHasControl.isEnemy == true)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


          Vector3 aimDirection = (worldPosition - transform.position).normalized;

          float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
           aimTransform.eulerAngles = new Vector3(0, 0, angle);
        }
    }


    


}
