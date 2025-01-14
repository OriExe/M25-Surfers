using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeToTurning : MonoBehaviour
{
    [SerializeField] private float timeRemaining;
    [SerializeField] private CarAction carAction;
    [SerializeField] private TuringCarAction turingCarAction;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        timeRemaining = Random.Range(45f, 110f);
    }
    void Update()
    {
        if (enabled == false) return;
        timeRemaining-= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            carAction.enabled = false;
            turingCarAction.enabled = true;
            Destroy(carAction);
            enabled = false;
        }
    }
}
