using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button controlsButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private Button controlsBackButton = null;

    [SerializeField] private GameObject controlsPanel = null;
    [SerializeField] private AudioManager audioManager = null;

    void Start()
    {        
        startButton.onClick.AddListener(GameCoroutine);
        controlsButton.onClick.AddListener(ToggleControls);
        exitButton.onClick.AddListener(ExitGame);
        controlsBackButton.onClick.AddListener(ToggleControls);
    }

    void GameCoroutine()
    {
        audioManager.PlayClickSound();
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");

        while (!asyncLoad.isDone) {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainGame"));
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("TitleScreen");
    }

    void ToggleControls()
    {
        audioManager.PlayClickSound();
        controlsPanel.SetActive(!controlsPanel.activeSelf);
    }

    void ExitGame()
    {
        audioManager.PlayClickSound();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
