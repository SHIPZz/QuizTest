using System.Threading.Tasks;
using CodeBase.WorldDataSystem;

namespace CodeBase.Services.WorldDataSystem
{
    public interface IWorldDataService
    {
        Task Load();
        void Save();
        WorldData WorldData { get; }
        void Reset();
    }
}