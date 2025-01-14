using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beer : PowerUp
{
    /// <summary>
    /// Triggers powerup
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.invisibiltyFrames = 999;
            powerup.SetActive(false);

        }

    }

    

}
