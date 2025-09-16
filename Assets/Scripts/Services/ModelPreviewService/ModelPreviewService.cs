using Character;
using UnityEngine;

namespace Services.ModelPreviewService
{
    public class ModelPreviewService : MonoBehaviour
    {
        [SerializeField] private Camera _previewCamera;
        [SerializeField] private Transform _previewAnchor;

        private CharacterVisuals _currentInstance;

        void Start()
        {
            _previewCamera.enabled = false;
        }

        public void Despawn()
        {
            if (_currentInstance != null)
            {
                Destroy(_currentInstance.gameObject);
                _currentInstance = null;
            }
            _previewCamera.enabled = false;
        }

        public void Spawn(CharacterVisuals prefab)
        {
            Despawn();

            if (prefab == null)
                return;

            _currentInstance = Instantiate(prefab, _previewAnchor);
            Transform t = _currentInstance.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
        
            _previewCamera.enabled = true;
        }
    }
}
