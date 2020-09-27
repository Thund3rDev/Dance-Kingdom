using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

//Class RankingReader, to read the ranking scores and show them in the Ranking Menu.
public class RankingReader : MonoBehaviour
{
    public TextMeshProUGUI[] rankingTexts = new TextMeshProUGUI[9];

    //On start, we read the ranking.
    void Start()
    {
        readRanking();
    }

    //Reads the ranking.
    void readRanking()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/ranking.txt";

        //If file exists, read it, if not, create it.
        if (System.IO.File.Exists(path))
        {
            //Reader to read from our text file.
            StreamReader reader = new StreamReader(path, true);

            //Counter to iterate on the lines.
            int counter = 0;

            //Level to iterate on game levels.
            int lvl = -1;

            //We read options from the text file.
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(':');

                if (line[0].Equals("-"))
                {
                    lvl++;
                }
                else
                {
                    int seconds = (int.Parse(line[2]) % 60);
                    int minutes = ((int.Parse(line[2]) / 60) % 60);
                    string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
                    string formattedControls = (bool.Parse(line[4])) ? "CC" : "KC";

                    //0: player | 1: time | 2: gold

                    switch (lvl)
                    {
                        case 0:
                            rankingTexts[0].text += line[1] + "\n";

                            if (formattedTime.Equals("1:30") && line[3].Equals("0"))
                            {
                                rankingTexts[1].text += "---\n";
                                rankingTexts[2].text += "---\n";
                            }
                            else
                            {

                                rankingTexts[1].text += formattedTime + "\n";
                                rankingTexts[2].text += line[3] + "\n";
                            }
                            break;
                        case 1:
                            rankingTexts[3].text += line[1] + "\n";

                            if (formattedTime.Equals("2:40") && line[3].Equals("0"))
                            {
                                rankingTexts[4].text += "---\n";
                                rankingTexts[5].text += "---\n";
                            }
                            else
                            {

                                rankingTexts[4].text += formattedTime + "\n";
                                rankingTexts[5].text += line[3] + "\n";
                            }
                            break;
                        case 2:
                            rankingTexts[6].text += line[1] + "\n";

                            if (formattedTime.Equals("3:42") && line[3].Equals("0"))
                            {
                                rankingTexts[7].text += "---\n";
                                rankingTexts[8].text += "---\n";
                            }
                            else
                            {

                                rankingTexts[7].text += formattedTime + "\n";
                                rankingTexts[8].text += line[3] + "\n";
                            }
                            break;
                    } //Fin switch

                }
                counter++;
            }
            reader.Close();
        }
        else
        {
            //Writer to write on our text file.
            StreamWriter writer = new StreamWriter(path, true);

            //We write our ranking in the text file.
            writer.WriteLine("-:Easy");
            for (int i = 0; i < 10; i++)
            {
                writer.WriteLine((i + 1) + ":---:90:0:false");
            }
            writer.WriteLine("-:Normal");
            for (int i = 0; i < 10; i++)
            {
                writer.WriteLine((i + 1) + ":---:160:0:false");
            }
            writer.WriteLine("-:Hard");
            for (int i = 0; i < 10; i++)
            {
                writer.WriteLine((i + 1) + ":---:222:0:false");
            }

            //Close the writer.
            writer.Close();

            //Calls to himself to write this time.
            readRanking();
        }


    }

}
