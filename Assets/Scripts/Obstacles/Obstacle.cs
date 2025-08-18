using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    { 

        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<CharacterController>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}