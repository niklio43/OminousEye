using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarOnPlayer : MonoBehaviour
{
    public int maxEnergy = 100;
    public int currentEnergy;

    public EnergyBar energyBar;

    //Counter
    //public float startNumber = 100f;        // Starting number for energy
    //public float countSeconds = 3f;        // Time needed for counting
    //private float timeMultiplier = 0f;     // Will multiply to deltaTime for counting

    //Counter

    float timeElapsed = 100; //Count time down from 100 
    float timeSpeed = 20;    //Acceleration
    

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E))
        {
            if (currentEnergy >= 0)
            { 
            timeElapsed -= timeSpeed * Time.deltaTime;
            currentEnergy = (int)timeElapsed;
            energyBar.SetEnergy(currentEnergy);
            }
        }
        if (Input.GetKey(KeyCode.I))
        {
            if (currentEnergy <= 100)
            {
                timeElapsed += timeSpeed * Time.deltaTime;
                currentEnergy = (int)timeElapsed;
                energyBar.SetEnergy(currentEnergy);
            }

        }
    }
}


// En timer som räknar ned från 100 och använder int. 