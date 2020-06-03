using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float sensibilityInY = 1;
    [SerializeField]
    private bool InvertLookY = false;
    private int playerNumber;
    private int joystickNumber;
    // Start is called before the first frame update
    void Start()
    {
        FisrtPersonCharacterController character = this.GetComponentInParent<FisrtPersonCharacterController>();
        playerNumber = character.playerNumber;
        joystickNumber = character.joystickNumber;
    }

    // Update is called once per frame
    void Update()
    {
        float viewMovementY = 0;
        if(playerNumber == 1)
        {
            viewMovementY = Input.GetAxis("Mouse Y");

            if (viewMovementY == 0)
            {
                viewMovementY = Input.GetAxis("Mouse Y_Joystick" + joystickNumber);
            }
            
        }
        else
        {
            viewMovementY = Input.GetAxis("Mouse Y_Joystick" + joystickNumber);
        }

        Vector3 newRotation = transform.localEulerAngles;
        
        if(!InvertLookY)
        {
            newRotation.x += (viewMovementY * sensibilityInY);
        }
        else
        {
            newRotation.x -= (viewMovementY * sensibilityInY);
        }
        transform.localEulerAngles = newRotation;

    }
}
