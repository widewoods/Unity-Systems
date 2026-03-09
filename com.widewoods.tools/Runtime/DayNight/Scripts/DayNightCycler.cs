using System;
using UnityEngine;

public class DayNightCycler : MonoBehaviour
{
    [SerializeField] private GameObject directionalLight;
    [SerializeField] private Material skybox;

    [Range(0f, 24f)]
    [SerializeField] private float inGameHour;
    private float normalizedTime;

    [SerializeField] private float inGameMinutesPerSecond;

    [SerializeField] private Gradient lightColorByTime;
    [SerializeField] private AnimationCurve intensityByTime = AnimationCurve.Linear(0, 0, 1, 1);

    [SerializeField] private Gradient skyColorByTime;
    [SerializeField] private Gradient equatorColorByTime;
    [SerializeField] private Gradient groundColorByTime;
    [SerializeField] private AnimationCurve ambientIntensityByTime = AnimationCurve.Linear(0, 0, 1, 1);


    [SerializeField] private AnimationCurve skyboxExposureByTime = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField] private AnimationCurve skyboxAtmosphereByTime = AnimationCurve.Linear(0, 0, 1, 1);


    private Transform sunTransform;
    private Light sunLight;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        inGameHour += inGameMinutesPerSecond / 60 * Time.deltaTime;
        if (inGameHour >= 24f)
        {
            inGameHour = 0f;
        }
        normalizedTime = NormalizedInGameHour();
        UpdateSunDirection(normalizedTime);
        UpdateSunLight(normalizedTime);
        UpdateEnvironmentLighting(normalizedTime);
        UpdateSkybox(normalizedTime);
    }

    void Initialize()
    {
        sunTransform = directionalLight.transform;
        sunLight = directionalLight.GetComponent<Light>();
    }

    void UpdateSunDirection(float normalizedTime)
    {
        float angle = Mathf.Lerp(-90f, 270f, normalizedTime);
        sunTransform.rotation = Quaternion.Euler(angle, 20, 20);
    }

    void UpdateSunLight(float normalizedTime)
    {
        sunLight.color = lightColorByTime.Evaluate(normalizedTime);
        sunLight.intensity = intensityByTime.Evaluate(normalizedTime);
    }

    void UpdateEnvironmentLighting(float normalizedTime)
    {
        RenderSettings.ambientSkyColor = skyColorByTime.Evaluate(normalizedTime);
        RenderSettings.ambientEquatorColor = equatorColorByTime.Evaluate(normalizedTime);
        RenderSettings.ambientGroundColor = groundColorByTime.Evaluate(normalizedTime);
        RenderSettings.ambientIntensity = ambientIntensityByTime.Evaluate(normalizedTime);
    }

    void UpdateSkybox(float normalizedTime)
    {
        skybox.SetFloat("_Exposure", skyboxExposureByTime.Evaluate(normalizedTime));
        skybox.SetFloat("_AtmosphereThickness", skyboxAtmosphereByTime.Evaluate(normalizedTime));
    }

    public float NormalizedInGameHour()
    {
        return inGameHour / 24;
    }
}
