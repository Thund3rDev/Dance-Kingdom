using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

//Auxiliar class SongMapper, to map any song based on real input.
public class SongMapper : MonoBehaviour
{
    string path;
    StreamWriter writer;
    float beatTempo;
    float firstBT;

    //On start, open the file.
    void Start()
    {
        //Ruta del archivo de texto.
        path = "./Dance Kingdom_Data/Resources/Mapping Files/" + SceneManager.GetActiveScene().name + "C.txt";

        //Borramos el contenido del archivo existente.
        System.IO.File.WriteAllText(path, string.Empty);

        //Escribimos en el archivo de texto.
        writer = new StreamWriter(path, true);

        beatTempo = BeatScroller.instance.beatTempo;
        firstBT = beatTempo;
    }

    //Every frame, checks if a key is pressed and write it on the file.
    void Update()
    {
        beatTempo = BeatScroller.instance.beatTempo;

        //Recogemos tiempo de notas en un txt.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Mathf.Abs(beatTempo - GameManager.instance.newBPM/60.0f) < 1e-5)
            {
                writer.WriteLine("ArrowUp-" + System.Math.Round((Time.time * beatTempo - (GameManager.instance.timeWhenBPMUpdate * (beatTempo - firstBT))), 3));
            }
            else
            {
                writer.WriteLine("ArrowUp-" + System.Math.Round((Time.time * beatTempo), 3));
            } 
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Mathf.Abs(beatTempo - GameManager.instance.newBPM / 60.0f) < 1e-5)
            {
                writer.WriteLine("ArrowDown-" + System.Math.Round((Time.time * beatTempo - (GameManager.instance.timeWhenBPMUpdate * (beatTempo - firstBT))), 3));
            }
            else
            {
                writer.WriteLine("ArrowDown-" + System.Math.Round((Time.time * beatTempo), 3));
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Mathf.Abs(beatTempo - GameManager.instance.newBPM / 60.0f) < 1e-5)
            {
                writer.WriteLine("ArrowLeft-" + System.Math.Round((Time.time * beatTempo - (GameManager.instance.timeWhenBPMUpdate * (beatTempo - firstBT))), 3));
            }
            else
            {
                writer.WriteLine("ArrowLeft-" + System.Math.Round((Time.time * beatTempo), 3));
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Mathf.Abs(beatTempo - GameManager.instance.newBPM / 60.0f) < 1e-5)
            {
                writer.WriteLine("ArrowRight-" + System.Math.Round((Time.time * beatTempo - (GameManager.instance.timeWhenBPMUpdate * (beatTempo - firstBT))), 3));
            }
            else
            {
                writer.WriteLine("ArrowRight-" + System.Math.Round((Time.time * beatTempo), 3));
            }
        }
    }

    //When closing the applicacion, close the writer.
    void OnApplicationQuit()
    {
        writer.Close();
    }
}
