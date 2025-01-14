using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TouchInputManager : MonoBehaviour
{
    #region Touch values
    /// <summary>
    /// Number of pixels for threadhold to be reached
    /// O is horizontal
    /// 1 is Vertical
    /// </summary>
    private float[] pixelSwipeThreadholdNumber = { 0f, 1f };

    [SerializeField] private LineRenderer DebugLine;
    [Range(0f, 100f)]
    [SerializeField] private int percentageThressholH = 18;
    [Range(0f, 100f)]
    [SerializeField]private int percentageThressholV = 18;
    
    #region Touch Movement

    Vector2 StartTouchPosition;

    bool swipeMadeHorizontal = false;
    bool swipeMadeVertical = false;


    bool swipeCurrentAppliedHorizontal = false;
    bool swipeCurrentAppliedVertical = false;

    #endregion
    #endregion

    #region Player Position (Enum)
    public enum FingerHorizontalPos
    {
        left,
        middle,
        right
    }
    enum FingerVerticalPos
    {
        up,
        none,
        Slide
    }

    /// <summary>
    /// Left right or in the Middle
    /// </summary>
    FingerHorizontalPos FingerplayerSwipeHoriz = FingerHorizontalPos.middle;
    /// <summary>
    /// Is the player on the ground, sliding or in the air
    /// </summary>
    FingerVerticalPos FingerplayerSwipeVertic = FingerVerticalPos.none;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //Calculates number of pixels the player needs to swipe to register input
        pixelSwipeThreadholdNumber[0] = Screen.width * (percentageThressholH / 100f);
        pixelSwipeThreadholdNumber[1] = Screen.height * (percentageThressholV / 100f);
        print(pixelSwipeThreadholdNumber[0]);
        print(pixelSwipeThreadholdNumber[1]);
    }

  
    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            //Stores the value of the first finger to touch the screen
            Touch touch = Input.GetTouch(0);

            switch (touch.phase) //Determines where finger has moved
            {
                case TouchPhase.Began:
                    print("Finger Placed on screen");
                    StartTouchPosition = touch.position;
                    break;
                    
                case TouchPhase.Moved:
                    print("Finger Moved");
                    //Calcuates how much pixels the player has swiped their finger on screen
                    float amountDraggedHorizontal = touch.position.x - StartTouchPosition.x;
                    float amountDraggedVertical = touch.position.y - StartTouchPosition.y;
                    //Mathf.Abs makes it positive if negative
                    if (Mathf.Abs(amountDraggedHorizontal) > pixelSwipeThreadholdNumber[0] && (!swipeMadeVertical && !swipeMadeHorizontal))
                    {
                        swipeMadeHorizontal = true;
                        if (amountDraggedHorizontal > 0)
                        {
                            Debug.Log("Moved to right");
                            FingerplayerSwipeHoriz = FingerHorizontalPos.right;
                            DebugLine.material.color = Color.yellow;
                        }
                        else
                        {
                            Debug.Log("Moved to left");
                            FingerplayerSwipeHoriz = FingerHorizontalPos.left;
                            DebugLine.material.color = Color.yellow;
                            
                        }
                    }
                    if (Mathf.Abs(amountDraggedVertical) > pixelSwipeThreadholdNumber[1] && (!swipeMadeVertical && !swipeMadeHorizontal))
                    {
                        swipeMadeVertical = true;
                        if (amountDraggedVertical > 0)
                        {
                            Debug.Log("Moved to up");
                            FingerplayerSwipeVertic = FingerVerticalPos.up;
                            DebugLine.material.color = Color.yellow;
                        }
                        else
                        {
                            Debug.Log("Moved to down");
                            FingerplayerSwipeVertic = FingerVerticalPos.Slide;
                            DebugLine.material.color = Color.yellow;
                        }
                    }
                    break;
                    
                case TouchPhase.Ended:
                    print("Finger Off");
                    //Changes all variables to their neutral position
                    swipeMadeVertical = swipeMadeHorizontal = false;
                    swipeCurrentAppliedHorizontal = swipeCurrentAppliedVertical = false;
                    
                    FingerplayerSwipeHoriz = FingerHorizontalPos.middle;
                    FingerplayerSwipeVertic = FingerVerticalPos.none;
                    break;
                
            }
            if (!swipeCurrentAppliedHorizontal && FingerplayerSwipeHoriz != FingerHorizontalPos.middle)
            {
                swipeCurrentAppliedHorizontal = true;
                //If is left or right
                if (FingerplayerSwipeHoriz == FingerHorizontalPos.left)
                {
                    PlayerController.instance.updatePlayerHorizontalPosition(-1);
                }
                else
                {
                    PlayerController.instance.updatePlayerHorizontalPosition(1);
                }
            }
            if (!swipeCurrentAppliedVertical && FingerplayerSwipeVertic != FingerVerticalPos.none)
            {
                swipeCurrentAppliedVertical = true;
                if (FingerplayerSwipeVertic == FingerVerticalPos.up)
                {
                    PlayerController.instance.jumpPlayer();
                }
                
            }
        }

    }
}
