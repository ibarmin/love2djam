using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HullScene : MonoBehaviour
{
    public HullSceneCharacterController characterController;

    public TMP_Text neededTextField;
    public TMP_Text weightTextField;

    private SceneLoader sceneLoader = new SceneLoader();

    private SpaceShipProgress spaceShipProgress = new SpaceShipProgress();

    public void Start() {
        characterController.onPartCollected += onPartCollected;

        int currentArmor = spaceShipProgress.getArmorCollected();
        int currentWeight = spaceShipProgress.getArmorWeight();
        int needed = spaceShipProgress.getArmorNeeded();

        neededTextField.text = "Нужно: " + needed + "/" + currentArmor;
        weightTextField.text = "Вес: " + currentWeight;
    }

    public void finishArmorCollection() {
        sceneLoader.loadScene(SceneNumbers.GAME_PROGRESS_SCENE_ID);
    }

    private void onPartCollected(HullSceneCollectible collectible) {
        int currentArmor = spaceShipProgress.getArmorCollected();
        int currentWeight = spaceShipProgress.getArmorWeight();
        int needed = spaceShipProgress.getArmorNeeded();

        currentArmor += collectible.armorValue;
        currentWeight += collectible.weight;

        spaceShipProgress.setArmourCollected(currentArmor);
        spaceShipProgress.setArmourWeight(currentWeight);

        neededTextField.text = "Нужно: " + needed + "/" + currentArmor;
        weightTextField.text = "Вес: " + currentWeight;

        if (currentArmor >= needed) {
            finishArmorCollection();
        }
    }
}
