using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : PowerUp
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !used)
        {
            print("POWERUP");
            PlayerController.instance.invisibiltyFrames = 1;
            powerup.SetActive(false);
            used = true;
        }

        
    }
}
