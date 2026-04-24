using UnityEngine;

public class GhostScatter : GhostBehavior
{
	void OnDisable()
	{
		ghost.chase.Enable();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		Node node = collision.GetComponent<Node>();
        if (node && this.enabled && !ghost.frightened.enabled)
        {
            int index = Random.Range(0, node.AvailableDirections.Count);
            while (node.AvailableDirections[index] == -ghost.movement.Direction && 
                node.AvailableDirections.Count > 1)
            {
                index = Random.Range(0, node.AvailableDirections.Count);
            }
            // Debug.Log("Scatter: " + node.name + " " + node.AvailableDirections[index]);
            ghost.movement.SetDirection(node.AvailableDirections[index]);
        }
	}
}
