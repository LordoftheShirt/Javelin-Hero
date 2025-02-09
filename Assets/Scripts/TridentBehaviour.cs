using Unity.Burst.CompilerServices;
using UnityEngine;

public class TridentBehaviour : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float idleFollowSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float throwSpeed = 5f;
    [SerializeField] private Transform followAnchor;
    [SerializeField] private Transform cursor;
    [SerializeField] private LayerMask layerMaskLanded, layerMaskIdle;

    private Vector2 screenPosition, lineInSpace;
    private Vector3 worldPosition;
    private Ray mouseRay;
    //private RaycastHit2D hit;

    private bool isIdlePosition = true;
    private bool hasLanded = false;

    private EdgeCollider2D edgeCollider;
    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.enabled = false;
    }

    void Update()
    {
        RecordCursorLocation();

        if (input.RetrieveTridentThrowInput())
        {
            if (isIdlePosition)
            {
                isIdlePosition = false;
                edgeCollider.enabled = true;
            }
        }

        if (input.RetrieveTridentRecallInput())
        {
            if (!isIdlePosition)
            {
                isIdlePosition = true;
                hasLanded = false;
                edgeCollider.enabled = false;
                body.excludeLayers = layerMaskIdle;
            }
        }
    }

    void LateUpdate()
    {
        if (isIdlePosition)
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

        lineInSpace = new Vector2(cursor.position.x - followAnchor.position.x, cursor.position.y - followAnchor.position.y);
        transform.up = lineInSpace;
    }

    private void TridentThrowAction()
    {
        if (!isIdlePosition)
        {
            if (!hasLanded)
            {
                transform.position = Vector2.MoveTowards(transform.position, lineInSpace * 100f, Time.deltaTime * throwSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hasLanded = true;
        body.excludeLayers = layerMaskLanded;
    }


    public Vector2 GetTridentVectorDirection()
    {
        return lineInSpace.normalized;
    }
}
