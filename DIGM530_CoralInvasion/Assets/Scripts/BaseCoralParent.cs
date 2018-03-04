using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCoralParent : MonoBehaviour {

    public float growthSpeed = 1.0f;
    public float winScale = 2.0f;

    private void Update()
    {
        IncreaseSize();
        WinCondition();
    }


    void IncreaseSize()
    {
        transform.localScale += new Vector3(growthSpeed, growthSpeed, 0f) * Time.deltaTime;
    }

    void WinCondition()
    {
        if (transform.localScale.x >= winScale) //Only checks scale on the x-axis, but should be the same all around
        {
            FindObjectOfType<GameManagerScript>().WinGame();
        }
    }
}
