using UnityEngine;

public class FadeImgCtrl : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneChanger.instance.OnFadeComplete();
    }
}
