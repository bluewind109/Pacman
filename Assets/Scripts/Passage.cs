using UnityEngine;

public class Passage : MonoBehaviour
{
    [SerializeField] private Transform otherEnd;

	void OnTriggerEnter2D(Collider2D collision)
	{
		Vector3 position = otherEnd.position;
		collision.transform.position = position;
	}
}
