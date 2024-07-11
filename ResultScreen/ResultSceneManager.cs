using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] private Button titleButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private AudioManager audioManager = null;

    void Start()
    {
        titleButton.onClick.AddListener(TitleCoroutine);
        exitButton.onClick.AddListener(ExitGame);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void TitleCoroutine()
    {
        audioManager.PlayClickSound();
        StartCoroutine(LoadTitle());
    }

    IEnumerator LoadTitle()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScreen");

        while (!asyncLoad.isDone) {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TitleScreen"));
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("ResultScreen");
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
