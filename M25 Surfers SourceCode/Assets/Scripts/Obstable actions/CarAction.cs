using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarAction : ObstableAction
{
    [SerializeField] private bool isDriving;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject lighting;

    /// <summary>
    /// Randomly determines if the car is driving or not (Moves faster if it is)
    /// </summary>
    public override void action()
    {
        if (enabled == false) return;
        int randomNum = Random.Range(0, 100);
        if (randomNum < 70)
        {
            isDriving = false;
            lighting.SetActive(false);
        }
        else if (randomNum >= 70)
        {
            isDriving = true;
            lighting.SetActive(true);
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
