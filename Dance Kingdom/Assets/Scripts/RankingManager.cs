using UnityEngine;
using System.IO;

//Class RankingManager, to manage the ranking.
public class RankingManager : MonoBehaviour
{
    public RankingScores[,] rankings = new RankingScores[3,10];
    public static RankingManager instance;

    //On awake, instance itself.
    void Awake()
    {
        instance = this;
    }

    //Updates ranking checking if the score get is better than anyone.
    public void updateRanking(int genGold, float timeLeft, float totalTime, string initials, int level)
    {
        readRanking();

        bool betterFound = false;
        int timeToWin = (int)(totalTime - timeLeft);

        for (int i = 0; i < rankings.GetLength(1); i++)
        {
            if (!betterFound)
            {
                if ((timeToWin < rankings[level, i].timeToWin) || (timeToWin == rankings[level, i].timeToWin && genGold > rankings[level, i].generatedGold))
                {
                    betterFound = true;
                    for (int j = (rankings.GetLength(1) - 1); j > i; j--)
                    {
                        rankings[level, j].initials = rankings[level, j - 1].initials;
                        rankings[level, j].generatedGold = rankings[level, j - 1].generatedGold;
                        rankings[level, j].timeToWin = rankings[level, j - 1].timeToWin;
                        rankings[level, j].cc = rankings[level, j - 1].cc;
                    }

                    rankings[level, i].initials = initials;
                    rankings[level, i].generatedGold = genGold;
                    rankings[level, i].timeToWin = timeToWin;
                }
            }
        }
        writeRanking();
    }

    //Search in the ranking to see if it's a better game.
    public bool checkRanking(int genGold, float timeLeft, float totalTime, int level)
    {
        readRanking();
        int timeToWin = (int)(totalTime - timeLeft);
        bool betterFound = false;
        for (int i = 0; i < rankings.GetLength(1); i++)
        {
            if (!betterFound)
            {
                if ((timeToWin < rankings[level, i].timeToWin) || (timeToWin == rankings[level, i].timeToWin && genGold > rankings[level, i].generatedGold))
                {
                    betterFound = true;
                }
            }
        }
        return betterFound;
    }

    //Reads the ranking to get the info.
    void readRanking()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/ranking.txt";

        //Reader to read from our text file.
        StreamReader reader = new StreamReader(path, true);

        //Counter to iterate on the lines.
        int counter = 0;

        //Level to iterate on game levels.
        int lvl = -1;

        //AuxArray to iterate on the array.
        int auxArray = 0;

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
                switch (lvl)
                {
                    case 0:
                        auxArray = (counter - 1);
                        break;
                    case 1:
                        auxArray = (counter - 12);
                        break;
                    case 2:
                        auxArray = (counter - 23);
                        break;
                }

                rankings[lvl, auxArray] = new RankingScores();
                rankings[lvl, auxArray].initials = line[1];
                rankings[lvl, auxArray].timeToWin = int.Parse(line[2]);
                rankings[lvl, auxArray].generatedGold = int.Parse(line[3]);
                rankings[lvl, auxArray].cc = bool.Parse(line[4]);

            }

            counter++;
        }
        reader.Close();
    }

    //Writes the actual ranking.
    void writeRanking()
    {
        //Path the our text file.
        string path = "./Dance Kingdom_Data/Resources/ranking.txt";

        //Erase all content of the text file.
        System.IO.File.WriteAllText(path, string.Empty);

        //Writer to write on our text file.
        StreamWriter writer = new StreamWriter(path, true);

        //We write our rankings in the text file.
        writer.WriteLine("-:Easy");
        for (int i = 0; i < rankings.GetLength(1); i++)
        {
            writer.WriteLine((i + 1) + ":" + rankings[0, i].initials + ":" + rankings[0, i].timeToWin + ":" + rankings[0, i].generatedGold + ":" + rankings[0, i].cc);
        }

        writer.WriteLine("-:Normal");
        for (int i = 0; i < rankings.GetLength(1); i++)
        {
            writer.WriteLine((i + 1) + ":" + rankings[1, i].initials + ":" + rankings[1, i].timeToWin + ":" + rankings[1, i].generatedGold + ":" + rankings[1, i].cc);
        }

        writer.WriteLine("-:Hard");
        for (int i = 0; i < rankings.GetLength(1); i++)
        {
            writer.WriteLine((i + 1) + ":" + rankings[2, i].initials + ":" + rankings[2, i].timeToWin + ":" + rankings[2, i].generatedGold + ":" + rankings[2, i].cc);
        }

        //Close the writer.
        writer.Close();
    }
}

public class RankingScores
{
    public string initials;
    public int generatedGold;
    public int timeToWin;
    public bool cc;
}
