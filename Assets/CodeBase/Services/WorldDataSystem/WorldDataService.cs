using System.Threading.Tasks;
using CodeBase.Services.SaveSystem;
using CodeBase.WorldDataSystem;

namespace CodeBase.Services.WorldDataSystem
{
    public class WorldDataService : IWorldDataService
    {
        private readonly ISaveSystem _saveSystem;
        
        public WorldData WorldData { get; private set; }

        public WorldDataService(ISaveSystem saveSystem) => 
            _saveSystem = saveSystem;

        public async Task Load()
        {
            WorldData = await _saveSystem.Load();
        }

        public void Reset()
        {
            WorldData = null;
            WorldData = new WorldData();
            Save();
        }

        public void Save() => 
            _saveSystem.Save(WorldData);
    }
}