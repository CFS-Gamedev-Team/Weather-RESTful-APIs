using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System.Threading.Tasks;

public class WeatherController : MonoBehaviour
{
    private const string API_KEY = "439d4b804bc8187953eb36d2a8c26a02";
    private const float API_CHECK_MAXTIME = 10 * 60.0f; //10 minutes
    public GameObject SnowSystem;
    public string CityId;
    private float apiCheckCountdown = API_CHECK_MAXTIME;

    // Start is called before the first frame update
    void Start()
    {
        CheckSnowStatus();
    }

    // Update is called once per frame
    void Update()
    {
        apiCheckCountdown -= Time.deltaTime;

        if (apiCheckCountdown <= 0)
        {
            CheckSnowStatus();
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }

    public async void CheckSnowStatus()
    {
        bool snowing = (await GetWeather()).weather[0].main.Equals("Snow");

        if (snowing)
            SnowSystem.SetActive(true);
        else
            SnowSystem.SetActive(false);
    }
    private async Task<WeatherInfo> GetWeather()
    {
        // HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("http://api.openweathermap.org/data/2.5/weather?id={0}&APPID={1}", CityId, API_KEY));
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://samples.openweathermap.org/data/2.5/weather?id={0}&appid={1}", CityId, API_KEY));
        HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
        return info;
    }
}
