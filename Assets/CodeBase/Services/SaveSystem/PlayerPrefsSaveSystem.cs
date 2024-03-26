using System.Threading.Tasks;
using CodeBase.WorldDataSystem;
using UnityEngine;

namespace CodeBase.Services.SaveSystem
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string DataKey = "Data";
        
        public void Save(WorldData data)
        {
            string jsonData = JsonUtility.ToJson(data);
            
            PlayerPrefs.SetString(DataKey, jsonData);
            PlayerPrefs.Save();
        }
        
        public async Task<WorldData> Load()
        {
            if (PlayerPrefs.HasKey(DataKey))
            {
                string jsonData = PlayerPrefs.GetString(DataKey);
                return JsonUtility.FromJson<WorldData>(jsonData);
            }
            
            await Task.Yield();
            
            return new WorldData();
        }
    }
}