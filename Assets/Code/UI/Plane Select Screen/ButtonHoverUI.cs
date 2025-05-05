using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ButtonHoverUI : MonoBehaviour , IPointerEnterHandler
{
    public UnityEngine.UI.Image _baseImage;
    public Sprite _hoverImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SFXManager.Instance.PlaySound(SFXManager.Instance.buttonHover);

        if (_baseImage != null && _hoverImage != null)
        {
            _baseImage.sprite = _hoverImage;
        }
    }
}
