using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.LuisPedroFonseca.ProCamera2D.TopDownShooter
{
	public class GameOver : MonoBehaviour
	{
		public Canvas GameOverScreen;

		private void Awake()
		{
			GameOverScreen.gameObject.SetActive(false);
		}

		public void ShowScreen()
		{
			GameOverScreen.gameObject.SetActive(true);
			Time.timeScale = 0f;
		}

		public void PlayAgain()
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}