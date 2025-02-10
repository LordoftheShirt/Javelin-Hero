using Unity.Burst.CompilerServices;
using UnityEngine;

public class TridentBehaviour : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float idleFollowSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float throwSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float anticipationSpeed = 5f;
    [SerializeField, Range(0f, 2f)] private float anticipationTime = 0.2f;
    [SerializeField] private Transform followAnchor;
    [SerializeField] private Transform cursor;
    [SerializeField] private LayerMask layerMaskLanded, layerMaskIdle;

    private Vector2 screenPosition, normal;
    private Vector3 worldPosition, cursorToTridentDelta;
    private Ray mouseRay;
    //private RaycastHit2D hit;

    private bool isIdleState = true;
    private bool hasLanded = false;

    private EdgeCollider2D edgeCollider;
    private Rigidbody2D body;
    private float anticipationCounter;

    private void Start()
    {
        anticipationCounter = anticipationTime;
        body = GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = false;
    }

    private void Update()
    {
        RecordCursorLocation();

        if (input.RetrieveTridentThrowInput())
        {
            if (isIdleState)
            {
                isIdleState = false;
                edgeCollider.enabled = true;
            }
        }

        if (input.RetrieveTridentRecallInput())
        {
            if (!isIdleState)
            {
                isIdleState = true;
                hasLanded = false;
                edgeCollider.enabled = false;
                body.excludeLayers = layerMaskIdle;
                anticipationCounter = anticipationTime;
            }
        }
    }

    private void LateUpdate()
    {
        if (isIdleState)
        {
            UpTowardsCursor();
            transform.position = Vector2.MoveTowards(transform.position, followAnchor.position, Time.deltaTime * idleFollowSpeed);
        }
    }

    private void FixedUpdate()
    {
        TridentThrowAction();
    }

    private void RecordCursorLocation()
    {
        // Gets input of mouse, Ray "mouseRay" records the Vector2 (x and y) of the mouse position on screen relative to resolution.
        screenPosition = Input.mousePosition;
        mouseRay = Camera.main.ScreenPointToRay(screenPosition);

        // Vector3 worldPosition translates the x and y to where that position is in the real world. Since it is Vector3 (has z), z must be set to 0. Otherwise it'll be on the same z as camera.
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
    }

    private void UpTowardsCursor()
    {
        // moves "end" object to cursor location.
        cursor.position = worldPosition;

        cursorToTridentDelta = cursor.position - transform.position;
        //cursorToTridentDelta = new Vector2(cursor.position.x - transform.position.x, cursor.position.y - transform.position.y);
        transform.up = cursorToTridentDelta;
    }

    private void TridentThrowAction()
    {
        if (!isIdleState && !hasLanded)
        {
            if (0 < anticipationCounter)
            {
                transform.position += -cursorToTridentDelta.normalized * anticipationSpeed * Time.deltaTime;
                anticipationCounter -= Time.deltaTime;
            } 
            else 
            {
                transform.position += cursorToTridentDelta.normalized * throwSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //if (collision.TryGetComponent<>)
        }
        hasLanded = true;
        body.excludeLayers = layerMaskLanded;
    }


    public Vector2 GetTridentVectorDirection()
    {
        return cursorToTridentDelta.normalized;
    }

    public bool GetImpactCheck()
    {
        return hasLanded;
    }

    public Vector2 TridentStandCoordinates()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        return transform.position;
    }
}
