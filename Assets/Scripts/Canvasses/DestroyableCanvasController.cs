using UnityEngine;
using System.Collections;
using System;
public class DestroyableCanvasController : MonoBehaviour
{
    public event Action onDestroy;

    internal void removeCanvas(float time)
    {
        StartCoroutine(destroyCanvas(time));
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
