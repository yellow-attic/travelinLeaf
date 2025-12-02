using UnityEngine;
using TMPro;

public class InfroManager : MonoBehaviour
{
    [SerializeField] private TMP_Text[] textTargets;
    [SerializeField] private string[] messages = new string[7]
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
        AssignRandomTexts();
    }

    void AssignRandomTexts()
    {
        // 复制数组，避免影响原本顺序
        string[] pool = (string[])messages.Clone();

        // 先打乱 pool
        ShuffleArray(pool);

        // 分配到 TextMeshPro 对象上
        for (int i = 0; i < textTargets.Length && i < pool.Length; i++)
        {
            textTargets[i].text = pool[i];
        }
    }

    // Fisher–Yates 洗牌算法
    void ShuffleArray(string[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }
}