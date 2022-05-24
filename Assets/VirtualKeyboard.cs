using System.Collections;
using UnityEngine;

public class VirtualKeyboard : MonoBehaviour {

    [SerializeField]
    private MyInputField _inputField;

    [SerializeField]
    float _pressedActionStartDelay = 0.5f;
    [SerializeField]
    AnimationCurve _pressedActionDelay;

    Event _pressed;
    Coroutine _holdCoroutine = null;

    #region Keyboard (Button.OnClick)

    public void CharacterKeyPressed(string c) => HandleKeyboardEvent(WrapIntoEvent(c));

    public void KeyLeft() => HandleKeyboardEvent(WrapIntoEvent(KeyCode.LeftArrow));

    public void KeyRight() => HandleKeyboardEvent(WrapIntoEvent(KeyCode.RightArrow));

    public void KeyDelete() => HandleKeyboardEvent(WrapIntoEvent(KeyCode.Backspace));

    #endregion

    #region Holding a key (Button.EventTrigger)

    public void VirtualKeyDown(string input)
    {
        _pressed = WrapIntoEvent(input);
        _holdCoroutine = StartCoroutine(PressKey());
    }

    public void VirtualKeyUp(string input)
    {
        _pressed = null;
        StopCoroutine(_holdCoroutine);
    }
    
    #endregion

    private void HandleKeyboardEvent(Event e)
    {
        _inputField.ProcessEvent(e);
        _inputField.Refresh();
    }

    private Event WrapIntoEvent(string input)
    {
        Event e = new Event();
        if (input?.Length == 1)
            e.character = input[0];
        if (System.Enum.TryParse(input, out KeyCode keyCode))
            e.keyCode = keyCode;

        //if (_isShiftPressed)
        //    e.modifiers |= EventModifiers.Shift;

        return e;
    }
    private Event WrapIntoEvent(KeyCode keyCode) => WrapIntoEvent(keyCode.ToString());//new Event() { keyCode = keyCode };
    
    private void Start()
    {
        if (!_inputField.isFocused)
            _inputField.ActivateInputField();
    }

    IEnumerator PressKey()
    {
        yield return new WaitForSecondsRealtime(_pressedActionStartDelay);

        float startTime = Time.time;
        while (_pressed != null)
        {
            HandleKeyboardEvent(_pressed);

            yield return new WaitForSecondsRealtime(_pressedActionDelay.Evaluate(Time.time - startTime));
        }
        _holdCoroutine = null;
    }
}
