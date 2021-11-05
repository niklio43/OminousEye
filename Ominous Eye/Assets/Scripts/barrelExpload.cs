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


    void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "Possessed")
            {
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
