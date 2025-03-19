using Unity.Burst.CompilerServices;
using UnityEngine;

public class TridentBehaviour : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float idleFollowSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float throwSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float anticipationSpeed = 5f;
    [SerializeField, Range(0f, 2f)] private float anticipationTime = 0.2f;
    [SerializeField, Range(0f, 90f)] private float noAnticipationAngle = 40;
    [SerializeField] private Transform followAnchor;
    [SerializeField] private Transform cursor;
    [SerializeField] private LayerMask layerMaskLanded, layerMaskIdle;

    private Vector2 normal;
    private Vector3 cursorToTridentDelta;

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
        if (input.RetrieveTridentThrowInput())
        {
            if (isIdleState)
            {
                isIdleState = false;
                edgeCollider.enabled = true;
                FindObjectOfType<AudioManager>().Play("Throw0");
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

    private void UpTowardsCursor()
    {
        // moves "cursor" object to cursor location. I think this dude doesn't actually have to exist. I just like him.
        cursor.position = RecordCursor.GetCursorPosition();

        cursorToTridentDelta = cursor.position - transform.position;
        transform.up = cursorToTridentDelta;
    }

    private void TridentThrowAction()
    {
        if (!isIdleState && !hasLanded)
        {
            // Anticipation animation: Checks if the anticipation counter is activated, so that the spear isn't pointed far enough down in which case it should fire instantaneously.
            if (0 < anticipationCounter && (transform.eulerAngles.z > 180 + noAnticipationAngle || transform.eulerAngles.z < 180 - noAnticipationAngle))
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
            if (collision.TryGetComponent<SimpleEnemyBird>(out SimpleEnemyBird component))
            {
                component.Die();
            }
        }
        else
        {
            if (!hasLanded)
            {
                FindObjectOfType<AudioManager>().Play("Hit0");
            }
            hasLanded = true;
            body.excludeLayers = layerMaskLanded;
        }
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
