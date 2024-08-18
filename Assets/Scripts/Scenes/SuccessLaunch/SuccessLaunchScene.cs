using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessLaunchScene : MonoBehaviour
{
    private SceneLoader sceneLoader = new SceneLoader();

    public void toMainMenu() {
        sceneLoader.loadScene(SceneNumbers.MAIN_SCENE_ID);
    }
}
