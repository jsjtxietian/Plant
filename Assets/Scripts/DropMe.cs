using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image receivingImage;
    private bool isInstruction;

    public void OnEnable()
    {
        Helper.SetTransparent(receivingImage, 0f);
        isInstruction = false;
    }

    public void OnDrop(PointerEventData data)
    {

        if (receivingImage == null)
            return;

        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null)
        {
            receivingImage.sprite = dropSprite;
            isInstruction = true;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null && !isInstruction)
            Helper.SetTransparent(receivingImage,1f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!isInstruction)
        {
            Helper.SetTransparent(receivingImage,0f);
        }
    }

    private Sprite GetDropSprite(PointerEventData data)
    {
        var originalObj = data.pointerDrag;
        if (originalObj == null)
            return null;

        var dragMe = originalObj.GetComponent<DragMe>();
        if (dragMe == null)
            return null;

        var srcImage = originalObj.GetComponent<Image>();
        if (srcImage == null)
            return null;

        return srcImage.sprite;
    }
}