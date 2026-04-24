using UnityEngine;

public class PowerPellet : Pellet
{
    [SerializeField] private float duration = 8f;

    protected override void Consume()
    {
        base.Consume();
        // StartCoroutine(FindObjectOfType<GameManager>().PowerPelletEffect(duration));
    }
}
