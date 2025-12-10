using UnityEngine;

public class PlayerSpeedUp : MonoBehaviour
{
    [Header("Boost Settings")]
    [SerializeField] private float boostDistance = 2f;
    [SerializeField] private float boostDuration = 0.5f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            PlayerMovement player = GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.StartCoroutine(player.SpeedBoost(boostDistance, boostDuration));
            }
        }
    }
}
