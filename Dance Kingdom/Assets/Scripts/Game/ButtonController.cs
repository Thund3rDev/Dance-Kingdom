using UnityEngine;

//Class ButtonController, that controls how buttons works.
public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;

    public KeyCode keyToPress;
    public KeyCode realKC;

    //On Start we search for the Sprite Renderer.
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

        updateKeys();
    }

    //Every frame we check if button is pressed.
    void Update()
    {
        //If pressed, change color to black.
        if (Input.GetKeyDown(realKC) && !GameManager.instance.gameOver)
        {
            theSR.color = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        }

        //If not pressed, change color to original.

        if (Input.GetKeyUp(realKC) && !GameManager.instance.gameOver)
        {
            theSR.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }
    }

    public void updateKeys()
    {
        if (keyToPress == KeyCode.UpArrow)
        {
            realKC = GlobalVars.globalVars.keyBinds["ArrowUp"];
        }
        else if (keyToPress == KeyCode.DownArrow)
        {
            realKC = GlobalVars.globalVars.keyBinds["ArrowDown"];
        }
        else if (keyToPress == KeyCode.LeftArrow)
        {
            realKC = GlobalVars.globalVars.keyBinds["ArrowLeft"];
        }
        else if (keyToPress == KeyCode.RightArrow)
        {
            realKC = GlobalVars.globalVars.keyBinds["ArrowRight"];
        }
    }
}