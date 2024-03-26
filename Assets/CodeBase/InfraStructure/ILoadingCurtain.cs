using System;
using System.Threading.Tasks;

namespace CodeBase.InfraStructure
{
    public interface ILoadingCurtain
    {
        event Action Closed;
        void Show(float sliderDuration);
        Task Hide();
    }
}