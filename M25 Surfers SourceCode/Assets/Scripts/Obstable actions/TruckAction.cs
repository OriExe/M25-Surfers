using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class TruckAction : ObstableAction
{
    [SerializeField] private bool isDriving;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    public override void action()
    {
        int randomNum = Random.Range(0, 100);
        if (randomNum < 70)
        {
            isDriving = false;
        }
        else if (randomNum >= 70)
        {
            isDriving = true;
        }
    }

    private void Update()
    {
        if (isDriving)
        {
            rb.velocity = -Vector3.forward * speed;
        }
    }
}
