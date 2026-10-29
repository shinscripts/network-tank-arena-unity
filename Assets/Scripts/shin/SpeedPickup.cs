using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace NetworkTankArena.shin
{
    public class SpeedPickup : NetworkBehaviour
    {
        [SerializeField] private float multiplier = 1.5f;
        [SerializeField] private float duration = 5f;

        private bool collected;

        private void Update()
        {
            transform.Rotate(Vector3.up, Random.Range(20f, 35f) * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer || !IsSpawned || collected)
                return;

            var boost = other.GetComponentInParent<SpeedBoost>();
            if (!boost)
                return;

            if (!boost.TryActivate(multiplier, duration))
                return;

            collected = true;
            Debug.Log("Speed boost collected!");
            NetworkObject.Despawn();
        }
    }
}