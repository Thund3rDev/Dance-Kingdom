using UnityEngine;
using UnityEngine.EventSystems;

//Clase ButtonManager, to control how buttons behave.
public class ButtonManager : MonoBehaviour, IPointerClickHandler
{
    //On click, plays the button sound.
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.ManageAudio("Button", "sound", "play");
    }
}
