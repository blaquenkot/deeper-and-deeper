using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class DestroyableCanvasController : MonoBehaviour
{
    public bool shouldDestroyWhenPlayerDied = true;
    public event Action onDestroy;

    private CanvasGroup mainCanvasGroup;
    private GameController gameController;
    private bool canBeDestroyed = false;

    void Awake()
    {
        this.gameController = FindObjectOfType<GameController>();
        this.mainCanvasGroup = GetComponent<CanvasGroup>();
        this.mainCanvasGroup.alpha = 0.05f;
    }

    public virtual void Start()
    {
        this.mainCanvasGroup.DOFade(1f, 0.25f);
    }

    void Update()
    {
        if ((this.shouldDestroyWhenPlayerDied && !this.gameController.isAlive()) || (this.canBeDestroyed && Input.GetMouseButtonDown(0)))
        {
            this.removeCanvas(0f);
        }
    }

    internal void removeCanvas(float time)
    {
        this.canBeDestroyed = true;
        StartCoroutine(this.destroyCanvas(time));
    }

    private IEnumerator destroyCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        this.mainCanvasGroup
            .DOFade(0.05f, 0.25f)
            .OnComplete(() => {
                if(this.onDestroy != null)
                {
                    this.onDestroy();
                }
                Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
            });
    }
}
