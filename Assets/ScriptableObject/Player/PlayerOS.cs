using UnityEngine;
using System;
namespace CookCo_opGame
{
    [Serializable]
    public class PlayerDefaultData
    {
        [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; } = 1f;

        [Header("IdleData")]
        [Header("WalkData")]
        [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; } = 0.225f;
        [Header("RunData")]
        [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; } = 1f;
    }
    [Serializable]
    public class PlayerCookData
    {
        [field: SerializeField][field: Range(0f, 2f)] public float CutSpeedModifier { get; private set; } = 1f;
    }
    [Serializable]
    public class PlayerWaterData
    {
        [field: SerializeField][field: Range(0f, 2f)] public float WashSpeedModifier { get; private set; } = 1f;
    }

    [CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
    public class PlayerOS : ScriptableObject
    {
        [field: SerializeField] public PlayerDefaultData DefaultData { get; private set; }
        [field: SerializeField] public PlayerCookData CookData { get; private set; }
        [field: SerializeField] public PlayerWaterData WaterData { get; private set; }
    }
}
