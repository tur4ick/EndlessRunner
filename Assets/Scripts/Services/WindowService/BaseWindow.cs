using UnityEngine;

namespace Services
{
    public abstract class BaseWindow : MonoBehaviour
    {
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}