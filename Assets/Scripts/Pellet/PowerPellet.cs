using UnityEngine;

public class PowerPellet : Pellet
{
    public float Duration { get; private set; } = 8f;

    protected override void Eat()
    {
        GameManager.Instance?.PowerPelletEaten(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Eat();
        }
    }
}
