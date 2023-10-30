using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ConfinerManager : MonoBehaviour
{
    public List<CinemachineConfiner> confiners; // 用于存储不同场景的Cinemachine Confiner

    // 切换到指定场景的边界
    public void SwitchConfiner(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= confiners.Count) return;

        foreach (var confiner in confiners)
        {
            confiner.enabled = false;
        }

        confiners[sceneIndex].enabled = true;
    }
}

