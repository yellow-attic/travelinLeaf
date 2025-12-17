using UnityEngine;

public class PageSwitcher : MonoBehaviour
{
    [Header("Pages (UI Panels)")]
    [SerializeField] private GameObject[] pages;

    [Header("Input")]
    private KeyCode leftKey = KeyCode.LeftArrow;
    private KeyCode rightKey = KeyCode.RightArrow;

    private int currentIndex = 0;

    void Start()
    {
        ShowPage(currentIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            PreviousPage();
        }

        if (Input.GetKeyDown(rightKey))
        {
            NextPage();
        }
    }

    void NextPage()
    {
        currentIndex++;
        if (currentIndex >= pages.Length)
            currentIndex = 0; // 循环（可改成 Clamp）

        ShowPage(currentIndex);
    }

    void PreviousPage()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = pages.Length - 1;

        ShowPage(currentIndex);
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}