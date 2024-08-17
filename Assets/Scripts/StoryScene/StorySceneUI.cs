using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneUI : MonoBehaviour
{
    private SceneLoader sceneLoader = new SceneLoader();

    public void onSkipStory() {
        sceneLoader.loadScene(SceneNumbers.GAME_PROGRESS_SCENE_ID);
    }
}
