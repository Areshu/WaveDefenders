using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton Setup
    private static GameManager instance = null;

    public static GameManager Instance
    {
        get { return instance; }
    }

    #endregion

    #region Variables

    private int score = 0;
    private int highScore = 0;
    private bool isHardMode = false;
    private float gameTime;
    private int enemyLife;
    //private bool isGameOver = false;

    public const int HARD_ENEMY_LIFE = 150;
    public const int MEDIUM_ENEMY_LIFE = 100;
    public const float HARD_GAME_TIME = 45f;
    public const float MEDIUM_GAME_TIME = 30f;

    #endregion

    #region Initialization
    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
    }

    void Init()
    {
        DontDestroyOnLoad(this);
    }

    //void CleanUp()
    //{

    //}

    #endregion

    #region GettersAndSetter

    public int Score { get => score; set => score = value; }
    public int HighScore { get => highScore; set => highScore = value; }
    public bool IsHardMode { get => isHardMode; set => isHardMode = value; }
    public float GameTime { get => gameTime; set => gameTime = value; }
    public int EnemyLife { get => enemyLife; set => enemyLife = value; }
    //public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    #endregion

    #region Methods

    public void UpdateHighScore()
    {
        if (score > highScore)
            highScore = score;
    }

    #endregion

    //private void OnDestroy()
    //{
    //    CleanUp();
    //}
}
