using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AudioSyncCircle : MonoBehaviour
{
    private LineRenderer m_lineRenderer;
    private Color m_lineColor;
    private int m_numPoints;
    [Min(0.1f)]public float radius = 1f;

    private float[] m_samples;

    private void Start() 
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_numPoints = 24 + 2;

        m_samples = AudioSyncer.GetFrequenceBands(true);

        m_lineRenderer.positionCount = m_numPoints;
        m_lineRenderer.useWorldSpace = false;
        m_lineColor = m_lineRenderer.material.GetColor("_EmissionColor");

        CreatePoints();
    }

    private void Update() 
    {
        m_samples = AudioSyncer.GetFrequenceBands(true);
        //transform.Rotate(Vector3.forward * 50 * Time.deltaTime, Space.Self);
        CreatePoints();
        //ChangeColor();
        ChangeScale();
    }

    private void CreatePoints()
    {
        float x, y;
        float angle = 20f;
        float sample = 0;
        float mult;

        for (int i = 0; i < m_lineRenderer.positionCount - 2; i++)
        {
            float radAngle = Mathf.Deg2Rad * angle;

            if (i%2 == 0)
                mult = 2f;
            else
                mult = 1.9f;

            if (m_samples != null)
                sample = Mathf.Max(m_samples[i/3] * mult, radius);

            x = Mathf.Sin(radAngle) * radius * sample;
            y = Mathf.Cos(radAngle) * radius * sample;

            m_lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / (m_numPoints - 2));
        }

        // To close the circle + hide the broken bloom
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount-2, m_lineRenderer.GetPosition(0));
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount-1, m_lineRenderer.GetPosition(1));
    }

    private void ChangeColor()
    {
        m_lineRenderer.material.SetColor("_EmissionColor", m_lineColor * 11 * AudioSyncer.GetAmplitude(true));
    }

    private void ChangeScale()
    {
        float maxScale = 0.5f;
        float amp = AudioSyncer.GetAmplitude(true);
        transform.localScale = new Vector3(1 + amp*maxScale, 1 + amp*maxScale, 1 + amp*maxScale);
    }
}
