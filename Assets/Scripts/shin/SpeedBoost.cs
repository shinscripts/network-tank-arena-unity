using Unity.Netcode;
using UnityEngine;

namespace NetworkTankArena.shin
{
    public class SpeedBoost : NetworkBehaviour
    {
        [HideInInspector] public NetworkVariable<float> Multiplier { get; private set; } = new(1f);
        private double boostEndTime;

        public bool TryActivate(float multiplier, float duration)
        {
            if (!IsServer || !IsSpawned)
                return false;

            if (Multiplier.Value > 1f)
                return false;

            if (multiplier <= 1f || duration <= 0f)
                return false;

            boostEndTime = NetworkManager.ServerTime.Time + duration;
            Multiplier.Value = multiplier;
            return true;
        }

        private void Update()
        {
            if (!IsServer || !IsSpawned)
                return;

            if (Multiplier.Value > 1f && NetworkManager.ServerTime.Time >= boostEndTime)
                Multiplier.Value = 1f;
        }
    }
}