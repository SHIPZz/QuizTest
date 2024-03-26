using UnityEngine;

namespace CodeBase.Services.Providers
{
    public class LocationProvider : MonoBehaviour
    {
        [field: SerializeField] public Transform UIParent { get; private set; }
    }
}