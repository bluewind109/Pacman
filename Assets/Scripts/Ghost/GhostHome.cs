using UnityEngine;

public class GhostHome : GhostBehavior
{
	[SerializeField] private Transform inside;
	[SerializeField] private Transform outside;

	public Transform Inside { get { return inside; } }

	private Awaitable exitTransition;

	void OnEnable()
	{
		if (exitTransition != null) exitTransition.Cancel();
	}

	void OnDisable()
	{
		if (this.gameObject.activeSelf)
			exitTransition = ExitTransition();
	}

	private async Awaitable ExitTransition()
	{
		Debug.Log("Exiting ghost home...");
		ghost.movement.SetDirection(Vector2.up, true);
		ghost.movement.Rb2d.bodyType = RigidbodyType2D.Kinematic;
		ghost.movement.enabled = false;

		Vector3 position = this.transform.position;

		float duration = 0.5f;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			Vector3 newPosition = Vector3.Lerp(position, inside.position, elapsed / duration);
			newPosition.z = 0f;
			ghost.transform.position = newPosition;
			elapsed += Time.deltaTime;
			await Awaitable.NextFrameAsync();
		}

		elapsed = 0f;

		while (elapsed < duration)
		{
			Vector3 newPosition = Vector3.Lerp(inside.position, outside.position, elapsed / duration);
			newPosition.z = 0f;
			ghost.transform.position = newPosition;
			elapsed += Time.deltaTime;
			await Awaitable.NextFrameAsync();
		}

		Vector2 newDirection = new Vector2(Random.value < 0.5f ? -1 : 1, 0);
		ghost.movement.SetDirection(newDirection, true);
		ghost.movement.Rb2d.bodyType = RigidbodyType2D.Dynamic;
		ghost.movement.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
		{
			ghost.movement.SetDirection(-ghost.movement.Direction, true);
		}
	}
}
