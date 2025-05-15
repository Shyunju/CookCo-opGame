using UnityEngine;

namespace CookCo_opGame
{
    public class PlayerManager : MonoBehaviour
    {
        PlayerController _player1Controller;
        void Start()
        {
            _player1Controller = GetComponent<PlayerController>();
        }
        void Update()
        {
            _player1Controller.MoveCharacter();
        }
    }
}
