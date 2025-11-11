using UnityEngine;

public class ToolStaub : MonoBehaviour
{
    [SerializeField] private GameObject leg;

    [SerializeField] private ToolConnect toolconnect;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LegOut();
        }
    }

    public void LegOut()
    {
        toolconnect.ArmIn();

        leg.SetActive(!leg.activeSelf);

        AudioSource armsound = leg.GetComponent<AudioSource>();
        armsound.Play();
    }

    public void LegIn()
    {
        leg.SetActive(false);
    }
}
