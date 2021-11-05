using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private bool inFireRange;
    private Vector3 targetPos;

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
        if (possessed == null)
        {
            possessed = GameObject.FindGameObjectWithTag("Possessed");
        }
        if (inFireRange)
        {
            if (this.gameObject.tag == "Enemy")
            {
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

        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

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


        Debug.Log("Target Pos: " + screenPoint);
        Debug.Log("Mouse Pos: " + Input.mousePosition);
    }

    void HandleShooting()
    {
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
    }

}
