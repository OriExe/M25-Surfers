using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : ObstableAction
{
    [SerializeField] private int length; //Length of powerup
    [SerializeField] protected GameObject powerup;
    private void OnTriggerEnter(Collider other)
    {
       
    }

    /// <summary>
    /// Enables powerup
    /// </summary>
    public override void action()
    {
        powerup.SetActive(true);
    }
}
