using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmartMovement : MonoBehaviour {

    // Fix a range how early u want your enemy detect the obstacle.
    private int range;
    public float initialMoveSpeed = 2.0f;
    public float currentMoveSpeed;
    //private float speed;
    private bool isThereAnyThing = false;
    public int rotationSpeed = 2;

    // Specify the target for the enemy.
    //public GameObject target;
    private RaycastHit hit;
    private Transform target;
    private Transform myTransform;

    //private int layerMask;
    public LayerMask environmentLayer;


    void Awake()
    {
        myTransform = transform;
    }

    void Start()
    {
        range = 2;
        //speed = 10f;
        currentMoveSpeed = initialMoveSpeed;
        //rotationSpeed = 15f;
        GameObject go = GameObject.FindGameObjectWithTag("Base");
        target = go.transform;
        //layerMask = 1 << 11;
    }

    void Update()
    {
      /*  //// create the tiles map
        float[,] tilesmap = new float[width, height];
        // set values here....
        // every float in the array represent the cost of passing the tile at that position.
        // use 0.0f for blocking tiles.

        // create a grid
        PathFind.Grid grid = new PathFind.Grid(width, height, tilesmap);

        // create source and target points
        PathFind.Point _from = new PathFind.Point(1, 1);
        PathFind.Point _to = new PathFind.Point(10, 10);

        // get path
        // path will either be a list of Points (x, y), or an empty list if no path is found.
        List<PathFind.Point> path = PathFind.Pathfinding.FindPath(grid, _from, _to);*/

        //Look At Somthly Towards the Target if there is nothing in front.
        if (!isThereAnyThing)
        {
            Vector3 relativePos = target.position - myTransform.position;
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.FromToRotation(Vector3.right, relativePos),
                Time.deltaTime * rotationSpeed);
        }

        // Enemy translate in forward direction.

        //transform.Translate(Vector3.forward * Time.deltaTime * currentMoveSpeed);
        myTransform.position += transform.right * currentMoveSpeed * Time.deltaTime; // Makes sure enemy travels the direction it is currently facing

        //Checking for any Obstacle in front.
        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = transform;
        Transform rightRay = transform;
        //Use Phyics.RayCast to detect the obstacle

        if (Physics2D.Raycast(leftRay.position + (transform.up / 8), transform.right, range, environmentLayer) ||
            Physics2D.Raycast(rightRay.position - (transform.up / 8), transform.right, range, environmentLayer))
        {
            isThereAnyThing = true;
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed *20);
            Debug.Log("Obstacle Seen");
        }

        //// Attempting to make physics2D version of raycast logic seen below
        
        //// Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        //// Just making this boolean variable false it means there is nothing in front of object.

        if(Physics2D.Raycast(transform.position - (transform.right / 8), transform.up, range, environmentLayer) ||
            Physics2D.Raycast(transform.position - (transform.right / 8), -transform.up, range, environmentLayer))
        {
            isThereAnyThing = false;
        }


        // These are the actual rays
        Debug.DrawRay(transform.position - (transform.right / 8), -transform.up * 2, Color.yellow);
        Debug.DrawRay(transform.position - (transform.right / 8), transform.up * 2, Color.yellow);
        Debug.DrawRay(transform.position + (transform.up / 8), transform.right * 2, Color.red);
        Debug.DrawRay(transform.position - (transform.up / 8), transform.right * 2, Color.red);
    }
    
    public void Slow()
    {
        currentMoveSpeed = currentMoveSpeed / 2;
        Debug.Log("Enemy has been slowed to value: " + currentMoveSpeed);
    }

    public void Exit()
    {
        currentMoveSpeed = currentMoveSpeed * 2;
        Debug.Log(currentMoveSpeed);
    }
}

