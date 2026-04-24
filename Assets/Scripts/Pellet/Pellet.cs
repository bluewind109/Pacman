using UnityEngine;

public class Pellet : MonoBehaviour
{
    [SerializeField] private int points = 10;
    public int Points { get { return points; } }

    protected virtual void Eat()
    {
        GameManager.Instance?.PelletEaten(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Eat();
        }
    }


}
