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
    /// 01 ³õ´º¥J¤J°}¦C 0 ¸¹¸I¼²
    /// 02 ³õ´º¥J¤J°}¦C 1 ¸¹¸I¼²
    /// 03 ³õ´º¥J¤J°}¦C 2 ¸¹¸I¼²
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        FindConfiner(arg1.name);
    }

    /// <summary>
    /// ´M§ä½d³òª«¥ó
    /// </summary>
    /// <param name="nameScene"></param>
    private void FindConfiner(string nameScene)
    {
        switch (nameScene)
        {
            case "01_FLIES_´ËªL _new":
                cofiner = GameObject.Find(namesConfiner[0]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            case "02_FLIES_Äq§| _new":
                cofiner = GameObject.Find(namesConfiner[1]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            case "03_FLIES_¤ô´¹¥ÍºA_new":
                cofiner = GameObject.Find(namesConfiner[2]).GetComponent<Collider2D>();
                cinemachineConfiner.m_BoundingShape2D = cofiner;
                break;
            default:
                break;
        }
    }
}
