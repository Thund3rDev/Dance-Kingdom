using UnityEngine;
using System.Collections.Generic;
using System.IO;

//Class GlobalVars, to control variables between scenes.
public class GlobalVars : MonoBehaviour
{
    public static GlobalVars globalVars;

    public float musicVolume;
    public float fxVolume;

    public float brightnessLvl;
    public bool fullScreen;
    public Resolution resolution;

    public Dictionary<string, KeyCode> keyBinds = new Dictionary<string, KeyCode>();

    //On Awake, if already exists replace it.
    //Also read options.
    void Awake()
    {
        if (globalVars == null)
        {
            globalVars = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        readOptions();
    }

    //Read the options from the options file.
    void readOptions()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/options.txt";

        //If file doesn't exist, create it.
        if (System.IO.File.Exists(path))
        {
            //Reader to read from our text file.
            StreamReader reader = new StreamReader(path, true);

            //Counter to iterate on the lines.
            int counter = 0;

            //We read options from the text file.
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(':');

                //We set options from what we've read.
                switch (counter)
                {
                    case 0:
                        musicVolume = float.Parse(line[1]);
                        break;
                    case 1:
                        fxVolume = float.Parse(line[1]);
                        break;
                    case 2:
                        brightnessLvl = float.Parse(line[1]);
                        break;
                    case 3:
                        fullScreen = bool.Parse(line[1]);
                        break;
                    case 4:
                        resolution.width = int.Parse(line[1]);
                        resolution.height = int.Parse(line[2]);
                        break;
                    case 5:
                        keyBinds.Add("StreetKeyC", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[2]));
                        keyBinds.Add("ShopKeyC", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[4]));
                        keyBinds.Add("TroopKeyC", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[6]));
                        keyBinds.Add("ArrowUp", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[8]));
                        keyBinds.Add("ArrowDown", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[10]));
                        keyBinds.Add("ArrowLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[12]));
                        keyBinds.Add("ArrowRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), line[14]));
                        break;
                }
                counter++;
            }

            //Close the reader.
            reader.Close();
        }
        else
        {
            //Writer to write on our text file.
            StreamWriter writer = new StreamWriter(path, true);

            musicVolume = 0.5f;
            fxVolume = 0.5f;
            brightnessLvl = 0.5f;
            fullScreen = true;
            resolution.width = 1920;
            resolution.height = 1080;
            keyBinds.Add("StreetKeyC", KeyCode.Q);
            keyBinds.Add("ShopKeyC", KeyCode.W);
            keyBinds.Add("TroopKeyC", KeyCode.E);
            keyBinds.Add("ArrowUp", KeyCode.UpArrow);
            keyBinds.Add("ArrowDown", KeyCode.DownArrow);
            keyBinds.Add("ArrowLeft", KeyCode.LeftArrow);
            keyBinds.Add("ArrowRight", KeyCode.RightArrow);

            //We write our options in the text file.
            writer.WriteLine("musicVolume:" + musicVolume);
            writer.WriteLine("fxVolume:" + fxVolume);
            writer.WriteLine("brightnessLvl:" + brightnessLvl);
            writer.WriteLine("fullScreen:" + fullScreen);
            writer.WriteLine("resolution:" + resolution.width + ":" + resolution.height);
            writer.WriteLine("keybinds:street:" + keyBinds["StreetKeyC"] + ":shop:" + keyBinds["ShopKeyC"] + ":troop:" + keyBinds["TroopKeyC"] + 
                ":aUp:" + keyBinds["ArrowUp"] + ":aDown:" + keyBinds["ArrowDown"] + ":aLeft:" + keyBinds["ArrowLeft"] + ":aRight:" + keyBinds["ArrowRight"]);

            //Close the writer.
            writer.Close();
        }

    }
}
