using UnityEngine;

public class GhostFrightened : GhostBehavior
{
	[SerializeField] private SpriteRenderer body;
	[SerializeField] private SpriteRenderer eyes;
	[SerializeField] private SpriteRenderer blue;
	[SerializeField] private SpriteRenderer white;

	public bool eaten { get; private set; }

	void OnEnable()
	{
		ghost.movement.speedMultiplier = 0.5f;
	}

	void OnDisable()
	{
		ghost.movement.speedMultiplier = 1.0f;
		this.eaten = false;
	}

	public override void Enable(float duration)
	{
		base.Enable(duration);

		this.body.enabled = false;
		this.eyes.enabled = false;
		this.blue.enabled = true;
		this.white.enabled = false;

		Invoke(nameof(Flash), duration / 2.0f);
	}

	public override void Disable()
	{
		base.Disable();

		this.body.enabled = true;
		this.eyes.enabled = true;
		this.blue.enabled = false;
		this.white.enabled = false;
	}

	private void Flash()
	{
		if (this.eaten) return;

		this.blue.enabled = false;
		this.white.enabled = true;
		this.white.GetComponent<AnimatedSprite>().Restart();
	}

	public void Eaten()
	{
		this.eaten = true;
		this.body.enabled = false;
		this.eyes.enabled = true;
		this.blue.enabled = false;
		this.white.enabled = false;

		Vector3 position = ghost.home.Inside.position;
		position.y = 0;
		ghost.transform.position = position;

		ghost.home.Enable(this.Duration);
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Node node = collision.GetComponent<Node>();
		if (node && this.enabled)
		{
			Vector2 direction = Vector2.zero;
			float maxDistance = float.MinValue;
			foreach (Vector2 availableDirection in node.AvailableDirections)
			{
				Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
				float distance = (ghost.target.position - newPosition).sqrMagnitude;

				if (distance > maxDistance)
				{
					direction = availableDirection;
					maxDistance = distance;
				}
			}

			ghost.movement.SetDirection(direction);
		}
	}
}
