using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrivenTest : MonoBehaviour
{
    public Animator testingAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            testingAnimator.SetTrigger("Red");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            testingAnimator.SetTrigger("Card");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            testingAnimator.SetTrigger("Chip");
        }
    }
}
