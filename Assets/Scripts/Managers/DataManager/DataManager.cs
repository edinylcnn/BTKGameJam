using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace LiliApiSdk.Runtime.Storage
{
    [Serializable]
    public class TestData
    {
        //Sonradan { get; private set; } prop olacak bunlar şimdili serialize etmek için public
        
        public string testUserName  = "TestUser";
        public int testLevel        = 1;

        public void UpdateUserName(string username)
        {
            testUserName = username;
            Notify();
        }

        public void LevelUp()
        {
            testLevel++;
            Notify();
        }

        public void Notify()
        {
            DataManager.UpdateObservableClass(DataManager.Instance.ObservableTestData, this);
        }
    }

    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        public Observable<TestData> ObservableTestData = new Observable<TestData>();

        //Ayrı Class a alınacak
        private const string KEY_TestData = "TestData";

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            DontDestroyOnLoad(gameObject);

            if (!ES3.FileExists())
                CreateNewSave();
            else
                Load();

            Application.wantsToQuit += SaveOnQuit;
            Application.focusChanged += SaveOnLoseFocus;
            SceneManager.sceneLoaded += SaveOnSceneLoad;
        }

        #region Save / Load

        public void Save()
        {
            Debug.Log("All data saved");

            ES3.Save(KEY_TestData, ObservableTestData.Value);
        }

        public void Load()
        {
            ObservableTestData.Value = ES3.KeyExists(KEY_TestData) ? ES3.Load<TestData>(KEY_TestData) : new TestData();
            //EnsureDefaults();
        }

        private void CreateNewSave()
        {
            Debug.Log("Creating new save...");

            ObservableTestData.Value = new TestData();

            Save();
        }

        // private void EnsureDefaults()
        // {
        //     // level?.EnsureDefaults();
        //     // settings?.EnsureDefaults();
        //     // ObservableTilesData.Value?.EnsureDefaults();
        //     // ObservableItemsData.Value?.EnsureDefaults();
        //     //
        //     // ObservablePlayerData.Value?.EnsureDefaults();
        // }

        #endregion

        #region Unity Event Hooks

        private void SaveOnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            Save();
        }

        private void SaveOnLoseFocus(bool hasFocus)
        {
            if (!hasFocus)
                Save();
        }

        private bool SaveOnQuit()
        {
            Save();
            return true;
        }

        #endregion

        public static void UpdateObservableClass<T>(Observable<T> observableClass, T newClass)
        {
            observableClass.Value = newClass;
        }
    }
}