using UnityEngine;
using System;

namespace CookCo_opGame
{

    public class FoodManager : ItemManager
    {
        [SerializeField] Mesh[] _foodMeshArr;
        private MeshFilter _meshFilter;

        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void ChangeMesh(int index)
        {
            _meshFilter.mesh = _foodMeshArr[index];
        }
    }
}
