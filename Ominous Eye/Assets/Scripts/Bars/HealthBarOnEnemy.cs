using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarOnEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Auuuuch that hurts, get this fkn ey out of me!");
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Auuuuch that hurts, get this fkn ey out of me!");
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
