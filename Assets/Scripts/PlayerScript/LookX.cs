using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float sensibilityInX = 1;
    private int playerNumber;
    private int joystickNumber;
    // Start is called before the first frame update
    void Start()
    {
        FisrtPersonCharacterController character = this.GetComponent<FisrtPersonCharacterController>();
        playerNumber = character.playerNumber;
        joystickNumber = character.joystickNumber;
    }

    // Update is called once per frame
    void Update()
    {

        float viewMovementX;
        if(playerNumber == 1)
        {
            viewMovementX = Input.GetAxis("Mouse X");

            if(viewMovementX == 0)
            {
                viewMovementX = Input.GetAxis("Mouse X_Joystick"+ joystickNumber);
            }
        }
        else
        {
            viewMovementX = Input.GetAxis("Mouse X_Joystick" + joystickNumber);
        }

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += (viewMovementX * sensibilityInX);
        transform.localEulerAngles = newRotation;
        
    }
}
