using UnityEngine;
using System.Collections.Generic;

public class RadarDisplay : MonoBehaviour {

    #region Variables
    public GameObject[] trackedObjects;
    public List<GameObject> radarObjects;
    public GameObject radarPrefab;
    public List<GameObject> borderObjects;
    public float switchDistance = 10f;
    public Transform helpTransform;
	#endregion
	
	#region Unity Methods

	void Start ()
	{
        CreateRadarObjects();
	}
	
	void Update ()
	{
		for (int i = 0; i < radarObjects.Count; i++)
        {
            if (Vector3.Distance(radarObjects[i].transform.position, transform.position) > switchDistance)
            {
                // Switch to borderObjects
                helpTransform.LookAt(radarObjects[i].transform);
                borderObjects[i].transform.position = transform.position + switchDistance*helpTransform.forward;
                borderObjects[i].layer = LayerMask.NameToLayer("Radar");
                radarObjects[i].layer = LayerMask.NameToLayer("Invisible");
            }
            else
            {
                //Switch back to radarObjects
                borderObjects[i].layer = LayerMask.NameToLayer("Invisible");
                radarObjects[i].layer = LayerMask.NameToLayer("Radar");
            }
        }
	}

    void CreateRadarObjects()
    {
        radarObjects = new List<GameObject>();
        borderObjects = new List<GameObject>();

        foreach (GameObject o in trackedObjects)
        {
            GameObject k = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            radarObjects.Add(k);
            GameObject j = Instantiate(radarPrefab, o.transform.position, Quaternion.identity) as GameObject;
            borderObjects.Add(j);
        }
    }
	
	#endregion
}
