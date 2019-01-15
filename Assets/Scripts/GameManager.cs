using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LevelManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TransitionFader transitionFader;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // initialize references
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void LevelWin()
    {
        StopPlayerMovement();
        StopEnemiesMovement();
        StartCoroutine(EndCorutine());
    }

    public void LevelLose()
    {
        StopEnemiesMovement();
        LoseScreen.Open();
    }

    private IEnumerator EndCorutine()
    {
        if (transitionFader != null)
        {
            TransitionFader.PlayTransition(transitionFader);
            yield return new WaitForSeconds(transitionFader.FadeOnDuration + transitionFader.Delay);
        }
        else
        {
            yield return null;
        }
        WinScreen.Open();
    }

    private void StopPlayerMovement()
    {
        Player player = FindObjectOfType<Player>();
        player.GetComponent<Animator>().SetTrigger("Win");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.IsActive = false;
    }

    private void StopEnemiesMovement()
    {
        EnemyMovement[] enenemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enenemies)
        {
            enemy.GetComponent<Animator>().SetTrigger("End");
            enemy.Stop();
        }
    }
}