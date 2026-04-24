using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
	private Movement movement;

	void Awake()
	{
		movement = GetComponent<Movement>();
	}

	public void ResetState()
	{
		this.gameObject.SetActive(true);
		movement.ResetState();
	}

	public void Deactivate()
	{
		this.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			movement.SetDirection(Vector2.up);
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			movement.SetDirection(Vector2.down);
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			movement.SetDirection(Vector2.left);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			movement.SetDirection(Vector2.right);
		}

		float angle = Mathf.Atan2(this.movement.Direction.y, this.movement.Direction.x);
		this.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
	}
}
