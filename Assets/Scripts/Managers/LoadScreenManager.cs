using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreenManager : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Text _progressValue;
    [SerializeField] private string _targetScene = "Level"; // podria ser un parametro

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(_targetScene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            Debug.Log(async.progress);
            _progressBar.fillAmount = async.progress;
            _progressValue.text = $"LOADING - {Math.Round(async.progress * 100, 2)}%";
            yield return  new WaitForSeconds(0.1f);
        }
    }
}
