using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private TextMeshProUGUI BestScoreText;
    public TMP_InputField EnterName;
    public string PlayerName;
    public string BestScoreName;
    public int BestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string BestScoreName;
        public int BestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.BestScoreName = BestScoreName;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savedata.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savedata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            EnterName.text = data.BestScoreName;
            BestScoreName = data.BestScoreName;
            BestScore = data.BestScore;
        }

        BestScoreText = GameObject.Find("Best Score").GetComponent<TextMeshProUGUI>();
        BestScoreText.text = "Best Score : " + BestScoreName + " : " + BestScore;
    }
}
