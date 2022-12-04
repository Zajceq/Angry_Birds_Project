using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public struct GameSaveData
{
    public float m_timeSinceLastSave;
    public float m_overallTime;
    public int LifetimeHits;
    public float m_masterVolume;
}

public class SaveManager : Singleton<SaveManager>
{
    public GameSaveData SaveData;
    private string m_pathBin;
    private string m_pathJSON;
    public bool UseBinary = true;

    public void Start()
    {
        SaveData.m_timeSinceLastSave = 0.0f;
        SaveData.m_masterVolume = AudioListener.volume;

        m_pathBin = Path.Combine(Application.persistentDataPath, "save.bin");
        m_pathJSON = Path.Combine(Application.persistentDataPath, "save.json");

        LoadSettings();
    }

    private void Update()
    {
        SaveData.m_timeSinceLastSave += Time.deltaTime;
    }

    public void SaveSettings()
    {
        SaveData.m_overallTime += SaveData.m_timeSinceLastSave;
        PlayerPrefs.SetFloat("OverallTime", SaveData.m_overallTime);
        SaveData.m_timeSinceLastSave = 0.0f;

        SaveData.LifetimeHits += GameplayManager.Instance.m_points;
        PlayerPrefs.SetInt("LifetimeHits", SaveData.LifetimeHits);

        if (UseBinary)
        {
            FileStream file = new FileStream(m_pathBin, FileMode.OpenOrCreate);
            BinaryFormatter binFormat = new BinaryFormatter();
            binFormat.Serialize(file, SaveData);
            file.Close();
        }
        else
        {
            string saveData = JsonUtility.ToJson(SaveData);
            File.WriteAllText(m_pathJSON, saveData);
        }
    }

    public void LoadSettings()
    {
        SaveData.m_overallTime = PlayerPrefs.GetFloat("OverallTime", 0.0f);

        SaveData.LifetimeHits = PlayerPrefs.GetInt("LifetimeHits", 0);

        if (UseBinary && File.Exists(m_pathBin))
        {
            FileStream file = new FileStream(m_pathBin, FileMode.Open);
            BinaryFormatter binFormat = new BinaryFormatter();
            SaveData = (GameSaveData)binFormat.Deserialize(file);
            file.Close();
            ApplySettings();
        }
        else if (!UseBinary && File.Exists(m_pathJSON))
        {
            string saveData = File.ReadAllText(m_pathJSON);
            SaveData = JsonUtility.FromJson<GameSaveData>(saveData);
            ApplySettings();
        }
        else
        {
            SaveData.m_timeSinceLastSave = 0.0f;
            SaveData.m_masterVolume = AudioListener.volume;
        }    
    }

    private void ApplySettings()
    {
        AudioListener.volume = SaveData.m_masterVolume;
    }

    public void OnApplicationQuit()
    {
        SaveSettings();
    }
}
