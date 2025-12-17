using UnityEngine;
using TMPro;

public class HinweisManager : MonoBehaviour
{
    [SerializeField] private GameObject Infropannel;
    [SerializeField] private GameObject hinweispannel;
    [SerializeField] private TMP_Text textTarget;

    private string[] messages = new string[7]
    {
        "Hier befindet man sich in der inneren Membran.",
        "Der Durchmesser dieses Ortes beträgt 7,3 Nanometer.",
        "Mehrere Komplexe füllen diesen Ort.",
        "Die Umwandlung von Energie macht diesen Ort zu einem lebensnotwendigen.",
        "Was man sieht, sieht man hier nur einmal ist aber vielfach vorhanden.",
        "Hier wird Energie gesammelt und entsprechend weitergeleitet.",
        "Alles hier kann sich an die umgebenden Bedingungen anpassen."
    };

    void Start()
    {
        
    }

    public void AssignTexts(string currenttxt)
    {
        textTarget.text = currenttxt;

        Infropannel.SetActive(false);
        hinweispannel.SetActive(true);
    }

    public void Reset()
    {
        textTarget.text = null;

        hinweispannel.SetActive(false);
    }
}