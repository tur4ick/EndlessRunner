using System;
using Services;
using UnityEngine;
using Zenject;

public class Obstacle : MonoBehaviour
{ 
    [Inject] private GameController _gameController;
    [Inject] private WindowService _windowService;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<CharacterController>();
        if (player != null)
        {
            player.Die();
            _gameController.OnPlayerDead();
        }
    }
}