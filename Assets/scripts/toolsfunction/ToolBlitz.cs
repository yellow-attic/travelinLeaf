using UnityEngine;
using UnityEngine.UI;

public class ToolBlitz : MonoBehaviour
{
    [SerializeField] private GameObject blitz;

    [SerializeField] private ToolConnect toolconnect;
    [SerializeField] private ToolStaub toolstaub;

    [SerializeField] private Color hovercolor;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Image img = GetComponent<Image>();
            img.color = hovercolor;
        }

        if (Input.GetKeyUp(KeyCode.Alpha3) || Raumkapsel.VR.Tools.GetToolPressed(Raumkapsel.VR.Tool.Blitz))
        {
            Image img = GetComponent<Image>();
            img.color = Color.white;

            SelectTools selecttools = GetComponentInParent<SelectTools>();
            selecttools.SelectedButton(GetComponent<Button>());

            BlitzOut();
        }
    }

    public void BlitzOut()
    {
        toolconnect.ArmIn();
        toolstaub.LegIn();

        blitz.SetActive(!blitz.activeSelf);

        AudioSource armsound = blitz.GetComponent<AudioSource>();
        if(armsound != null)
        {
            armsound.Play();
        }
    }

    public void BlitzIn()
    {
        blitz.SetActive(false);
    }
}
