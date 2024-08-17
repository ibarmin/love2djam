using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    private SceneLoader sceneLoader = new SceneLoader();


    public void onExitGame() {
        Application.Quit();
    }

    public void onStartGame() {
        sceneLoader.loadScene(SceneNumbers.STORY_SCENE_ID);
    }
}
