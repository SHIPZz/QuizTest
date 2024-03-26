using System.Threading.Tasks;
using CodeBase.WorldDataSystem;

namespace CodeBase.Services.SaveSystem
{
    public interface ISaveSystem
    {
        void Save(WorldData data);

        Task<WorldData> Load();
    }
}