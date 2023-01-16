using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : Singleton<AnalyticsManager>
{
    public void SendEvent(string eventName, string parameterName, object parameterValue)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add(parameterName, parameterValue);
        SendEvent(eventName, parameters);
    }

    public void SendEvent(string eventName, Dictionary<string, object> parameters = null)
    {
        AnalyticsResult result;
        if (parameters == null)
        {
            //result = AnalyticsEvent.Custom(eventName);
            result = Analytics.CustomEvent(eventName);
            //result = AnalyticsService.Instance.CustomData(eventName, null);
        }
        else
        {
            //result = AnalyticsEvent.Custom(eventName, parameters);
            result = Analytics.CustomEvent(eventName, parameters);
            //result = AnalyticsService.Instance.CustomData(eventName, parameters);
        }
        Debug.LogFormat("Event {0} Result : {1}", eventName, result);
    }

    private void OnApplicationQuit()
    {
        TimeSpan time = TimeSpan.FromMilliseconds(AnalyticsSessionInfo.sessionElapsedTime);
        string parameterValue = time.ToString("g");
        SendEvent("SessionTime", "time", parameterValue);
    }
}
