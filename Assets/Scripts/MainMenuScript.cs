using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button hardModeBtn;
    [SerializeField] private Button mediumModeBtn;

    [Header("Texts")]
    [SerializeField] private Text diffExplTxt;
    [SerializeField] private Text scoreTxt;

    private Color32 checkedBtnColor;
    private Color32 uncheckedBtnColor;

    private GameManager gameManager;

    private const string HARD_EXPLAIN = "Duracón la Partida: 45s\nVida de los Enemigos: 150";
    private const string MEDIUM_EXPLAIN = "Duracón la Partida: 30s\nVida de los Enemigos: 100";

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        SetDifficulty();

        if (gameManager.HighScore > 0)
            scoreTxt.text = "Score: " + gameManager.HighScore;
    }

    public void SetDifficultyBtn(bool isHard)
    {
        gameManager.IsHardMode = isHard;

        SetDifficulty();
    }

    public void StartGame()
    {
        gameManager.Score = 0;
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

}
