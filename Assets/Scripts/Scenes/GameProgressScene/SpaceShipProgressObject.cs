using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipProgressObject : MonoBehaviour
{
    public GameObject hullObject;
    public GameObject foodObject;
    public GameObject fuelObject;

    public void init() {
        hullObject.SetActive(false);
        foodObject.SetActive(false);
        fuelObject.SetActive(false);
    }
}
