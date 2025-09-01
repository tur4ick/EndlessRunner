using UnityEngine;

namespace Services.WindowService
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}