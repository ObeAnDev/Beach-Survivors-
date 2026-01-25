using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationManager : MonoBehaviour
{
    int turnUp;
    int turnDown;
    int turnLeft;
    int turnRight;

    int turnUpRight;
    int turnDownRight;
    int turnDownLeft;
    int turnUpLeft;


    void Update()
    {
        KeyInput();
    }

    void KeyInput()
    {
        //if(Input.GetKeyDown(KeyCode.W))

    }
}
