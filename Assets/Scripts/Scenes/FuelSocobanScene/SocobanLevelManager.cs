using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocobanLevelManager : MonoBehaviour
{
    public string easyFileName;
    public string hardFileName;
    public List<SocobanLevel> easyLevels;
    public List<SocobanLevel> hardLevels;
    
    public void Awake() {
        TextAsset text = (TextAsset)Resources.Load(easyFileName);
        if (!text) {
            Debug.Log("Levels file:" + easyFileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        string levelsText = text.text;
        string[] lines;

        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        easyLevels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {
                Debug.Log("New level added");
                easyLevels.Add(new SocobanLevel());
                continue;
            }
            easyLevels[easyLevels.Count - 1]._rows.Add(line);
            Debug.Log("Added Line: " + easyLevels[easyLevels.Count - 1]._rows.ToString());
        }





        text = (TextAsset)Resources.Load(hardFileName);
        if (!text) {
            Debug.Log("Levels file:" + hardFileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        levelsText = text.text;
        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        hardLevels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {
                Debug.Log("New level added");
                hardLevels.Add(new SocobanLevel());
                continue;
            }
            hardLevels[hardLevels.Count - 1]._rows.Add(line);
            Debug.Log("Added Line: " + hardLevels[hardLevels.Count - 1]._rows.ToString());
        }
    }
}
