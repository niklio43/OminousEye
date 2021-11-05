using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private bool inFireRange;
    private Vector3 targetPos;

    public GameObject parent;
    private GameObject possessed;
    public GameObject gun;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    private float angle;

    void Start()
    {
        inFireRange = false;
    }


    void Update()
    {
        if(parent.tag == "Possessed")
        {
            this.gameObject.GetComponent<EnemyAI>().enabled = false;
        }
        if (possessed == null)
        {
            possessed = GameObject.FindGameObjectWithTag("Possessed");
        }
        if (inFireRange)
        {
            if (parent.tag == "Enemy" && possessed)
            {
                this.gameObject.GetComponent<EnemyAI>().enabled = true;
                targetPos = possessed.transform.position;
                HandleAiming();
                HandleShooting();
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Possessed")
        {
            inFireRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Possessed")
        {
            inFireRange = false;
        }
    }
    void HandleAiming()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(targetPos);

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(parent.transform.localPosition);

        Vector2 offset = new Vector2(playerPos.x - screenPoint.x, playerPos.y - screenPoint.y);

        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 localScale = Vector3.one;

        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }

        else
        {
            localScale.y = +1f;
        }

        gun.transform.localScale = localScale;
    }

    void HandleShooting()
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
    }

}
