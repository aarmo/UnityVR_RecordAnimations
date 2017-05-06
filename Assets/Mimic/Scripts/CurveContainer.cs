using UnityEngine;
using System.Collections;

public class CurveContainer
{
    public string propertyName = "";
    public AnimationCurve animCurve;

    public CurveContainer(string _propertyName)
    {
        animCurve = new AnimationCurve();
        propertyName = _propertyName;
    }

    public void AddValue(float animTime, float animValue)
    {
        var key = new Keyframe(animTime, animValue, 0.0f, 0.0f);
        animCurve.AddKey(key);
    }
}