using UnityEngine;
using UnityEngine.UI;

public class ToolConnect : MonoBehaviour
{
    [SerializeField] private GameObject arm;

    [SerializeField] private ToolStaub toolstaub;
    [SerializeField] private Color hovercolor;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Image img = GetComponent<Image>();
            img.color = hovercolor;
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Image img = GetComponent<Image>();
            img.color = Color.white;

            SelectTools selecttools = GetComponentInParent<SelectTools>();
            selecttools.SelectedButton(GetComponent<Button>());

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
