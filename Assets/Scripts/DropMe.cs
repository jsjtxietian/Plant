using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image receivingImage;
    private bool alreadyHaveIns;
    public GameObject Controller ;

    public void OnEnable()
    {
        Controller = GameObject.Find("Controller");
        Helper.SetTransparent(receivingImage, 0f);
        alreadyHaveIns = false;
    }

    public void OnDisable()
    {
        Helper.SetTransparent(receivingImage, 0f);
        gameObject.GetComponent<Image>().sprite = Helper.CommandSpriteDictionary[Command.None];
        alreadyHaveIns = false;
    }

    public void OnDrop(PointerEventData data)
    {
        if (receivingImage == null)
            return;

        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null)
        {

            Coordinate self = gameObject.GetComponent<CommandSpriteController>().GetCoordinate();
            Controller.GetComponent<Instructions>().AddCommand(self.x, self.y, Helper.GetCommandTypeFromString(dropSprite.name));
            alreadyHaveIns = true;
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Sprite dropSprite = GetDropSprite(data);
        if (dropSprite != null && !alreadyHaveIns)
            Helper.SetTransparent(receivingImage,1f);
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (!alreadyHaveIns)
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