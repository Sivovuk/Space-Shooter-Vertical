using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI gameoverText;

    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image LivesImage;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        scoreText.text = "Score : " + 0;
        gameoverText.gameObject.SetActive(false);

        if (gameManager == null) 
        {
            Debug.LogError("Game Manager is null");
        }
    }


    public void UpdateScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void UpdateLives(int currentLives) 
    {
        LivesImage.sprite = _livesSprites[currentLives];

        if (currentLives <= 0) 
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence() 
    {
        gameoverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
        gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine() 
    {
        while (true)
        {
            gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
	
}
