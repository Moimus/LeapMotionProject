using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {
    public GameObject sun;
    private float sunInitIntensity;

    public GameObject spot;
    private float spotInitIntensity;

    public GameObject spotDetail;
    private float spotDetailInitIntensity;

    // Use this for initialization
    void Start () {
        sunInitIntensity = sun.GetComponent<Light>().intensity;
        spotInitIntensity = spot.GetComponent<Light>().intensity;
        spotDetailInitIntensity = spotDetail.GetComponent<Light>().intensity;
        spotDetail.GetComponent<Light>().intensity = 0;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void dimSun()
    {
        sun.GetComponent<Light>().intensity = 0.1f;
    }

    public void lightSun()
    {
        sun.GetComponent<Light>().intensity = sunInitIntensity;
    }

    public void detailViewLightSetup()
    {
        dimSun();
        spotDetail.GetComponent<Light>().intensity = spotDetailInitIntensity;
        spot.GetComponent<Light>().intensity = 0;
    }

    public void defaultLightSetup()
    {
        lightSun();
        spotDetail.GetComponent<Light>().intensity = 0;
        spot.GetComponent<Light>().intensity = spotInitIntensity;
    }
}
