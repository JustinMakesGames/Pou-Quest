using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float size;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x + size, gameObject.transform.localScale.y + size);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - size, gameObject.transform.localScale.y - size);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - size, gameObject.transform.localScale.y - size);
    }
}
