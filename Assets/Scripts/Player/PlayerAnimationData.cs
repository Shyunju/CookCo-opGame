using System;
using UnityEngine;

namespace CookCo_opGame
{
    [Serializable]
    public class PlayerAnimationData
    {
        [SerializeField] string defaultParameterName = "@Default";
        [SerializeField] string idleParameterName = "Idle";
        [SerializeField] string walkParameterName = "Walk";
        [SerializeField] string runParameterName = "Run";

        [SerializeField] string cookParameterName = "@Cook";
        [SerializeField] string cutParameterName = "Cut";

        [SerializeField] string waterParameterName = "@Water";
        [SerializeField] string washParameterName = "Wash";

        public int DefaultParameterHash { get; private set; }
        public int IdleParameterHash { get; private set; }
        public int WalkParameterHash { get; private set; }
        public int RunParameterHash { get; private set; }
        public int CookParameterHash { get; private set; }
        public int CutParameterHash { get; private set; }
        public int WaterParameterHash { get; private set; }
        public int WashParameterHash { get; private set; }
        public void Initialize()
        {
            DefaultParameterHash = Animator.StringToHash(defaultParameterName);
            IdleParameterHash = Animator.StringToHash(idleParameterName);
            WalkParameterHash = Animator.StringToHash(walkParameterName);
            RunParameterHash = Animator.StringToHash(runParameterName);

            CookParameterHash = Animator.StringToHash(cookParameterName);
            CutParameterHash = Animator.StringToHash(cutParameterName);

            WaterParameterHash = Animator.StringToHash(waterParameterName);
            WashParameterHash = Animator.StringToHash(washParameterName);

        }
    }
}
