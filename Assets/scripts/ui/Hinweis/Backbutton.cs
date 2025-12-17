using UnityEngine;

public class Backbutton : MonoBehaviour
{
    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject hinweispannel;


    public void BacktoAchivement()
    {
        Infropannel.SetActive(true);
        hinweispannel.SetActive(false);
    }
}
