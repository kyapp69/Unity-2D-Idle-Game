using UnityEngine;

[ExecuteInEditMode]
public class UI : MonoBehaviour {

    public UIData skin;

    protected virtual void OnSkinUI() {

    }

    public virtual void Awake() {
        OnSkinUI();
    }

    // public virtual void Update() {
    //     if (Application.isEditor) {
    //         OnSkinUI();
    //     }
    // }

    // private void Update() {
    //     OnSkinUI();
    // }

    private void OnRenderObject() {
        OnSkinUI();
    }

}