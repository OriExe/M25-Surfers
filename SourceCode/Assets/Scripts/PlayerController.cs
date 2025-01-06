
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public enum playerPositions { left, middle, right }
    
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

    //Location of different sides of the map

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
        }
    }

    public void jumpPlayer()
    {
        if (!isGrounded)
        {
            Debug.Log("Can't jump");
            return;
        }
            
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }
    public void updatePlayerHorizontalPosition(int moveDirection)
    {
        playerHorizontalPostionNum = Mathf.Clamp(playerHorizontalPostionNum += moveDirection, 0, runningPositons.Length - 1); ;
    }

    void CheckIfPlayerGrounded()
    {
        Vector3 checkPosition = transform.position + new Vector3(0f, 0f, 0f);

        isGrounded = Physics.Raycast(checkPosition, -transform.up, out hit, MaxDistance, groundMask);
        Debug.Log(hit.collider.name);
    }
}
