using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSyncer : MonoBehaviour
{
    // Private variables
    private AudioSource m_audioSource;

    private static float[] m_audioSpectrum;
    private static float[] m_freqBand, m_bandBuffer;
    private float[] m_decraseBuffer;

    private static float m_amplitude, m_amplitudeBuffer;
    private float m_amplitudeHighest;
    private float m_volumeMultiplier;

    // Public variables
    [Min(0)]public int sampleNumber = 128;
    public FFTWindow FFTFunction = FFTWindow.Blackman;
    public bool usesVolumeMultiplier = true;

    private void Start() 
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSpectrum = new float[sampleNumber];
        m_freqBand = new float[8];
        m_bandBuffer = new float[8];
        m_decraseBuffer = new float[8];
        m_amplitudeHighest = 2f;
    }

    private void Update()
    {
        m_audioSource.GetSpectrumData(m_audioSpectrum, 0, FFTFunction);
        MakeVolumeMult();
        MakeFrequencyBands();
        MakeBandBuffer();
        MakeAmplitude();
    }

    private void MakeVolumeMult()
    {
        float vol = m_audioSource.volume;
        m_volumeMultiplier = usesVolumeMultiplier ? (vol == 0 ? 1 : 1/vol) : 1;
    }

    public static float[] GetAudioSpectrum() 
    {
        return m_audioSpectrum;
    }

    private void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int) Mathf.Pow(2, i);

            for (int j = count; j < sampleCount; j++)
            {
                average += m_audioSpectrum[j] * (count + 1);
                count++;
            }

            average /= count;
            m_freqBand[i] = average * 10 * m_volumeMultiplier;
        }
        m_freqBand[7] *= 3;
    }

    private void MakeBandBuffer() 
    {
        for (int i = 0; i < 8; i++)
        {
            if (m_freqBand[i] > m_bandBuffer[i])
            {
                m_bandBuffer[i] = m_freqBand[i];
                m_decraseBuffer[i] = 1f;
            }
            else
            {
                m_bandBuffer[i] -= m_decraseBuffer[i] * Time.deltaTime;
                m_decraseBuffer[i] += 50f * Time.deltaTime;
            }
        }
    }

    public static float[] GetFrequenceBands(bool isBuffered)
    {
        return isBuffered ? m_bandBuffer : m_freqBand;
    }

    private void MakeAmplitude()
    {
        float currAmplitude = 0;
        float currAmplitudeBuff = 0;

        for (int i = 0; i < 8; i++)
        {
            currAmplitude += m_freqBand[i];
            currAmplitudeBuff += m_bandBuffer[i];
        }
        if (currAmplitude > m_amplitudeHighest) 
            m_amplitudeHighest = currAmplitude;

        m_amplitude = currAmplitude / m_amplitudeHighest;
        m_amplitudeBuffer = currAmplitudeBuff / m_amplitudeHighest;
    }

    public static float GetAmplitude(bool isBuffered)
    {
        return isBuffered ? m_amplitudeBuffer : m_amplitude;
    }
}
