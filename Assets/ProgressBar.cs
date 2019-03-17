using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    // Unity UI References
    public Slider slider;
    // public Text displayText;
    public float duration = 3;
    public float maxValue = 1f;


    public bool autoRepeat = false;
    public bool running = false;
    // Event to invoke when the progress bar fills up
    private UnityEvent onProgressCompleteEvent;

    // Create a property to handle the slider's value
    private float currentValue = 0f;

    public float CurrentValue {
        get {
            return currentValue;
        }
        set {
            if (value > maxValue) {
                onProgressCompleteEvent.Invoke();
                if (!autoRepeat) {
                    running = false;
                    currentValue = 0;
                } else {
                    currentValue = value % maxValue;
                }
            } else {
                currentValue = value;

            }
            slider.value = currentValue;
        }
    }

    public void Execute(float duration, UnityAction onProgressCompleteListner) {
        this.duration = duration;
        if (onProgressCompleteListner != null) {
            onProgressCompleteEvent = new UnityEvent();
            onProgressCompleteEvent.AddListener(onProgressCompleteListner);
        }
        running = true;
    }

    // Update is called once per frame
    void Update () {
        if (!running) {
            return;
        }
        CurrentValue += Time.deltaTime / duration;
    }
}
