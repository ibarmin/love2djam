using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressScene : MonoBehaviour, CoroutineScope
{
    private SceneLoader sceneLoader = new SceneLoader();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startCollectingFuel() {
        sceneLoader.loadScene(SceneNumbers.FUEL_SCENE_ID);
    }

    public void launch(IEnumerator routine) {
        StartCoroutine(routine);
    }
}
