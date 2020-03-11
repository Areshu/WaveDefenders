using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] private Text diffExplTxt;
    [SerializeField] private Text scoreTxt;

    [Header("Settings")]
    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioClip clickSound;

    private GameManager gameManager;

    #region Texts

    private const string HARD_EXPLAIN = "Duración la Partida: 45s\nVida de los Enemigos: 150";
    private const string MEDIUM_EXPLAIN = "Duración la Partida: 30s\nVida de los Enemigos: 100";
    private const string HIGH_SCORE_TEXT = "Puntuación Máxima: ";
    private const string GAME_SCENE = "GameScene";

    #endregion

    void Start()
    {
        gameManager = GameManager.Instance;

        SetDifficulty();

        gameManager.HighScore = PlayerPrefs.GetInt(GameManager.SAVE_SCORE);

        if (gameManager.HighScore > 0)
            scoreTxt.text = HIGH_SCORE_TEXT + gameManager.HighScore;

        aSource.clip = clickSound;
    }

    public void SetDifficultyBtn(bool isHard)
    {
        PlayClickSound();

        gameManager.IsHardMode = isHard;

        SetDifficulty();
    }

    public void StartGame()
    {
        PlayClickSound();

        gameManager.Score = 0;

        SceneManager.LoadScene(GAME_SCENE);
    }

    private void SetDifficulty()
    {
        if (gameManager.IsHardMode)
        {
            diffExplTxt.text = HARD_EXPLAIN;

            gameManager.IsHardMode = true;
            gameManager.GameTime = GameManager.HARD_GAME_TIME;
            gameManager.EnemyLife = GameManager.HARD_ENEMY_LIFE;
        }
        else
        {
            diffExplTxt.text = MEDIUM_EXPLAIN;

            gameManager.IsHardMode = false;
            gameManager.GameTime = GameManager.MEDIUM_GAME_TIME;
            gameManager.EnemyLife = GameManager.MEDIUM_ENEMY_LIFE;
        }
    }

    private void PlayClickSound()
    {
        aSource.Play();
    }

}
