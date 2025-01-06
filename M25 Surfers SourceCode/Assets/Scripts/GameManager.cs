using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent onDeathEvents;

    public static GameManager Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void playerDeath()
    {
        onDeathEvents.Invoke();
    }
}
