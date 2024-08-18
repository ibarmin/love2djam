using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocobanLevelBuilder : MonoBehaviour
{
    public int currentLevel;
    public List<SocobanLevelElement> lvlElements;
    private SocobanLevel level;
    private System.Random random = new System.Random();

    GameObject getPrefab(char c)
    {
        SocobanLevelElement elm = lvlElements.Find(le => le.elementCharacter == c.ToString());
        if (elm != null) {
            return elm.prefab;
        } else {
            return null;
        }
    }

    public void buildLevel(FuelStoreDoorType doorType)
    {
        switch (doorType) {
            case FuelStoreDoorType.normal: {
                List<SocobanLevel> arr = FindFirstObjectByType<SocobanLevelManager>().easyLevels;
                int rnd = random.Next(0, arr.Count);
                level = arr[rnd];
                break;
            }
            case FuelStoreDoorType.hard: {
                List<SocobanLevel> arr = FindFirstObjectByType<SocobanLevelManager>().hardLevels;
                int rnd = random.Next(0, arr.Count);
                level = arr[rnd];                
                break;
            }
        }

        Debug.Log("Tryig to build...");
        int startX = -level.Width / 2;
        int x = startX;

        int y = -level.Height / 2;
        Debug.Log("Rows are: " + level._rows.Count.ToString());

        foreach (var row in level._rows) {
            foreach (var ch in row) {
                Debug.Log(ch);
                GameObject prefab = getPrefab(ch);
                if (prefab) {
                    Debug.Log("Instantiating prefab...");
                    Debug.Log(prefab.name);
                    Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
                }
                x++;
            }
            y++;
            x = startX;
        }
    }
}
