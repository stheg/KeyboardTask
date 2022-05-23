using UnityEngine;

public class VirtualKeyboard : MonoBehaviour {

    [SerializeField]
    private MyInputField _inputField;

    public void KeyPress(string c)
    {
        _inputField.ProcessEvent(new Event() { character = c[0] });
        _inputField.Refresh();
    }

    public void KeyLeft() => KeyCodePressed(KeyCode.LeftArrow);

    public void KeyRight() => KeyCodePressed(KeyCode.RightArrow);

    public void KeyDelete() => KeyCodePressed(KeyCode.Backspace);

    private void KeyCodePressed(KeyCode keyCode)
    {
        _inputField.ProcessEvent(new Event() { keyCode = keyCode });
        _inputField.Refresh();
    }

    private void Start()
    {
        if (!_inputField.isFocused)
            _inputField.ActivateInputField();
    }
}
