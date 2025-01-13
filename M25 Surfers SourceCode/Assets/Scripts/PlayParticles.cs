using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem onLandParticlesPlay;
    private void OnEnable()
    {
        onLandParticlesPlay.Play();
    }
}
