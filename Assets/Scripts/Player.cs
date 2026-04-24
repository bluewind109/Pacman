using UnityEngine;

public class Player : MonoBehaviour
{
	public void ResetState()
	{
		this.gameObject.SetActive(true);
	}

	public void Deactivate()
	{
		this.gameObject.SetActive(false);
	}
}
