using UnityEngine;
using UnityEngine.UI;

public class ToolStaub : MonoBehaviour
{
    [SerializeField] private GameObject leg;

    [SerializeField] private ToolConnect toolconnect;
    [SerializeField] private ToolBlitz toolblitz;

    [SerializeField] private Color hovercolor;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Image img = GetComponent<Image>();
            img.color = hovercolor;
        }

        if (Leave.Tools.GetToolPressed(Leave.Tool.Staub))
        {
            Image img = GetComponent<Image>();
            img.color = Color.white;

            SelectTools selecttools = GetComponentInParent<SelectTools>();
            selecttools.SelectedButton(GetComponent<Button>());

            LegOut();
        }
    }

    public void LegOut()
    {
        toolconnect.ArmIn();
        toolblitz.BlitzIn();

        leg.SetActive(!leg.activeSelf);

        AudioSource armsound = leg.GetComponent<AudioSource>();
        if (armsound != null)
        {
            armsound.Play();
        }
    }

    public void LegIn()
    {
        leg.SetActive(false);
    }
}
