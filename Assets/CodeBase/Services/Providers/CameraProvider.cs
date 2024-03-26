using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class CameraProvider : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }
    }
}