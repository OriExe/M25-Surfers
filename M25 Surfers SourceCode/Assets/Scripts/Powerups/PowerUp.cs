using UnityEngine;

public class PowerUp : ObstableAction
{
    [SerializeField] private int length; //Length of powerup
    private float Timeleft;
    [SerializeField] protected GameObject powerup;
    
    protected bool used = false;

    private void Start()
    {
        Timeleft = length;
    }

    private void Update()
    {
        if (used) 
        {
            Timeleft -= Time.deltaTime;
            Debug.LogWarning(Timeleft);
            if (Timeleft <= 0) ///Ends effect when time lapsed
            {
                Debug.Log("Done");
                if (PlayerController.instance.invisibiltyFrames > 0) //Makes sure it doesn't give the player extra invisibility
                {
                    PlayerController.instance.invisibiltyFrames = 0;
                    Debug.Log("No more frames");
                }
           used = false;
            }
        }
    }

    /// <summary>
    /// Enables powerup
    /// </summary>
    public override void action()
    {
        Timeleft = length;
        powerup.SetActive(true);
        used = false;
    }
    

}
