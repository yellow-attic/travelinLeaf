using UnityEngine;
using UnityEngine.UI;


public class HinweisDetail : MonoBehaviour
{
    public string HinweisTxt;
    [SerializeField] private Sprite icon;

    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject hinweispannel;

    [SerializeField] private HinweisManager hinweismanager;


    public void GetHinweis()
    {
        Infropannel.SetActive(false);
        hinweispannel.SetActive(true);

        if(hinweismanager == null)
            hinweismanager = hinweispannel.GetComponent<HinweisManager>();

        hinweismanager.AssignTexts(HinweisTxt);
    }

    public void UnlockAchivement()
    {
        GetComponent<Image>().sprite = icon;
        GetComponent<Button>().interactable = true;
    }
}
