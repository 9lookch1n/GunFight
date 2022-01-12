using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

public class JoyButton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.parent.parent.GetComponent<Ctrl_Canvas>().PressBtt();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.parent.parent.GetComponent<Ctrl_Canvas>().Ctrl_UIStart();

        transform.parent.parent.GetComponent<Ctrl_FireMelee>().Shooting();
    }
}
