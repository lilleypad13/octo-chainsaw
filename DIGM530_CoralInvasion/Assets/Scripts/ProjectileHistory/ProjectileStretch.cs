using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStretch : MonoBehaviour {

    public int projectileId;


    private void Update()
    {
        ProjectileStretchFunc();
    }

    void ProjectileStretchFunc()
    {
        transform.localScale += new Vector3(0.1f * Time.deltaTime, 0, 0);
    }

}
