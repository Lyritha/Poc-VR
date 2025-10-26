using UnityEngine;

public class HighlightCollider : MonoBehaviour
{
    [SerializeField]
    private float outlineThickness = 0.01f;

    [SerializeField]
    private Material mat;

    [SerializeField]
    private Mesh mesh;

    private GameObject visualMesh;

    private void Awake()
    {
        CreateVisualMesh();
        Hide();
    }

    private void CreateVisualMesh()
    {
        if (visualMesh != null) return;

        Vector3 parentScale = transform.lossyScale;


        visualMesh = new GameObject("ColliderHighlightMesh");
        visualMesh.transform.SetParent(transform, false);
        visualMesh.transform.localPosition = Vector3.zero;
        visualMesh.transform.localRotation = Quaternion.identity;
        visualMesh.transform.localScale = new Vector3(
            1f / parentScale.x,
            1f / parentScale.y,
            1f / parentScale.z
        );

        MeshFilter mf = visualMesh.AddComponent<MeshFilter>();
        MeshRenderer mr = visualMesh.AddComponent<MeshRenderer>();

        // ✨ Clone and invert the mesh for an inverse hull outline
        Mesh meshCopy = Instantiate(mesh);
        Vector3[] verts = meshCopy.vertices;
        Vector3[] normals = meshCopy.normals;

        for (int i = 0; i < verts.Length; i++)
        {
            // push vertices outward to make visible outline
            verts[i] += normals[i] * outlineThickness;
        }

        meshCopy.vertices = verts;
        meshCopy.normals = normals;
        meshCopy.RecalculateBounds();

        mf.sharedMesh = meshCopy;
        mr.sharedMaterial = mat;

        // Apply material
        if (visualMesh.TryGetComponent<MeshRenderer>(out var renderer))
            renderer.sharedMaterial = mat;

        // Remove collider (from CreatePrimitive)
        if (visualMesh.TryGetComponent<Collider>(out var visualCol))
            Destroy(visualCol);
    }

    [ContextMenu("Show Highlight")]
    public void Show()
    {
        if (visualMesh != null)
            visualMesh.SetActive(true);
    }

    [ContextMenu("Hide Highlight")]
    public void Hide()
    {
        if (visualMesh != null)
            visualMesh.SetActive(false);
    }
}