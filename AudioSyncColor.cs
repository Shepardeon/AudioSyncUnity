using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncColor : MonoBehaviour
{
    public Color baseColor = new Color(255, 0, 0);
    public Color beatColor = new Color(0, 0, 255);
    public EasingFunction easingFunction;
    public Renderer materialRenderer;
    public bool isEmissive;

    private float m_amplitude;
    private EaseManager m_easeManager;

    private void Start() 
    {
        if (materialRenderer == null) 
        {
            Debug.LogError("No material given to the script AudioSyncColor on " + gameObject);
            this.enabled = false;
        }

        m_easeManager = new EaseManager(easingFunction);
    }

    private void Update() 
    {
        m_amplitude = AudioSyncer.GetAmplitude(true);
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (isEmissive)
            materialRenderer.material.SetColor("_EmissionColor", Color.Lerp(
                baseColor * 11 * m_amplitude, 
                beatColor * 11 * m_amplitude, 
                m_easeManager.Ease(m_amplitude)));
        else
            materialRenderer.material.SetColor("_Color", Color.Lerp(baseColor, beatColor, m_amplitude));
    }
}
