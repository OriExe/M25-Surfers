using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TuringCarAction : ObstableAction
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;
    [SerializeField] private float turningSpeed;
    [SerializeField] private float waitTime = 1.5f;



    /*
    private void Start() //Debug Code
    {
        action();
        transform.position = GameManager.PlayerPositionsStatic[0] + Vector3.forward * 50;
    }
    */

    /// <summary>
    /// This will figure out where the object is in game (Left middle or right) and determines where the car can turn and where it can't
    /// </summary>
    public override void action()
    {
        #region Where is the object
        int direction = 0; //-1 is turn left, 1 is turn right (From view of player)
        Vector3[] possiblePositions = GameManager.PlayerPositionsStatic;
        int objectLocation = 0;
        for (int i = 0; i < possiblePositions.Length; i++)
        {
            if (possiblePositions[i].x == transform.position.x)
            {
                objectLocation = i;
                Debug.Log("Object is in " + objectLocation);
            }
        }
        #endregion

        #region Direction that the object will turn
        //Determines where the car can turn and will turn
        switch (objectLocation)
        {
            case 0: //If Left
                direction = 1;
                break;

            case 1: //If Middle
                direction = Random.Range(-1, 1);
                if (direction == 0)
                    direction = 1;
                break;

            case 2: //If Right
                direction = -1;
                break;

            default:
                direction = 0;
                break;
        }
        #endregion

        #region Start the turning
        switch (direction)
        {
            case -1: //Turn Left
                StartCoroutine(startTurning(possiblePositions[objectLocation+direction].x));
                animator.SetTrigger("TurningRight");
                break;
            case 1: //Turn Right
                StartCoroutine(startTurning(possiblePositions[objectLocation+direction].x));
                animator.SetTrigger("TurningLeft");
                break;
            default: //Nothing happens
            break;

        }
        #endregion
    }

    IEnumerator startTurning(float MoveTo)
    {
        yield return new WaitForSeconds(waitTime / GameManager.Instance.getDifficulty());
        Vector3 newPosition = new Vector3(MoveTo, transform.position.y, transform.position.z); //Sets new position
        while (Vector3.Distance(newPosition, transform.position) > 0.2f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, turningSpeed); 
            newPosition = new Vector3(MoveTo, transform.position.y, transform.position.z);
            print("moving");
            yield return new WaitForFixedUpdate();
        }

    }
  
}