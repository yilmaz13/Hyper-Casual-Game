using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            GameManager.Instance.SetState(typeof(MenuGameState));
        }
    }

    public GameObject Player;
    public Camera MainCamera;
    public Transform EnemyGroup;
    public List<GameObject> enemies;

    public void InitLevel()
    {
        GameManager.Instance.IsGameActive = false;
        MainCamera = Camera.main;
        Player = GameObject.FindWithTag("Player");
        MainCamera.gameObject.GetComponent<CameraMovement>().SetTarget(Player.transform);

        foreach (Transform enemy in EnemyGroup)
        {
            enemies.Add(enemy.gameObject);
            enemy.GetComponent<EnemyController>().SetTarget(Player.transform);
        }
    }

    public void GameOver()
    {
        GameManager.Instance.SetState(typeof(GameOverGameState));
        GameManager.Instance.IsGameActive = false;
    }

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("Level") + 1 >= 5)
            PlayerPrefs.SetInt("Level", 1);
        else
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        GameManager.Instance.SetScene(PlayerPrefs.GetInt("Level"));
        GameManager.Instance.IsGameActive = false;
    }
}