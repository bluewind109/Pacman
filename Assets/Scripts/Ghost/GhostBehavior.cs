using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{   
    public Ghost ghost { get; private set; }

    public float Duration;

    void Awake()
    {
        ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(Duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke(nameof(Disable));
    }
}
