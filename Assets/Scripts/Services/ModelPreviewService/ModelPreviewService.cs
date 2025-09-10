using UnityEngine;

public class ModelPreviewService : MonoBehaviour
{
    [SerializeField] private Camera previewCamera;
    [SerializeField] private Transform previewAnchor;

    private GameObject currentInstance;

    void Start()
    {
        previewCamera.enabled = false;
    }

    public void Despawn()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }
        previewCamera.enabled = false;
    }

    public void Spawn(GameObject prefab)
    {
        Despawn();

        if (prefab == null)
            return;

        currentInstance = Instantiate(prefab, previewAnchor);
        Transform t = currentInstance.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;

        Animator[] animators = currentInstance.GetComponentsInChildren<Animator>(true);
        int i = 0;
        while (i < animators.Length)
        {
            animators[i].applyRootMotion = false;
            animators[i].Rebind();
            animators[i].Update(0f);
            i = i + 1;
        }

        previewCamera.enabled = true;
    }
}
