using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedWorldGlobal : MonoBehaviour
{
    [SerializeField, Range(0f, 0.1f)]
    float curveStrength = 0f;

    public bool animate = false;
    public float amplitude = 0.0025f;
    public float frequency = 1.0f;
    public bool useUnscaledTime = false;

    static readonly int CurveProp = Shader.PropertyToID("_CurveStrength");

    void OnEnable()  
    {
        Apply();
    }

    void Update()    
    {
        Apply();
    }

    void OnDisable() 
    {
        Shader.SetGlobalFloat(CurveProp, 0f);
    }

    void Apply()
    {
        float t;
        if (useUnscaledTime)
        {
            t = Time.unscaledTime;
        }
        else
        {
            t = Time.time;
        }
        float value = curveStrength;
        if (animate)
        {
            value += Mathf.Sin(t * Mathf.PI * 2f * frequency) * amplitude;
        }
        Shader.SetGlobalFloat(CurveProp, value);
    }
}
