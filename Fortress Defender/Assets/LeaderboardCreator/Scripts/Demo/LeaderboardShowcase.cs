using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class LeaderboardShowcase : MonoBehaviour
    {
        [SerializeField] private string _leaderboardPublicKey;
        
        [SerializeField] private TextMeshProUGUI _playerScoreText;
        [SerializeField] private TextMeshProUGUI[] _entryFields;
        
        [SerializeField] private TMP_InputField _playerUsernameInput;

        private int _playerScore;
        
        private void Start()
        {
            Load();
        }

        public void AddPlayerScore()
        {
            _playerScore++;
            _playerScoreText.text = "Your score: " + _playerScore;
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
                _entryFields[i].text = $"{entries[i].RankSuffix()}. {entries[i].Username} : {entries[i].Score}";
            }
        }

        public void Submit()
        {
            LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _playerUsernameInput.text, _playerScore, Callback, ErrorCallback);
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
