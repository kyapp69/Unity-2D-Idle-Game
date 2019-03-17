using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop {

    public Crop(string cropName, float initialValue, float baseTime) {
        this.cropName = cropName;
        this.initialValue = initialValue;
        this.baseTime = baseTime;
    }
    public string cropName { get; set; }
    public float initialValue { get; set; }
    public float baseTime { get; set; }
}
