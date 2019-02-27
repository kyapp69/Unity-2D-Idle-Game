using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop {

    public Crop(string cropName, float initialValue) {
        this.cropName = cropName;
        this.initialValue = initialValue;
    }
    public string cropName { get; set; }
    public float initialValue { get; set; }
}
