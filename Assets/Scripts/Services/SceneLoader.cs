using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    private AsyncOperation loadOperation = null;

    public void loadScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    public void loadSceneAsyncAdditive(int sceneNumber, Action completion, CoroutineScope scope) {        
        if (loadOperation != null) { return; }
        scope.launch(loadAsyncScene(sceneNumber, completion));
    }

    public void unloadScene(int sceneNumber, Action completion, CoroutineScope scope) {
        scope.launch(unloadAsyncScene(sceneNumber, completion));
    }

    private IEnumerator unloadAsyncScene(int sceneNumber, Action completion) {
        try {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneNumber);
            while (!unloadOperation.isDone) {
                yield return null;
            }
        } finally {
            completion();
        }        
    }

    private IEnumerator loadAsyncScene(int sceneNumber, Action completion)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneNumber, LoadSceneMode.Additive);
        while (!loadOperation.isDone) {
            yield return null;
        }
        
        loadOperation = null;        
        completion();
    }
}
