using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float reviveDuration = 5f;
    [SerializeField] private int points = 200;
    [SerializeField] private GhostBehavior initialBehavior;
    [SerializeField] private Transform target;

    public Movement movement { get; private set; }

    public GhostHome home { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostFrightened frightened { get; private set; }

    private Vector2 startingPosition;

    public int Points { get { return points; } }

    void Awake()
    {
        movement = GetComponent<Movement>();
        home = GetComponent<GhostHome>();
        scatter = GetComponent<GhostScatter>();
        chase = GetComponent<GhostChase>();
        frightened = GetComponent<GhostFrightened>();

        startingPosition = this.transform.position;
    }

    void Start()
    {
        ResetState();
    }

    public void ResetState(bool isGameOver = false)
    {
        this.gameObject.SetActive(true);
        movement.ResetState();
        this.transform.position = startingPosition;

        frightened.Disable();
        chase.Disable();
        scatter.Enable();

        if (home != initialBehavior)
            home.Disable();
        else
            home.Enable();

        if (initialBehavior == null)
            initialBehavior.Enable();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void Die()
    {
        Deactivate();
        Invoke(nameof(ResetState), reviveDuration);
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (frightened.enabled)
            {
                GameManager.Instance?.GhostEaten(this);
            }
            else
            {
                GameManager.Instance?.PacmanEaten();
            }
        }
	}
}
