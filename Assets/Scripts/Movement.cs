using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField] private Vector2 initialDirection;
    [SerializeField] private LayerMask obstacleLayer;

    public Rigidbody2D Rb2d { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }

    void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        StartingPosition = this.transform.position;
    }

    void Start()
    {
        ResetState();
    }

    private void ResetState()
    {
        speedMultiplier = 1.0f;
        Direction = initialDirection;
        NextDirection = Vector2.zero;
        this.transform.position = StartingPosition;
        Rb2d.bodyType = RigidbodyType2D.Dynamic;
        this.enabled = true;
    }

    void Update()
    {
        if (NextDirection != Vector2.zero)
        {
            SetDirection(NextDirection);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = Rb2d.position;
        Vector2 translation = Direction * speed * speedMultiplier * Time.fixedDeltaTime;
        Rb2d.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (!Occupied(direction) || forced)
        {
            Direction = direction;
            NextDirection = Vector2.zero;
        }
        else
        {
            NextDirection = direction; // Queue the next direction
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            this.transform.position,
            Vector2.one * 0.75f,
            0.0f,
            direction,
            1.5f,
            obstacleLayer
        );
        return hit.collider != null;
    }
}
