using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelScene : MonoBehaviour, CoroutineScope
{
    public Camera mainCamera;
    public FuelSceneCharacterController characterController;
    public FuelStoreDoorType lastOpenedDoorType;
    public GameObject sceneVisual;


    private int timeLeft = FuelSceneConsts.TIME_FOR_FUEL_LEVEL;
    private FuelStoreDoor lastOpenedDoor;
    private SceneLoader sceneLoader = new SceneLoader();

    void Start()
    {
        DontDestroyOnLoad(this);

        characterController.onDoorOpened += onDoorOpen;      
    }

    public void destroyLastCollidedDoor() {
        sceneLoader.unloadScene(SceneNumbers.FUEL_SOCOBAN_SCENE_ID);

        sceneVisual.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        
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
        characterController.deactivate();
        mainCamera.gameObject.SetActive(false);
        sceneVisual.gameObject.SetActive(false);
    }

    public void launch(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
