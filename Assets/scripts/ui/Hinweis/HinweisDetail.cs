using UnityEngine;

public class HinweisDetail : MonoBehaviour
{
    public string HinweisTxt;

    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject hinweispannel;


    public void GetHinweis()
    {
        Infropannel.SetActive(false);
        hinweispannel.SetActive(true);
    }
}
