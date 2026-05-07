using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Could not find a Settings Panel");
        }

    }

    public void OnBackClicked()
    {
        //hide settings panel
        settingsPanel.SetActive(false);
    }
    public void OnStartClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("2DPlat");
    }

    public void OnSettingsClicked()
    {
        bool isActive = settingsPanel.activeSelf;
        settingsPanel.SetActive(!isActive);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
