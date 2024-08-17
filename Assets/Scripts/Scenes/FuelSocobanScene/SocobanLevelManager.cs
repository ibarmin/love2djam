using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocobanLevelManager : MonoBehaviour
{
    public string fileName;
    public List<SocobanLevel> levels;
    
    public void Awake() {
        TextAsset text = (TextAsset)Resources.Load(fileName);
        if (!text) {
            Debug.Log("Levels file:" + fileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        string levelsText = text.text;
        string[] lines;

        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        levels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {
                Debug.Log("New level added");
                levels.Add(new SocobanLevel());
                continue;
            }
            levels[levels.Count - 1]._rows.Add(line);
            Debug.Log("Added Line: " + levels[levels.Count - 1]._rows.ToString());
        }
    }
}
