using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private int points = 10;
    public int Points { get { return points; } }

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
        {
            
        }
	}


}
