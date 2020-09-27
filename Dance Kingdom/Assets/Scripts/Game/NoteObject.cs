using UnityEngine;

//Clase NoteObject, for how notes works.
public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool pressed;
    public KeyCode keyToPress;
    public KeyCode realKC;
    private Animator myAnimator;

    //On Awake, get the animator component.
    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    //On start, get realKC.
    void Start()
    {
        updateKeys();
    }

    //Every frame we check if player press the note.
    void Update()
    {
        if (canBePressed)
        {
            if (Input.GetKeyDown(realKC))
            {
                GameManager.instance.NoteHit();
                pressed = true;
                myAnimator.SetBool("hit", true);
            }
        }

        //If note has been pressed, moves in opposite direction to the rest.
        if (pressed)
        {
            canBePressed = false;
            transform.position += new Vector3(0f, BeatScroller.instance.beatTempo * Time.deltaTime, 0f);
        }


    }

    //When enter on other collider, can be pressed.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    //When exit the other collider, can't be pressed.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "Activator")
            {
                canBePressed = false;
                GameManager.instance.NoteMissed();
            }
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

    //When exit the screen, destroy it.
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
