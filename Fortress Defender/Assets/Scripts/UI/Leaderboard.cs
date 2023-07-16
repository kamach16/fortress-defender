using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private string _leaderboardPublicKey;
        [SerializeField] private TextMeshProUGUI[] _entryFields;
        [SerializeField] private TMP_InputField _playerUsernameInput;
        [SerializeField] private EnemySpawner enemySpawner;

        private void Start()
        {
            Load();
        }

        public void CloseLeaderboard()
        {
            gameObject.SetActive(false);
        }

        public void Load() => LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, OnLeaderboardLoaded);

        private void OnLeaderboardLoaded(Entry[] entries)
        {
            foreach (var entryField in _entryFields)
            {
                entryField.text = "";
            }

            for (int i = 0; i < entries.Length; i++)
            {
                _entryFields[i].text = $"{entries[i].RankSuffix()}.      {entries[i].Username}      {entries[i].Score} WAVES";
            }
        }

        public void Submit()
        {
            LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _playerUsernameInput.text, enemySpawner.waveNumber, Callback, ErrorCallback);
        }

        public void DeleteEntry()
        {
            LeaderboardCreator.DeleteEntry(_leaderboardPublicKey, Callback, ErrorCallback);
        }

        public void ResetPlayer()
        {
            LeaderboardCreator.ResetPlayer();
        }

        private void Callback(bool success)
        {
            if (success)
                Load();
        }

        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }
    }
}
