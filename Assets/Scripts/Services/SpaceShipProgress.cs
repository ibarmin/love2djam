using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipProgress
{
    private static string armorNeededKeyName = "armor_needed";
    private static string armorCollectedKeyName = "armor_collected";
    private static string armorWeightKeyName = "armor_weight";

    private static string foodNeededKeyName = "armor_needed";
    private static string foodWeightKeyName = "armor_weight";

    public void initGameValues(int neededArmor, int neededFood) {
        setArmourNeeded(neededArmor);
        setFoodNeeded(neededFood);

        setFoodWeight(0);
        setArmourCollected(0);
        setArmourWeight(0);
    }
    
    public void setArmourNeeded(int armorValue) {
        PlayerPrefs.SetInt(armorNeededKeyName, armorValue);
    }

    public void setFoodNeeded(int foodValue) {
        PlayerPrefs.SetInt(foodNeededKeyName, foodValue);
    }

    public void setArmourCollected(int armorValue) {
        PlayerPrefs.SetInt(armorCollectedKeyName, armorValue);
    }

    public void setArmourWeight(int armorWeight) {
        PlayerPrefs.SetInt(armorWeightKeyName, armorWeight);
    }

    public void setFoodWeight(int foodWeight) {
        PlayerPrefs.SetInt(foodWeightKeyName, foodWeight);
    }

    public int getFoodWeight() {
        return PlayerPrefs.GetInt(foodWeightKeyName);
    }

    public int getArmorNeeded() {
        return PlayerPrefs.GetInt(armorNeededKeyName);
    }

    public int getArmorCollected() {
        return PlayerPrefs.GetInt(armorCollectedKeyName);
    }

    public int getArmorWeight() {
        return PlayerPrefs.GetInt(armorWeightKeyName);
    }

    public void resetProgress() {
        PlayerPrefs.DeleteKey(armorNeededKeyName);
        PlayerPrefs.DeleteKey(armorCollectedKeyName);
        PlayerPrefs.DeleteKey(armorWeightKeyName);

        PlayerPrefs.DeleteKey(foodNeededKeyName);
        PlayerPrefs.DeleteKey(foodWeightKeyName);
    }
}
