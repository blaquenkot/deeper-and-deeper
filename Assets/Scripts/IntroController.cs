using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Image image;
    private float MaxTimer = 1f;
    private SceneManagerController SceneManagerController;
    private float Timer = 0f;
    private bool TimerActive = true;

    void Awake()
    {
        SceneManagerController = Object.FindObjectOfType<SceneManagerController>();
        SceneManagerController.currentSceneIndex = 0;
    }

    void Start()
    {
        this.image.transform.DOPunchScale(new Vector3(1f, 1f, 0f) * 1.05f, 0.5f);
    }

    void Update()
    {
        if(TimerActive)
        {
            Timer += Time.deltaTime;

            if (Timer >= MaxTimer) 
            {
                TimerActive = false;
                SceneManagerController.GoToNexScene();
            }
        }
    }
}