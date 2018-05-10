using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSoundProjectileScript : MonoBehaviour {

    public float deathSpeed = 0.1f;
    public float growthSpeed = 1.0f;
    //public float targetScaleFactor = 2.0f;
    public int projectileId;

    private Vector3 targetScale;
    //private float timer;




    private void Update()
    {
        IncreaseSize();
        //DecreaseSize();
    }


    void DecreaseSize()
    {
        transform.localScale -= new Vector3(deathSpeed, deathSpeed, 0f) * Time.deltaTime;
    }

    void IncreaseSize()
    {
        transform.localScale += new Vector3(growthSpeed, growthSpeed, 0f) * Time.deltaTime;
    }

    //void IncreaseSize2()
    //{
    //    transform.localScale = Vector3.Lerp(transform.localScale, targetScale, growthSpeed * Time.deltaTime);
    //    if (transform.localScale == targetScale)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

}
