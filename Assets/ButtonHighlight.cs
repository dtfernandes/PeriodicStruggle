using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {

       GetComponent<RectTransform>().sizeDelta = new Vector2(49.36f, 51.47f);

    }
	

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {

        GetComponent<RectTransform>().sizeDelta = new Vector2(62.3f, 69.2f);

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        GetComponent<RectTransform>().sizeDelta = new Vector2(49.36f, 51.47f);

    }

    void  IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {

        GetComponent<RectTransform>().sizeDelta = new Vector2(62.3f, 69.2f);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
