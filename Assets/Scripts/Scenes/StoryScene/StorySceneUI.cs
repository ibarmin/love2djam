using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySceneUI : MonoBehaviour
{
    private SceneLoader sceneLoader = new SceneLoader();
    private SpaceShipProgress spaceShipProgress = new SpaceShipProgress();

    public void onSkipStory() {
        spaceShipProgress.initGameValues(ShipProgressConstant.NEEDED_ARMOR, ShipProgressConstant.NEEDED_FOOD);
        sceneLoader.loadScene(SceneNumbers.GAME_PROGRESS_SCENE_ID);
    }
}
