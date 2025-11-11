using UnityEngine;

public class ToolConnect : MonoBehaviour
{
    [SerializeField] private GameObject arm;

    [SerializeField] private ToolStaub toolstaub;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ArmOut();
        }
    }

    public void ArmOut()
    {
        toolstaub.LegIn();

        arm.SetActive(!arm.activeSelf);

        AudioSource armsound = arm.GetComponent<AudioSource>();
        armsound.Play();
    }

    public void ArmIn()
    {
        arm.SetActive(false);
    }
}
