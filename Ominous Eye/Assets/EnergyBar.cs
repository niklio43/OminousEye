using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnergyBar : MonoBehaviour
{
    Image energyBar;
    public float maxPower = 5f;
    float timeLeft;
    float rePower = 0.5f;
    public GameObject timesUpText;
    GameObject eye;


    // Start is called before the first frame update
    void Start()
    {
        eye = GameObject.Find("Player");
        timesUpText.SetActive(false);
        energyBar = GetComponent<Image>();
        timeLeft = maxPower;
    }

    // Update is called once per frame
    void Update()
    {
        if (eye != null)
        {
            timeLeft -= Time.deltaTime;
            energyBar.fillAmount = timeLeft / maxPower;
            Debug.Log(energyBar);
        }
        if (eye == null)
        {
            energyBar.fillAmount += rePower * Time.deltaTime;
        }
        if (energyBar.fillAmount == 0)
        {
            timesUpText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
