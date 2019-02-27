using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ButtonUI : UI {

    public enum ButtonType {

        Default,
        Confirm,
        Decline,
        Warning

    }

    Image image;
    Image icon;
    Button button;
    public ButtonType buttonType;

    protected override void OnSkinUI() {
        base.OnSkinUI();
        image = GetComponent<Image>();
        // icon = transform.Find("Icon").GetComponent<Image>();
        button = GetComponent<Button>();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = skin.buttonSprite;
        image.type = Image.Type.Sliced;
        button.spriteState = skin.buttonSpriteState;

        switch (buttonType) {
            case ButtonType.Default:
                image.color = skin.defaultColor;
                // icon.sprite = skin.defaultIcon;
                break;
            case ButtonType.Confirm:
                image.color = skin.confirmColor;
                // icon.sprite = skin.confirmIcon;
                break;
            case ButtonType.Decline:
                image.color = skin.declineColor;
                // icon.sprite = skin.declineIcon;
                break;
            case ButtonType.Warning:
                image.color = skin.warningColor;
                // icon.sprite = skin.warningIcon;
                break;
        }
    }

    // private void Awake() {
    //     OnSkinUI();
    // }

}