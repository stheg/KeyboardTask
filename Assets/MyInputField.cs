using UnityEngine.EventSystems;

public class MyInputField : InputFieldOriginal
{
    public override void OnSelect(BaseEventData eventData)
    {
    }

    public override void OnDeselect(BaseEventData eventData)
    {
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
    }

    public void Refresh()
    {
        Select();
        UpdateLabel();
    }
}