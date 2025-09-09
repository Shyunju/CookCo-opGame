using UnityEngine;

namespace CookCo_opGame
{
    public class ChangeChairMesh : MonoBehaviour
    {
        [SerializeField] Mesh[] _meshes;
        MeshFilter _meshFilter;

        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            int index = Mathf.Clamp(GameManager.Instance.Aggregate / 10000, 0, _meshes.Length - 1);
            _meshFilter.mesh = _meshes[index];
        }
    }
}
