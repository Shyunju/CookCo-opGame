using UnityEngine;
using System;
using Microsoft.Unity.VisualStudio.Editor;
using System.Linq;

namespace CookCo_opGame
{

    public class FoodManager : ItemManager
    {
        [SerializeField] Mesh[] _foodMeshArr;
        [SerializeField] Sprite _icon;
        private MeshFilter _meshFilter;
        public MeshFilter MeshFilter {get {return _meshFilter;}}
        public Sprite Icon { get { return _icon; } set { _icon = value; } }
        public int CurrentIndex { get; set; }


        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _icon = GameManager.Instance.ItemDataList.Find((item) => item.ItemID == ItemID).IconSprite;
            string path = GameManager.Instance.ItemDataList.Find((item) => item.ItemID == ItemID).IconPath;
        }

        public void ChangeMesh(int index)
        {
            _meshFilter.mesh = _foodMeshArr[index];
            CurrentIndex = index;
        }
    }
}
