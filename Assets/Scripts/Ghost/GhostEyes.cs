using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;

    [Header("Sprite directions")]
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite down;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;

    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (spriteRenderer == null || movement == null) return;

        if (movement.Direction == Vector2.up)
        {
            spriteRenderer.sprite = up;
        }
        else if (movement.Direction == Vector2.down)
        {
            spriteRenderer.sprite = down;
        }
        else if (movement.Direction == Vector2.left)
        {
            spriteRenderer.sprite = left;
        }
        else if (movement.Direction == Vector2.right)
        {
            spriteRenderer.sprite = right;
        }
    }
}
