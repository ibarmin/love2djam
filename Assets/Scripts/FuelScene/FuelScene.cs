using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelScene : MonoBehaviour, CoroutineScope
{
    public Camera mainCamera;
    public FuelSceneCharacterController characterController;
    public FuelStoreDoorType lastOpenedDoorType;
    public GameObject sceneVisual;
    public TMP_Text timerTextField;
    public TMP_Text collectedFuelTextField;
    public Button exitButton;


    private float timeLeft = FuelSceneConsts.TIME_FOR_FUEL_LEVEL;
    private int totalFuel = 0;
    private FuelStoreDoor lastOpenedDoor;
    private SceneLoader sceneLoader = new SceneLoader();

    public void Start()
    {
        characterController.onDoorOpened += onDoorOpen;      
    }

    public void Update() {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0.0) {
            finishFuelCollection();
        } else {
            float timeSpanConversiondMinutes = TimeSpan.FromSeconds(timeLeft).Minutes;
            float timeSpanConversionSeconds = TimeSpan.FromSeconds(timeLeft).Seconds;

            timerTextField.text = "Осталось " + timeSpanConversiondMinutes + ":" + timeSpanConversionSeconds;
        }

        collectedFuelTextField.text = "Собрано: " + totalFuel;
    }

    public void addCollectedFuel(int collectedValue) {
        totalFuel += collectedValue;
    }

    public void destroyLastCollidedDoor() {
        sceneLoader.unloadScene(SceneNumbers.FUEL_SOCOBAN_SCENE_ID, onRegularSocobanUnload, this);
    }

    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }

    public void finishFuelCollection() {
        if (!sceneVisual.gameObject.activeSelf) {
            sceneLoader.unloadScene(SceneNumbers.FUEL_SOCOBAN_SCENE_ID, onFinalSocobanUnload, this);        
        } else {
            onFinalSocobanUnload();
        }
    }

    private void onFinalSocobanUnload() {
        sceneLoader.loadScene(SceneNumbers.GAME_PROGRESS_SCENE_ID);
    }

    private void onRegularSocobanUnload() {
        sceneVisual.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
        
        lastOpenedDoor.destroyDoor();
        lastOpenedDoor = null;

        characterController.activate();
    }

    private void onDoorOpen(FuelStoreDoor door) {
        lastOpenedDoor = door;
        lastOpenedDoorType = door.doorType;

        sceneLoader.loadSceneAsyncAdditive(SceneNumbers.FUEL_SOCOBAN_SCENE_ID, onSocobanLoaded, this);
    }

    private void onSocobanLoaded() {
        exitButton.gameObject.SetActive(false);    
        characterController.deactivate();
        mainCamera.gameObject.SetActive(false);
        sceneVisual.gameObject.SetActive(false);
    }
}
