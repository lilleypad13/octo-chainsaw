using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSoundProjectileScript : MonoBehaviour {

    public float growthSpeed = 1.0f;
    //public float targetScaleFactor = 2.0f;
    public int projectileId;

    private Vector3 targetScale;
    //private float timer;




    private void Update()
    {
        IncreaseSize();
    }

    void IncreaseSize()
    {
        transform.localScale += new Vector3(growthSpeed, growthSpeed, 0f) * Time.deltaTime;
    }

}
