using System;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour {

    public CropsManager cropsManager;
    public CoinManager coinManager;

    private String saveFilePath;
    private Boolean dontSave;

    public void Save() {
        if (dontSave) {
            return;
        }

        DateTime lastSaveTime = DateTime.UtcNow;
        CropsManagerData cropsManagerData = cropsManager.SaveData();
        CoinManagerData coinManagerData = coinManager.SaveData();
        AllSaveData allSaveData = new AllSaveData(cropsManagerData, coinManagerData, lastSaveTime);
        BinaryFormatter bf = new BinaryFormatter();
        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
        FileStream file = File.Create(saveFilePath); //you can call it anything you want

        bf.Serialize(file, allSaveData);
        file.Close();
    }

    public void Load() {
        if (File.Exists(saveFilePath)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            AllSaveData allSaveData = (AllSaveData) bf.Deserialize(file);
            file.Close();

            cropsManager.LoadData(allSaveData.cropsManagerData);
            coinManager.LoadData(allSaveData.coinManagerData, allSaveData.lastSaveTime);
        } else {
            cropsManager.Initialise();
            coinManager.Initialise();
            dontSave = false;
        }
    }

    private void OnApplicationQuit() {
        Save();
    }

    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            Save();
        }
    }

    private void Start() {
        dontSave = false;
        saveFilePath = Application.persistentDataPath + "/FirstIdleGameSave.data";
        Load();

        // AutoSave every minute
        InvokeRepeating("Save", 3f, 60f);
    }

    public void DeleteSavedData() {
        dontSave = true;
        File.Delete(saveFilePath);
        Load();
    }

    [System.Serializable]
    public class AllSaveData {

        public CropsManagerData cropsManagerData;
        public CoinManagerData coinManagerData;
        public DateTime lastSaveTime;

        public AllSaveData(CropsManagerData cropsManagerData, CoinManagerData coinManagerData, DateTime lastSaveTime) {
            this.cropsManagerData = cropsManagerData;
            this.coinManagerData = coinManagerData;
            this.lastSaveTime = lastSaveTime;
        }

    }

}