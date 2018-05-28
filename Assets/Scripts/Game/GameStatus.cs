using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameStatus : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        bool NeedsToLoad = false;
        if (PlayerPrefs.HasKey("NeedsToLoad"))
        {
            if (PlayerPrefs.GetInt("NeedsToLoad") > 0)
            {
                NeedsToLoad = true;
            }
            else
            {
                NeedsToLoad = false;
            }
        }
        else
        {
            NeedsToLoad = false;
        }

        if (NeedsToLoad)
        {
            String saveFile = "playerData.dat";
            if (File.Exists(Application.persistentDataPath + "/" + saveFile))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/" + saveFile, FileMode.Open);

                GameData gameData = (GameData)bf.Deserialize(file);

                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.GetComponent<Player>().currentMaxHp = gameData.currentMaxHp;
                player.GetComponent<Player>().hp = gameData.hp;
                player.transform.position = new Vector3(gameData.playerPositionX, gameData.playerPositionY, 0);

                player.GetComponent<Player>().initialMap = GameObject.Find("Mapas/" + gameData.playerInitialMap);
                Camera.main.GetComponent<MainCamera>().SetBound(player.GetComponent<Player>().initialMap);
                player.GetComponent<Player>().UpdateHearts();
            }
        }
    }
}

