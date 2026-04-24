using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Ghost[] ghosts;
	[SerializeField] private Player pacman;
	[SerializeField] private Transform pellets;
	[SerializeField] private float resetDelay = 3f;

	public int Score { get; private set; }
	public int Lives { get; private set; }
	
	private int pelletCounts = 0;

	private bool isGameOver => Lives <= 0;
	private bool isAllPelletsEaten => pelletCounts <= 0;

	void Start()
	{
		NewGame();
	}

	void Update()
	{
		if (Input.anyKeyDown && isGameOver)
		{
			NewGame();
		}
	}

	private void NewGame()
	{
		SetScore(0);
		SetLives(3);
		pelletCounts = pellets.childCount;
		NewRound();
	}

	private void NewRound()
	{
		foreach (Transform pellet in pellets)
		{
			pellet.gameObject.SetActive(true);
		}

		ResetState();
	}

	private void ResetState()
	{
		foreach (Ghost ghost in ghosts)
		{
			ghost.ResetState();
		}

		pacman.ResetState();
	}

	private void GameOver()
	{
		foreach (Ghost ghost in ghosts)
		{
			ghost.Deactivate();
		}

		pacman.Deactivate();
	}

	private void SetScore(int score)
	{
		Score = score;
	}

	private void SetLives(int lives)
	{
		Lives = lives;
	}

	public void GhostEaten(Ghost ghost)
	{
		SetScore(Score + ghost.Points);
		ghost.Die();
	}

	public void PacmanEaten()
	{
		pacman.Deactivate();
		SetLives(Lives - 1);

		if (Lives > 0)
		{
			Invoke(nameof(ResetState), resetDelay); // Respawn
		}
		else
		{
			GameOver();
		}
	}

	public void PelletEaten(Pellet pellet)
	{
		pellet.gameObject.SetActive(false);
		SetScore(Score + pellet.Points);

		if (isAllPelletsEaten)
		{
			pacman.Deactivate();
			Invoke(nameof(NewRound), resetDelay); // You win, start a new round
		}
	}

	public void PowerPelletEaten(PowerPellet pellet)
	{
		PelletEaten(pellet);
		pelletCounts--;
		
		// TODO change ghost state to vulnerable for a duration
	}
}
