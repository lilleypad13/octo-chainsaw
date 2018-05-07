using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResourceManagement : MonoBehaviour {

    //public int resourceStockpile;
    public float growthStep = 0.5f;

    // Creates a property which updates the size of the entire base gameObject anytime the resourceStockpile value is changed.
    // This will need edited for flexibility in the future. The scaling formula could be done in a better way. Will create issues if the initial scale of the parent object is not 1.
    public int resourceStockpile
    {
        get
        {
            return _resourceStockpile;
        }
        set
        {
            _resourceStockpile = value;
            transform.parent.localScale = new Vector3(1 + growthStep * resourceStockpile, 1 + growthStep * resourceStockpile, 0f);
            Debug.Log("The current scale of the parent object is " + transform.parent.localScale);
        }
    }
    public int resourceGoal = 1;

    private int _resourceStockpile;


    private void Start()
    {
        resourceStockpile = 0;
    }

    private void Update()
    {
        if(resourceStockpile >= resourceGoal)
        {
            FindObjectOfType<GameManagerScript>().WinGame();
        }
        
    }


}
