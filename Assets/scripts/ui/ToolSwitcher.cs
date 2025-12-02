using UnityEngine;


public class ToolSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject Toolpanel;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTool();
        }
    }

    private void SwitchTool()
    {
        Toolpanel.SetActive(!Toolpanel.activeSelf);
    }
}
