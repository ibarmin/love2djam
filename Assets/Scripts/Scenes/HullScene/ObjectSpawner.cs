using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float timerPeriod = 4.0f;
    public List<GameObject> crystals = new List<GameObject>();
    public List<GameObject> rocks = new List<GameObject>();

    private float currentTimeValue;
    private System.Random rand = new System.Random();

    public void Start() {
        currentTimeValue = timerPeriod;
    }

    public void Update() {
        currentTimeValue -= Time.deltaTime;
        if (currentTimeValue <= 0) {
            spawn();
            currentTimeValue = timerPeriod;
        }
    }

    private void spawn() {
        int currentRand = rand.Next(0, 2);
        if (currentRand > 0) {
            currentRand = rand.Next(0, 2);
            if (currentRand > 0) {
                currentRand = rand.Next(0, rocks.Count);                
                Instantiate(rocks[currentRand], transform.position, new Quaternion());
            } else {
                currentRand = rand.Next(0, crystals.Count);
                Instantiate(crystals[currentRand], transform.position, new Quaternion());
            }
        }
    }
}
