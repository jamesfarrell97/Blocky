using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private const int MENU_INDEX = 0;
    private const int GAME_INDEX = 1;

    [SerializeField] GameObject audioManager;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        Instantiate(audioManager);
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GAME_INDEX);
        MenuManager.Instance.OpenMenu("Loading");
        AudioManager.Instance.StopAll();
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level.Equals(GAME_INDEX)) MenuManager.Instance.OpenMenu("HUD");
        else if (level.Equals(MENU_INDEX)) MenuManager.Instance.OpenMenu("Main");
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(MENU_INDEX);
        MenuManager.Instance.OpenMenu("Loading");

        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("Theme");
    }

    public void PauseGame()
    {
        MenuManager.Instance.OpenMenu("Pause");
    }

    public void ResumeGame()
    {
        MenuManager.Instance.OpenMenu("HUD");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        AudioManager.Instance.StopAll();
        AudioManager.Instance.Play("Theme");

        MenuManager.Instance.OpenMenu("End");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(MENU_INDEX);
        MenuManager.Instance.OpenMenu("Loading");
    }
}