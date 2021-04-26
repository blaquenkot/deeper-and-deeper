using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text StartButtonText;
    public SoundButton SoundButton;
    private LanguageController LanguageController;
    private SceneManagerController SceneManagerController;

    void Awake()
    {
        SoundButton.AudioController = GetComponent<AudioController>();
        SceneManagerController = Object.FindObjectOfType<SceneManagerController>();
        SceneManagerController.currentSceneIndex = 1;
        LanguageController = LanguageController.Shared;
        this.reloadTexts();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManagerController.GoToNexScene();
        }
    }

    public void OnClickStart() 
    {
        SceneManagerController.GoToNexScene();
    }

    public void OnClickES()
    {
        LanguageController.CurrentLanguage = LanguageController.Language.ES;
        this.reloadTexts();
    }

    public void OnClickEN()
    {
        LanguageController.CurrentLanguage = LanguageController.Language.EN;
        this.reloadTexts();
    }

    private void reloadTexts()
    {
        StartButtonText.text = LanguageController.getStartButtonText();
    }
}