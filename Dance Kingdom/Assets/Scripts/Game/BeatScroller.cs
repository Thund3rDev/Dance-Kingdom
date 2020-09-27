using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class BeatScroller, that controls musical notes.
public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    public NoteObject arrow;

    public static BeatScroller instance;

    //On Awake, set beatTempo to notes por second and instance itself.
    void Awake()
    {
        instance = this;
        beatTempo = beatTempo / 60f;
        if (instance.name.Equals("NoteHolder2"))
            readNotes();
    }

    //Every frame, notes go down.
    void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }

    //Read the notes from the text file.
    void readNotes()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/Mapping Files/" + SceneManager.GetActiveScene().name + ".txt";

        //Reader to read from our text file.
        StreamReader reader = new StreamReader(path, true);

        //Type of the note.
        int type = -1;

        //We read notes from the text file.
        while (!reader.EndOfStream)
        {
            string[] line = reader.ReadLine().Split('-');

            if (line[0].Equals("ArrowLeft"))
            {
                type = 0;
            }
            else if (line[0].Equals("ArrowDown"))
            {
                type = 1;
            }
            else if (line[0].Equals("ArrowUp"))
            {
                type = 2;
            }
            else if (line[0].Equals("ArrowRight"))
            {
                type = 3;
            }

            genNote(type, float.Parse(line[1]));

        }

        //Close the reader.
        reader.Close();
    }

    //Generate notes depending on parameters.
    void genNote(int type, float height)
    {
        //Left
        if (type == 0)
        {
            arrow.keyToPress = KeyCode.LeftArrow;
            Instantiate(arrow, new Vector3((-1.5f + 5.75f), (height - 3.55f), 0.0f), Quaternion.Euler(0.0f, 0.0f, 180.0f), this.transform);
        }
        //Down
        else if (type == 1)
        {
            arrow.keyToPress = KeyCode.DownArrow;
            Instantiate(arrow, new Vector3((-0.5f + 5.75f), (height - 3.55f), 0.0f), Quaternion.Euler(0.0f, 0.0f, 270.0f), this.transform);
        }
        //Up
        else if (type == 2)
        {
            arrow.keyToPress = KeyCode.UpArrow;
            Instantiate(arrow, new Vector3((0.5f + 5.75f), (height - 3.55f), 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f), this.transform);
        }
        //Right
        else if (type == 3)
        {
            arrow.keyToPress = KeyCode.RightArrow;
            Instantiate(arrow, new Vector3((1.5f + 5.75f), (height - 3.55f), 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f), this.transform);
        }
    }
}
