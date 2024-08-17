using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressScene : MonoBehaviour, CoroutineScope
{
    private SceneLoader sceneLoader = new SceneLoader();
    private SpaceShipProgress spaceShipProgress = new SpaceShipProgress();

    public Button shipBodyButton;
    public Button foodButton;
    public Button fuelButton;
    
    void Start()
    {
        bool armorReady = spaceShipProgress.getArmorCollected() >= spaceShipProgress.getArmorNeeded();
        bool foodReady = spaceShipProgress.getFoodWeight() > 0;

        fuelButton.interactable = armorReady && foodReady;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCollectingFood() {
        sceneLoader.loadScene(SceneNumbers.SCENE_2048_ID);
    }

    public void startCollectingFuel() {
        sceneLoader.loadScene(SceneNumbers.FUEL_SCENE_ID);
    }

    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }
}
