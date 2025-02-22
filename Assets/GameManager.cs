using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead

}


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public float PlayStartTime;


    public GameObject IntroUI;
    public GameObject DeadUI;

    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public TMP_Text txt_score;

    public int lives = 3;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }



    public Player Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroUI.SetActive(true);
        DeadUI.SetActive(false);
    }

    float CalculateScore()
    {
        return Time.time - PlayStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());

        int currentHighScore = PlayerPrefs.GetInt("highScore");

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    public float CalculateGameSpeed()
    {
        if(State!=GameState.Playing)
        {
            return 5f;
        }

        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f));

        return Mathf.Min(speed, 30f);
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }
    // Update is called once per frame
    void Update()
    {

            if (State == GameState.Playing)
            {
                txt_score.text = $"Score : { CalculateScore():F0}";
            }
            else if (State == GameState.Dead)
            {
            txt_score.text = $"High Score : {GetHighScore()}";
        }
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);

            PlayStartTime = Time.time;
        }

        if(State == GameState.Playing && lives == 0)
        {
            State = GameState.Dead;
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            Player.KillPlayer();
            SaveHighScore();
        }

        if(State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
