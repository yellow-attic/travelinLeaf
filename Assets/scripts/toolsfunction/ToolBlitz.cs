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
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Image img = GetComponent<Image>();
            img.color = hovercolor;
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
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
        armsound.Play();
    }

    public void BlitzIn()
    {
        blitz.SetActive(false);
    }
}
