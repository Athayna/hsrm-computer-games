using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField] private Button continueButton = null;
    [SerializeField] private Button titleButton = null;
    [SerializeField] private Button controlsButton = null;
    [SerializeField] private Button exitButton = null;
    [SerializeField] private Button controlsBack = null;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject controlsMenu = null;
    [SerializeField] private ThirdPersonPlayerController controller = null;
    [SerializeField] private AudioManager audioManager = null;
    [SerializeField] private GameObject dialogueBox = null;
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        controller.transform.GetPositionAndRotation(out startPosition, out startRotation);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        continueButton.onClick.AddListener(ContinueGame);
        titleButton.onClick.AddListener(TitleCoroutine);
        exitButton.onClick.AddListener(ExitGame);
        controlsBack.onClick.AddListener(BackToPauseMenu);
        controlsButton.onClick.AddListener(ControlsMenu);

        // open first dialogue box at the start of the game
        Dialogue("Novice! Go find the 3 rune stones to activate the portal! Start your journey by going to the village!");
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Tab))
#else
        if (Input.GetKeyDown(KeyCode.Escape))
#endif

        {
            Time.timeScale = 0;
            controller.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            pauseMenu.SetActive(true);
        }

        if (!dialogueBox.activeSelf) {
            return;
        }

        // BUG: if button is set to E, the dialogue box will close immediately
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            dialogueBox.SetActive(false);
            controller.enabled = true;
        }
    }   

    void TitleCoroutine()
    {
        audioManager.PlayClickSound();
        StartCoroutine(BackToTitle());
    }

    public void CompleteGameCoroutine()
    {
        StartCoroutine(CompleteGame());
    }

    IEnumerator BackToTitle()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("TitleScreen");

        while (!asyncLoad.isDone) {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TitleScreen"));
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainGame");
    }

    IEnumerator CompleteGame()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("ResultScreen");

        while (!asyncLoad.isDone) {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("ResultScreen"));
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainGame");
    }

    void ContinueGame()
    {
        audioManager.PlayClickSound();
        Time.timeScale = 1;
        controller.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenu.SetActive(false);
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

    public void Dialogue(string dialogue)
    {
        dialogueBox.SetActive(true);
        controller.enabled = false;
        dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = dialogue;
    }

    public void ControlsMenu()
    {
        audioManager.PlayClickSound();
        controlsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        audioManager.PlayClickSound();
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void GameOver()
    {
        dialogueBox.SetActive(true);
        controller.enabled = false;
        controller.transform.SetPositionAndRotation(startPosition, startRotation);
        dialogueBox.GetComponentInChildren<TextMeshProUGUI>().text = "You died! Try again!";
    }
}
