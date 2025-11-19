using UnityEngine;
using System.Collections.Generic;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private List<Camera> cameras = new List<Camera>();
    private int currentIndex = 0;

    void Start()
    {
        // 关闭所有相机，只开第一个
        for (int i = 0; i < cameras.Count; i++)
            cameras[i].gameObject.SetActive(i == 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // 关闭当前相机
        cameras[currentIndex].gameObject.SetActive(false);

        // 下一个 index
        currentIndex = (currentIndex + 1) % cameras.Count;

        // 开启新的相机
        cameras[currentIndex].gameObject.SetActive(true);
    }
}