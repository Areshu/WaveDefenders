using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text lifeText;
    [SerializeField] private GameObject gameOverPopup;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text gameOverScoreText;

    [Header("Settings")]
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnTime = 2;
    [SerializeField] private Transform[] spawnPoints;

    private int enemiesInGame = 0;
    private float enemyTimeToSpawn = 0;

    private float time;
    private int score;

    private bool isGameOver = false;

    private GameManager gameManger;

    private const int SCORE_PER_DEATH = 5;
    private const string HOME_SCENE = "MainMenu";
    private const string YOU_LOOSE = "Has Muerto...";
    private const string YOU_WIN = "Has Sobrevivido uná Noche Más...";

    #region Singleton Setup
    private static GameSceneController instance = null;

    public static GameSceneController Instance
    {
        get { return instance; }
    }
    #endregion

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManger = GameManager.Instance;

        time = gameManger.GameTime;
        score = 0;

        enemyTimeToSpawn = Time.time + spawnTime;

        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            if (enemiesInGame < maxEnemies && Time.time >= enemyTimeToSpawn)
            {
                //Enemy spawned per spawn Time
                enemyTimeToSpawn = Time.time + spawnTime;
                SpawnEnemy();
            }
        }
        else
        {
            GameOver(true);
        }
    }

    IEnumerator Timer()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1f);
            --time;
            timeText.text = time.ToString("00");
        }

        Debug.Log("WIN!");
    }

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = "Munición: " + ammo.ToString("00");
    }

    public void Reloading()
    {
        ammoText.text = "RECARGANDO...";
    }

    public void UpdateLife(int life)
    {
        lifeText.text = "Vida: " + life.ToString();

        if (life <= 0)
        {
            GameOver(false);
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Puntuación: " + score.ToString();
    }

    public void DeadEnemy()
    {
        enemiesInGame--;
        score += SCORE_PER_DEATH;
        UpdateScore();
    }

    private int GetRandomSpawnPoint()
    {
        return Random.Range(0, spawnPoints.Length);
    }

    private void SpawnEnemy()
    {
        GameObject enemy;
        int point = GetRandomSpawnPoint();

        enemy = ObjectPooler.Instance.GetPooledObject("Enemy");
        enemy.transform.position = spawnPoints[point].position;
        enemy.transform.rotation = spawnPoints[point].rotation;
        enemy.SetActive(true);
        enemy.GetComponent<Enemy>().Init();
    }


    private void GameOver(bool win)
    {
        isGameOver = true;

        if (win)
        {
            gameOverText.text = YOU_WIN;
            gameOverScoreText.text = "Puntuación: " + score.ToString();
            CheckHighScore();
        }
        else
        {
            gameOverText.text = YOU_LOOSE;
        }

        gameOverPopup.SetActive(true);
    }


    private void CheckHighScore()
    {
        if (gameManger.HighScore < score)
            gameManger.HighScore = score;
    }

    public void GoToHome()
    {
        SceneManager.LoadScene(HOME_SCENE);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    
}
