using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfinerSystem : MonoBehaviour
{
    [SerializeField]
    private string[] namesConfiner;

    private Collider2D cofiner;

    private CinemachineConfiner cinemachineConfiner;

    private void Start()
    {
        cinemachineConfiner = GetComponent<CinemachineConfiner>();
        SceneManager.activeSceneChanged += OnSceneChanged;
        FindConfiner(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// 01 �����J�J�}�C 0 ���I��
    /// 02 �����J�J�}�C 1 ���I��
    /// 03 �����J�J�}�C 2 ���I��
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        FindConfiner(arg1.name);
    }

    /// <summary>
    /// �M��d�򪫥�
    /// </summary>
    /// <param name="nameScene"></param>
    private void FindConfiner(string nameScene)
    {
        switch (nameScene)
        {
            case "01_FLIES_�˪L _new":
                cofiner = GameObject.Find(namesConfiner[0]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            case "02_FLIES_�q�| _new":
                cofiner = GameObject.Find(namesConfiner[1]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            case "03_FLIES_�����ͺA_new":
                cofiner = GameObject.Find(namesConfiner[2]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            default:
                break;
        }
    }
}
