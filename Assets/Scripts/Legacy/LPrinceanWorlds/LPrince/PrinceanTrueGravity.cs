using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PrinceanTrueGravity : MonoBehaviour
{
    [SerializeField] private GameObject gravityVectorObjectPrefabAssign;
    private int totalLengthOfArray = 13;
    [SerializeField] private Vector2 startVelocity;
    [SerializeField] private float gravityAmount = 1;
    [SerializeField] private float visualVectorLineThickness = 0.12f;


    private Rigidbody2D myBody;
    private Rigidbody2D[] astralBodyCount;
    private Vector2[] gravity, halfMagnitude;
    private float[] hypotenusan;
    private bool[] instantiateVector;
    private GameObject[] gravityVectorObject;

    private void Start()
    {
        astralBodyCount = new Rigidbody2D[totalLengthOfArray];
        gravity = new Vector2[totalLengthOfArray];
        halfMagnitude = new Vector2[totalLengthOfArray];
        hypotenusan = new float[totalLengthOfArray];
        instantiateVector = new bool[totalLengthOfArray];
        gravityVectorObject = new GameObject[totalLengthOfArray];
        myBody = GetComponent<Rigidbody2D>();
        myBody.AddForce(startVelocity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Rigidbody2D>(out Rigidbody2D otherRigidBody))
        {
            for (int i = 0; i < astralBodyCount.Length; ++i)
            {
                if (astralBodyCount[i] == otherRigidBody)
                {
                    return;
                }
                else if (astralBodyCount[i] == null)
                {
                    Debug.Log(i);
                    astralBodyCount[i] = otherRigidBody;
                    instantiateVector[i] = true;
                    return;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    
    {
        
        if (astralBodyCount[0] != null) { if (other.gameObject == astralBodyCount[0].gameObject) GravityCalculation(0); }
        if (astralBodyCount[1] != null) { if (other.gameObject == astralBodyCount[1].gameObject) GravityCalculation(1); }
        if (astralBodyCount[2] != null) { if (other.gameObject == astralBodyCount[2].gameObject) GravityCalculation(2); }
        if (astralBodyCount[3] != null) { if (other.gameObject == astralBodyCount[3].gameObject) GravityCalculation(3); }
        if (astralBodyCount[4] != null) { if (other.gameObject == astralBodyCount[4].gameObject) GravityCalculation(4); }
        if (astralBodyCount[5] != null) { if (other.gameObject == astralBodyCount[5].gameObject) GravityCalculation(5); }
        if (astralBodyCount[6] != null) { if (other.gameObject == astralBodyCount[6].gameObject) GravityCalculation(6); }
        if (astralBodyCount[7] != null) { if (other.gameObject == astralBodyCount[7].gameObject) GravityCalculation(7); }
        if (astralBodyCount[8] != null) { if (other.gameObject == astralBodyCount[8].gameObject) GravityCalculation(8); }
        if (astralBodyCount[9] != null) { if (other.gameObject == astralBodyCount[9].gameObject) GravityCalculation(9); }
        if (astralBodyCount[9] != null) { if (other.gameObject == astralBodyCount[9].gameObject) GravityCalculation(9); }
        if (astralBodyCount[10] != null) { if (other.gameObject == astralBodyCount[10].gameObject) GravityCalculation(10); }
        if (astralBodyCount[11] != null) { if (other.gameObject == astralBodyCount[11].gameObject) GravityCalculation(11); }
        if (astralBodyCount[12] != null) { if (other.gameObject == astralBodyCount[12].gameObject) GravityCalculation(12); }


    }
    
    private void FixedUpdate()
    {
        /*
        GravityCalculation(0);
        GravityCalculation(1);
        GravityCalculation(2);
        GravityCalculation(3);
        GravityCalculation(4);
        GravityCalculation(5);
        GravityCalculation(6);
        GravityCalculation(7);
        GravityCalculation(8);
        GravityCalculation(9);
        */

        // I think I must import "using IEnumerable"). This breaks the game currently:
        {
            /*
            if (gravity.Max() != null)
            {
                Vector2 strongestGravity = gravity.Max();
                transform.up = strongestGravity;
            }
            */
        }
    }
    public void GravityCalculation(int rigidBodyArraySlot)
    {
        if (astralBodyCount[rigidBodyArraySlot] != null)
        {
            // Creates a vector stretching between its own pivot and pivot of OtherObject.
            gravity[rigidBodyArraySlot] = new Vector2(astralBodyCount[rigidBodyArraySlot].transform.position.x - transform.position.x, astralBodyCount[rigidBodyArraySlot].transform.position.y - transform.position.y);
            halfMagnitude[rigidBodyArraySlot] = new Vector2((transform.position.x + astralBodyCount[rigidBodyArraySlot].transform.position.x) / 2, (transform.position.y + astralBodyCount[rigidBodyArraySlot].transform.position.y) / 2);
            
            if (astralBodyCount[rigidBodyArraySlot].gameObject.CompareTag("Player"))
            {
                astralBodyCount[rigidBodyArraySlot].transform.up = gravity[rigidBodyArraySlot];
            } 

            if (instantiateVector[rigidBodyArraySlot] == true)
            {
                // Instantiates the visual vector object:
                gravityVectorObject[rigidBodyArraySlot] = Instantiate(gravityVectorObjectPrefabAssign);
                instantiateVector[rigidBodyArraySlot] = false;
            }
            else if (gravityVectorObject != null)
            {
                // renders the visual vector object:
                gravityVectorObject[rigidBodyArraySlot].transform.position = halfMagnitude[rigidBodyArraySlot];
                hypotenusan[rigidBodyArraySlot] = gravity[rigidBodyArraySlot].magnitude;
                gravityVectorObject[rigidBodyArraySlot].transform.localScale = new Vector2(visualVectorLineThickness, hypotenusan[rigidBodyArraySlot]);
                gravityVectorObject[rigidBodyArraySlot].transform.up = gravity[rigidBodyArraySlot];
            }

            // STATEMENT BELOW IS FALSE. DON'T LISTEN. ISSUE: WHEN X/Y BECOMES 0, IT CRASHES. WHEN IT GETS NEAR 0, IT MASSIVE FOR JUST AN INSTANCE IN TIME, LEADING TO WEIRD ZIGZAG BLAST OFFS.
            // Turns the vectors into decimals so that rather than have the gravity increase as otherObject grow farther apart, gravity will decrease.
            //gravity[rigidBodyArraySlot] = new Vector2(1f / gravity[rigidBodyArraySlot].x, 1f / gravity[rigidBodyArraySlot].y);

            // gravityAmount is negative to make otherObject move in contrary to direction of the vector (otherObject will fall toward the vectors base). 
            astralBodyCount[rigidBodyArraySlot].AddForce(gravity[rigidBodyArraySlot].normalized * -gravityAmount * myBody.mass / (gravity[rigidBodyArraySlot].normalized.magnitude * gravity[rigidBodyArraySlot].magnitude));
            Debug.Log(gravityAmount * myBody.mass / (gravity[rigidBodyArraySlot].magnitude));
        }
    }
}
