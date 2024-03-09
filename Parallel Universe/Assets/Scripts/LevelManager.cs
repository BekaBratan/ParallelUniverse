using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int neededHead = 1;
    int headCount = 0;
    int playerCount = 0;
    public float time = 3;
    float timer;
    float timer2;
    int clone = 0;
    bool timerOn;

    public TMP_Text headText;
    public TMP_Text lastText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Head", 0);
        PlayerPrefs.SetInt("Players", 2);
        headText.SetText("HEADS: " + headCount + "/" + neededHead);
        lastText.SetText("Fight");
        timer = time;
        timer2 = 1;
        PlayerPrefs.SetInt("Clone",3);
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerOn) { 
            timer2 -= Time.deltaTime;
            if (timer2 <= 0) {
                lastText.SetText("");
                timer2 = 2;
                timerOn = false;
            }
        }
        
        if (timer == time)
        {
            clone = PlayerPrefs.GetInt("Clone", 0);
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                neededHead = clone;
            }
            headCount = PlayerPrefs.GetInt("Head", 0);
        }
        headText.SetText("HEADS: " + headCount + "/" + neededHead);
        if (headCount >= neededHead) 
        {
            headText.SetText("");
            lastText.SetText("Next level on auto load");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                LoadNextScene();
            }
        }

        playerCount = PlayerPrefs.GetInt("Players", 2);
        if (playerCount <= 0)
        {
            headText.SetText("");
            lastText.SetText("Game Over \n Main menu on auto load");
            // audioSource.Play();
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                MainScene();
            }
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);

    }
}
