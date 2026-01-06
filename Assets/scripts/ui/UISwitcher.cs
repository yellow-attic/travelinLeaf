using UnityEngine;


public class ToolSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject Toolpanel;

    [SerializeField] private GameObject hinweispanel;
    [SerializeField] private GameObject brokenpanel;
    [SerializeField] private GameObject Infropanel;

    [SerializeField] private GameObject production;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (hinweispanel.activeSelf || brokenpanel.activeSelf)
                return;

            SwitchAlmaPanel();
        }
    }

    private void SwitchTool()
    {
        Toolpanel.SetActive(!Toolpanel.activeSelf);
    }

    private void SwitchAlmaPanel()
    {
        Infropanel.SetActive(!Infropanel.activeSelf);

        if(Infropanel.activeSelf)
        {
            production.SetActive(true);
        }
        else
        {
            production.SetActive(false);
        }

    }
}
