using UnityEngine;
using System.Collections;
using System;

public class DestroyableCanvasController : MonoBehaviour
{
    public bool shouldDestroyWhenPlayerDied = true;
    public event Action onDestroy;

    private GameController gameController;

    void Awake()
    {
        this.gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (this.shouldDestroyWhenPlayerDied && !this.gameController.isAlive())
        {
            this.removeCanvas(0f);
        }
    }

    internal void removeCanvas(float time)
    {
        StartCoroutine(this.destroyCanvas(time));
    }

    private IEnumerator destroyCanvas(float time)
    {
        yield return new WaitForSeconds(time);
        if(this.onDestroy != null)
        {
            this.onDestroy();
        }
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
