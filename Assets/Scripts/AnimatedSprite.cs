using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float animationSpeed = 0.25f;
    [SerializeField] private bool loop = true;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

    void Start()
    {
        InvokeRepeating(nameof(Advance), animationSpeed, animationSpeed);
    }

    private void Advance()
    {
        if (sprites.Length == 0) return;
        if (!this.spriteRenderer.enabled) return;

        currentFrame++;

        if (currentFrame >= sprites.Length)
        {
            if (loop)
            {
                currentFrame = 0;
            }
            else
            {
                CancelInvoke(nameof(Advance));
                return;
            }
        }

        spriteRenderer.sprite = sprites[currentFrame];
    }

    public void Restart()
    {
        this.currentFrame = -1;
        Advance();
    }
}
