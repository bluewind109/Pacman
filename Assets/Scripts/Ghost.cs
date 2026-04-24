using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float reviveDuration = 5f;
    [SerializeField] private int points = 200;
    public int Points { get { return points; } }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    
	public void Die()
	{
		Deactivate();
        Invoke(nameof(ResetState), reviveDuration);
	}
}
