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
        public MeshFilter MeshFilter {get {return _meshFilter;}}
        public GameObject Icon { get { return _icon; } }
        public int Index { get; set; }


        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
        }

        public void ChangeMesh(int index)
        {
            _meshFilter.mesh = _foodMeshArr[index];
            Index = index;
        }
    }
}
