using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    private Canvas _gameOverCanvas;

    private void Start()
    {
        _gameOverCanvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        PlayerController.GameOverEvent += GameOver;
    }

    private void OnDisable()
    {
        PlayerController.GameOverEvent -= GameOver;
    }

    private void GameOver()
    {
        StartCoroutine(GameOverTimer());
    }


    private IEnumerator GameOverTimer()
    {
        _gameOverCanvas.enabled = true;
        yield return new WaitForSeconds(4f);
        _gameOverCanvas.enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
