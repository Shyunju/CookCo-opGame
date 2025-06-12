using UnityEngine;
using System;
using Microsoft.Unity.VisualStudio.Editor;

namespace CookCo_opGame
{

    public class FoodManager : ItemManager
    {
        [SerializeField] Mesh[] _foodMeshArr;
        [SerializeField] GameObject _icon;
        private MeshFilter _meshFilter;
        public GameObject Icon { get { return _icon; } }


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
