using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI chancesText;
    public Button restartButton;

    //Health related variables
    public int noOfBombs;
    public int currentHealth;

    public Image[] bombs;
    
    private PlayerController playerController;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        noOfBombs = playerController.numOfBombs;
        updateScore(score);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.isGameOver)
        {
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            chancesText.gameObject.SetActive(false);
        }
    }

    public void updateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void updateHealth(int healthValue, bool isPowerUp)
    {
        //If player has missed an animal decrement the chances count
        if(!isPowerUp)
        {
            bombs[healthValue].enabled = false;
        } 
        else if(healthValue < noOfBombs)
        {
            bombs[healthValue].enabled = true;
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
