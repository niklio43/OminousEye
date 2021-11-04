using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelExpload : MonoBehaviour
{

    [Header("Particles")]
    [SerializeField] private ParticleSystem particlesystem;
    [SerializeField] private int emitAmount;
    [SerializeField] private ParticleSystem particlesystem2;
    [SerializeField] private int emitAmount2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("BABOOOM!");
            EmitParticles(emitAmount);
            EmitParticles(emitAmount2);
        }

    }

    void EmitParticles(int emitamount)
    {
        particlesystem.Emit(emitAmount);
        particlesystem2.Emit(emitAmount2);
    }

}
