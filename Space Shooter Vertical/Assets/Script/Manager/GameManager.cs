using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
	private bool isGameOver = false;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && isGameOver) 
		{
			SceneManager.LoadScene(0);
		}
	}

	public void GameOver() 
	{
		isGameOver = true;
	}
   
	
}
