using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressScene : MonoBehaviour, CoroutineScope
{
    private SceneLoader sceneLoader = new SceneLoader();
    private SpaceShipProgress spaceShipProgress = new SpaceShipProgress();

    public Button shipBodyButton;
    public Button foodButton;
    public Button fuelButton;

    public SpaceShipProgressObject spaceShipProgressObject;

    public TMP_Text neededArmorTextField;
    public TMP_Text weightArmorTextField;

    public TMP_Text neededFoodTextField;
    public TMP_Text weightFoodTextField;
    public TMP_Text fuelTextField;
    
    public void Start() {
        spaceShipProgressObject.init();

        int currentArmor = spaceShipProgress.getArmorCollected();
        int currentArmorWeight = spaceShipProgress.getArmorWeight();
        int neededArmor = spaceShipProgress.getArmorNeeded();

        int currentFood = spaceShipProgress.getFoodCollected();
        int currentFoodWeight = spaceShipProgress.getFoodWeight();
        int neededFood = spaceShipProgress.getFoodNeeded();

        int fuelCollected = spaceShipProgress.getFuelCollected();

        neededArmorTextField.text = "Корпус: " + neededArmor + "/" + currentArmor;
        weightArmorTextField.text = "Вес корпуса: " + currentArmorWeight;

        neededFoodTextField.text = " Еда: " + neededFood + "/" + currentFood;
        weightFoodTextField.text = "Вес еды: " + currentFoodWeight;

        fuelTextField.text = "Топливо: " + fuelCollected;

        bool armorReady = currentArmor >= neededArmor;
        bool foodReady = currentFood >= currentFoodWeight;

        if (armorReady) {
            neededArmorTextField.color = Color.green;
            weightArmorTextField.color = Color.green;

            spaceShipProgressObject.hullObject.SetActive(true);
        } else {
            neededArmorTextField.color = Color.red;
            weightArmorTextField.color = Color.red;
        }

        if (foodReady) {
            neededFoodTextField.color = Color.green;
            weightFoodTextField.color = Color.green;

            spaceShipProgressObject.foodObject.SetActive(true);
        } else {
            neededFoodTextField.color = Color.red;
            weightFoodTextField.color = Color.red;
        }

        if (fuelCollected > 0) {
            spaceShipProgressObject.fuelObject.SetActive(true);
        }

        int totalWeightCoff = (currentArmorWeight + currentFoodWeight) / 50;

        spaceShipProgressObject.transform.localScale = new Vector3(
            2 + totalWeightCoff,
            2 + totalWeightCoff,
            1
        );

        fuelButton.interactable = armorReady && foodReady;
        shipBodyButton.interactable = !armorReady;
        foodButton.interactable = !foodReady;
    }

    public void startCollectingArmor() {
        sceneLoader.loadScene(SceneNumbers.HULL_SCENE_ID);
    }

    public void startCollectingFood() {
        sceneLoader.loadScene(SceneNumbers.SCENE_2048_ID);
    }

    public void startCollectingFuel() {
        sceneLoader.loadScene(SceneNumbers.FUEL_SCENE_ID);
    }

    public void quitGame() {
        sceneLoader.loadScene(SceneNumbers.MAIN_SCENE_ID);
    }

    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }
}
