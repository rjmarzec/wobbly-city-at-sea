using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    private CanvasGroup cg;
    private float timer = 0;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void OnClick()
    {
        if(cg.alpha >= 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        cg.alpha = timer / 2;
    }
}
