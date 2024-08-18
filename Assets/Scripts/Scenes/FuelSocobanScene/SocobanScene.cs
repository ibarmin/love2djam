using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocobanScene : MonoBehaviour
{
    public SocobanLevelBuilder levelBuilder;    

    private FuelScene fuelScene;
    private SocobanPlayer player;
    private bool readyForInput;

    public void Start()
    {
        fuelScene = GameObject.FindFirstObjectByType<FuelScene>();

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneNumbers.FUEL_SOCOBAN_SCENE_ID));
        
        levelBuilder.buildLevel(fuelScene.lastOpenedDoorType);
        player = FindObjectOfType<SocobanPlayer>();

        SocobanBox[] boxes = FindObjectsOfType<SocobanBox>();
        foreach (var box in boxes) {
            box.onBoxArrived += onBoxArrived;
        }
    }

    public void Update() {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput.Normalize();

        if (player) {
            if (IsLevelComplete()) {
                onExitDoor();
            }
            
            
            if (movementInput.sqrMagnitude > 0.5) {
                if (readyForInput) {
                    readyForInput = false;
                    player.Move(movementInput);                    
                }
            } else {
                readyForInput = true;
            }
        }
    }

    public void onExitDoor() {       
        fuelScene.destroyLastCollidedDoor();
    }

    private bool IsLevelComplete() {
        SocobanBox[] boxes = FindObjectsOfType<SocobanBox>();
        foreach (var box in boxes) {
            if (!box.arrived) {
                return false;
            }
        }
        return true;
    }

    private void onBoxArrived(int collectedValue) {
        fuelScene.addCollectedFuel(collectedValue);
    }
}
