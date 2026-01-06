using UnityEngine;

public class Backbutton : MonoBehaviour
{
    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject hinweispannel;

    [SerializeField] private GameObject scalepage;
    [SerializeField] private GameObject achievementpage;

    public void BacktoAchivement()
    {
        Infropannel.SetActive(true);

        scalepage.SetActive(false);
        achievementpage.SetActive(true);

        hinweispannel.SetActive(false);
    }
}
