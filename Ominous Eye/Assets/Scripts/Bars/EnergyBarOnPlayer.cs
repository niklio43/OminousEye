using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergyBarOnPlayer : MonoBehaviour
{
    public int maxEnergy = 100;
    public int currentEnergy;

    public EnergyBar energyBar;

    [SerializeField] private string loadSceneString;

    //Counter
    //public float startNumber = 100f;        // Starting number for energy
    //public float countSeconds = 3f;        // Time needed for counting
    //private float timeMultiplier = 0f;     // Will multiply to deltaTime for counting

    //Counter

    float timeElapsed = 100; //Count time down from 100 
    float timeSpeed = 10;    //Acceleration
    
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
    }

    // Update is called once per frame
    void Update()
    {
        //decrease energy
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            if (currentEnergy >= 0)
            { 
            timeElapsed -= timeSpeed * Time.deltaTime;
            currentEnergy = (int)timeElapsed;
            energyBar.SetEnergy(currentEnergy);
                
            }

            else
            {
                SceneManager.LoadScene(loadSceneString);
            }
        }

        //increase energy
        if (GameObject.FindGameObjectWithTag("Possessed") != null)
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


// En timer som r?knar ned fr?n 100 och anv?nder int. 