using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager m_Instance;

    private bool m_IsPaused = false;

    public CanvasGroup m_PauseCanvas;

    public float m_PauseFadeSpeed;

    private void Awake()
    {
        if (m_Instance)
        {
            Destroy(this);
        }
        else
        {
            m_Instance = this;
        }
    }

    void Update()
    {
        if (m_IsPaused && m_PauseCanvas.alpha < 1)
        {
            m_PauseCanvas.alpha += Time.deltaTime * (1 / m_PauseFadeSpeed);
        }
        else if (!m_IsPaused && m_PauseCanvas.alpha > 0)
        {
            m_PauseCanvas.alpha -= Time.deltaTime * (1 / m_PauseFadeSpeed);
        }
    }

    public void TogglePause(bool pause)
    {
        m_PauseCanvas.blocksRaycasts = pause;
        m_PauseCanvas.interactable = pause;
        m_IsPaused = pause;
    }
}
