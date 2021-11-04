using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    private Transform possessed;
    private GameObject player;
    public float lineOfSight;
    public float shootingRange;

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            possessed = GameObject.FindGameObjectWithTag("Possessed").transform;

            float distanceFromPlayer = Vector2.Distance(possessed.position, transform.position);
            if(distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(possessed.position.x, -1.743764f), speed * Time.deltaTime);
            }
        }
    }
}
