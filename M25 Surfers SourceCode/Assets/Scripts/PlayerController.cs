
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum playerPositions { left, middle, right }

    [SerializeField] private Transform CameraPos;
    [SerializeField]
    Transform[] runningPositons = new Transform[3];
    Rigidbody rb;
    #region Jump Values
    bool isGrounded = false;
    [SerializeField]
    LayerMask groundMask;
    RaycastHit hit;
    [SerializeField]
    [Tooltip("For Detecting Jumping")]
    float MaxDistance;


    [SerializeField]
    float jumpStrength;
    #endregion

    #region PowerUps
    [Header("Powerup Values")]
    [SerializeField] private GameObject Bus; //Actually a police car
    [SerializeField]private GameObject BeerEffect;
    [SerializeField] private GameObject playerMesh;
    private bool invisibility;

    private int InvisFrame = 0;
    public int invisibiltyFrames 
    { 
       get { return InvisFrame; }
       set 
       { 
            if (value <=0)
            {
                normalState();
                invisibility = true;
                InvisFrame = value; //Puts player back to normal state
                Invoke("invunerablEnded", 5f);
            }
            else if (value == 1) //If player picks up Bus
            {
                InvisFrame = value;
                animator.enabled = false;
                playerMesh.SetActive(false);
                Bus.SetActive(true);
            }
            else if (value > 1)
            {
                InvisFrame = value;
                BeerEffect.SetActive(true);
            }
       }
            } //How much hits the player can take
    #endregion
    [Space]
    //Animator
    private Animator animator;
    //Location of different sides of the map
    [SerializeField] private GameObject dustParticles;
    #region Movement Values
    //Player Left and Right Position
    [SerializeField]
    playerPositions targetHorizontalPosition = playerPositions.middle;
    private int playerHorizontalPostionNum //Gets the enum as an int
    {
        get { return (int)targetHorizontalPosition; }
        set
        {
            switch (value)
            {
                case 0:
                    targetHorizontalPosition = playerPositions.left;
                    break;
                case 1:
                    targetHorizontalPosition = playerPositions.middle;
                    break;
                case 2:
                    targetHorizontalPosition = playerPositions.right;
                    break;
            }
        }
    }
    

    [SerializeField]
    float horizontalMovementSpeed = 1f;
    #endregion
    // Start is called before the first frame update

    [SerializeField]
    string groundTag = "Ground";
    public static PlayerController instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Duplicate Detected");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayerHorizontally();

        CheckIfPlayerGrounded();
    }
    /// <summary>
    /// Move player from left to right
    /// </summary>
    void movePlayerHorizontally()
    {
        if (transform.position.x != runningPositons[playerHorizontalPostionNum].position.x)
        {
            float movementStepPerFrame = horizontalMovementSpeed * Time.deltaTime;

            Vector3 targetPosition = new Vector3(runningPositons[playerHorizontalPostionNum].position.x, transform.position.y, transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementStepPerFrame);


            CameraPos.position = Vector3.MoveTowards(CameraPos.position, new Vector3(runningPositons[playerHorizontalPostionNum].position.x, CameraPos.position.y, CameraPos.position.z), movementStepPerFrame);
        }
    }

    public void jumpPlayer()
    {
        if (!isGrounded)
        {
            Debug.Log("Can't jump");
            return;
        }
        animator.SetTrigger("Jump");
            
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
    public void updatePlayerHorizontalPosition(int moveDirection)
    {
        playerHorizontalPostionNum = Mathf.Clamp(playerHorizontalPostionNum += moveDirection, 0, runningPositons.Length - 1); ;
    }

    void CheckIfPlayerGrounded()
    {
        Vector3 checkPosition = transform.position + new Vector3(0f, 0f, 0f);

        isGrounded = Physics.Raycast(checkPosition, -transform.up, MaxDistance, groundMask);

        dustParticles.SetActive(isGrounded);
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(groundTag))
            return;

        if (invisibiltyFrames <= 0 && !invisibility)
        {
            GameManager.Instance.playerDeath();
        }

        if (invisibility || invisibiltyFrames > 0)
        {
            collision.transform.position = Vector3.forward * -50;
        }
        if (invisibiltyFrames > 0)
        {
            
            invisibiltyFrames--;
        }

        Debug.Log(invisibiltyFrames);
    }

    public void playerIsDead()
    {
        animator.SetBool("IsDead", true);
        animator.applyRootMotion = true;
    }

    //Turns off invisibility
    private void normalState()
    {
        animator.enabled = true;
        BeerEffect.SetActive(false);
        Bus.SetActive(false);
        playerMesh.SetActive(true);
    }
    /// <summary>
    /// Player is no longer invunerable
    /// </summary>
    private void invunerablEnded()
    {
        invisibility = false;
    }

}
