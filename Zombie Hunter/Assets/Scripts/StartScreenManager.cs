using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public Button startButton;
    public Button rulesButton;
    public Button backButton;

    public GameObject rulesScreen;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(startGame);
        rulesButton.onClick.AddListener(showRules);
        backButton.onClick.AddListener(goToTitleScreen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startGame()
    {
        SceneManager.LoadScene("My Game");
    }

    void showRules()
    {
        rulesScreen.SetActive(true);
        backButton.gameObject.SetActive(true);
        titleScreen.SetActive(false);
    }

    void goToTitleScreen()
    {
        rulesScreen.SetActive(false);
        backButton.gameObject.SetActive(false);
        titleScreen.SetActive(true);
    }
}
