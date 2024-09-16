using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            Initialization();
        }

        private void Initialization()
        {
            ScreenWrapManager.Instance.SetPlayer(this.gameObject);
        }
    }
}