using UnityEngine;

public class PlayerSpeedUp : MonoBehaviour
{
    [Header("Boost Settings")]
    [SerializeField] private float boostDistance = 2f;
    [SerializeField] private float boostDuration = 0.5f;

    private bool energyTaken = false;

    private BatteryManager batterymanager;


    private void Start()
    {
        batterymanager = FindAnyObjectByType<BatteryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            PlayerMovement player = GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.StartCoroutine(player.SpeedBoost(boostDistance, boostDuration));
            }

            if (!energyTaken)
            {
                energyTaken = true;
                batterymanager.GetLineEnergy();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Line"))
        {
            energyTaken = false;
        }
    }
}
