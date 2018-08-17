using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragMeToDelete : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public bool dragOnSurfaces = true;
	
	private Dictionary<int,GameObject> m_DraggingIcons = new Dictionary<int, GameObject>();
	private Dictionary<int, RectTransform> m_DraggingPlanes = new Dictionary<int, RectTransform>();
    private GameObject newOne;
    private Instructions Instructions;
    private bool isHand;

    void Start()
    {
        Instructions = GameObject.Find("Controller").GetComponent<Instructions>();
        isHand = gameObject.GetComponent<HandSpriteController>() != null;
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
        Helper.SetTransparent(gameObject.GetComponent<Image>(), 0);

		var canvas = FindInParents<Canvas>(gameObject);
		if (canvas == null)
			return;

		newOne = new GameObject("icon");

		newOne.transform.SetParent (canvas.transform, false);
		newOne.transform.SetAsLastSibling();
	    newOne.transform.localScale = new Vector3(0.5f,0.5f,0.5f);


        var image = newOne.AddComponent<Image>();
		var group = newOne.AddComponent<CanvasGroup>();
		group.blocksRaycasts = false;

		image.sprite = GetComponent<Image>().sprite;
		
		if (dragOnSurfaces)
			m_DraggingPlanes[eventData.pointerId] = transform as RectTransform;
		else
			m_DraggingPlanes[eventData.pointerId]  = canvas.transform as RectTransform;
		
		SetDraggedPosition(eventData);

	}

	public void OnDrag(PointerEventData eventData)
	{
		if (newOne != null)
			SetDraggedPosition(eventData);
	}

	private void SetDraggedPosition(PointerEventData eventData)
	{
		if (dragOnSurfaces && eventData.pointerEnter != null && eventData.pointerEnter.transform as RectTransform != null)
			m_DraggingPlanes[eventData.pointerId] = eventData.pointerEnter.transform as RectTransform;
		
		var rt = newOne.GetComponent<RectTransform>();
		Vector3 globalMousePos;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_DraggingPlanes[eventData.pointerId], eventData.position, eventData.pressEventCamera, out globalMousePos))
		{
			rt.position = globalMousePos;
			rt.rotation = m_DraggingPlanes[eventData.pointerId].rotation;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (newOne != null)
			Destroy(newOne);

		newOne = null;

        if (isHand)
        {
            Instructions.DeleteHand(gameObject.GetComponent<HandSpriteController>().order);
        }
        else
        {
            Instructions.DeleteCommand(gameObject.GetComponent<CommandSpriteController>().GetCoordinate());
        }

        Helper.SetTransparent(gameObject.GetComponent<Image>(), 1);
    }

    static public T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null) return null;
		var comp = go.GetComponent<T>();

		if (comp != null)
			return comp;
		
		var t = go.transform.parent;
		while (t != null && comp == null)
		{
			comp = t.gameObject.GetComponent<T>();
			t = t.parent;
		}
		return comp;
	}
}
