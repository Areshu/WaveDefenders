using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ammoText;
    [SerializeField] private Text lifeText;

    [Header("Settings")]
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnTime = 2;
    [SerializeField] private Transform[] spawnPoints;

    private int enemiesInGame = 0;
    private float enemyTimeToSpawn = 0;

    private float time;
    private int score;

    private GameManager gameManger;

    private const int SCORE_PER_DEATH = 5;

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
            //GAME OVER
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

        //WIN!!!
        Debug.Log("WIN!");
    }

    public void UpdateAmmo(int ammo)
    {
        ammoText.text = ammo.ToString("00");
    }

    public void UpdateLife(int life)
    {
        lifeText.text = life.ToString();

        if(life <= 0)
        {
            // GAME OVER
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
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
}
