using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class StandingController : MonoBehaviour
{
    public static StandingController standingInstance;

    public Standing userStanding;

    public void Awake()
    {
        if (standingInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        standingInstance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class Standing
    {
        public string userName;
        public int highScore;
    }

    [System.Serializable]
    public class SaveData
    {
        public List<Standing> userStandings;
    }

    public void SaveStanding()
    {
        SaveData standings = LoadStandings();
        Debug.Log(Application.persistentDataPath);

        if (standings.userStandings != null)
        {
            if (standings.userStandings.Exists(x => x.userName == standingInstance.userStanding.userName))
            {
                Standing matchedStanding = standings.userStandings.Find(x => x.userName == standingInstance.userStanding.userName);
                if (matchedStanding.highScore < standingInstance.userStanding.highScore)
                {
                    matchedStanding.highScore = standingInstance.userStanding.highScore;
                }
                else
                {
                    standingInstance.userStanding.highScore = matchedStanding.highScore;
                }
            }
            else
            {
                standings.userStandings.Add(standingInstance.userStanding);
            }
        }
        else
        {
            standings.userStandings = new List<Standing>();
            standings.userStandings.Add(standingInstance.userStanding);
        }


        OrderStandings(standings);

        string json = JsonUtility.ToJson(standings);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public SaveData LoadStandings()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        SaveData data = new SaveData();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }

        return data;
    }

    public void OrderStandings(SaveData data)
    {
        data.userStandings = data.userStandings.OrderByDescending(x => x.highScore).ToList();
    }
}
