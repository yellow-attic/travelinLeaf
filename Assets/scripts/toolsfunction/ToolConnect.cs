using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ToolConnect : MonoBehaviour
{
    [SerializeField] private GameObject arm;

    [SerializeField] private ToolStaub toolstaub;
    [SerializeField] private ToolBlitz toolblitz;

    [SerializeField] private Color hovercolor;

    private CameraFollow camerafollow;
    private PlayerMovement player;

    private void Start()
    {
        camerafollow = FindAnyObjectByType<CameraFollow>();
        player = FindAnyObjectByType<PlayerMovement>();
    }

    void Update() {
        // switches to arms tool - check what to do for it actually
        //bool primaryDown = VrOrigin.GetLeftPrimaryButton() || VrOrigin.GetRightPrimaryButton();
        //if (primaryDown && !primaryButtonDown) {
        //    ArmOut();
        //}
        //primaryButtonDown = primaryDown;


        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Image img = GetComponent<Image>();
            img.color = hovercolor;
        }

        if (Input.GetKeyUp(KeyCode.Alpha4) || Raumkapsel.VR.Tools.GetToolPressed(Raumkapsel.VR.Tool.Arms))
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
        toolblitz.BlitzIn();

        arm.SetActive(!arm.activeSelf);

        AudioSource armsound = arm.GetComponent<AudioSource>();
        armsound.Play();

        camerafollow.ToggleFocusMode();
        player.MoveChange();
    }

    public void ArmIn()
    {
        arm.SetActive(false);

        camerafollow.ExitFocusMode();
        player.MoveContinue();
    }

    public void ConnectFailed()
    {
        SelectTools selecttools = GetComponentInParent<SelectTools>();
        selecttools.SelectedButton(GetComponent<Button>());

        ArmIn();
    }
}
