using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameSceneController sceneController;

    private GameManager gameManager;
    private int life;
    private EnemyAI ai;

    public const int DAMAGE = 10;

    // Start is called before the first frame update
    void Awake()
    {
        sceneController = GameSceneController.Instance;
        gameManager = GameManager.Instance;
        ai = GetComponent<EnemyAI>();
    }

    public void Init()
    {
        if (sceneController == null)
            sceneController = GameSceneController.Instance;

        if (gameManager == null)
            gameManager = GameManager.Instance;

        life = gameManager.EnemyLife;
        gameObject.SetActive(true);

        ai.InitAI();
    }

    public void SetDamage(int damage)
    {
        life -= damage;
        CheckLife();
    }

    private void CheckLife()
    {
        if (life <= 0)
            Dead();
    }

    private void Dead()
    {
        sceneController.DeadEnemy();

        gameObject.SetActive(false);
    }
}
