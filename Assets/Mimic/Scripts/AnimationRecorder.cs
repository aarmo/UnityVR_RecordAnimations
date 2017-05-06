using UnityEngine;

public class AnimationRecorder : MonoBehaviour
{
    public string clipName;
    public AnimationClip lastClip;
    public bool recordChildren = false;

    Transform[] _recordObjs;
    ObjectAnimation[] _objRecorders;
    int _maxFrames = 0;
    int _currentFrame = 0;
    float _nowTime = 0.0f;
    bool _recording = false;
    bool _recordLimitedFrames = false;

    public void SetupRecorders()
    {
        _recordObjs = (recordChildren ? gameObject.GetComponentsInChildren<Transform>() : new[] { transform });

        _objRecorders = new ObjectAnimation[_recordObjs.Length];

        for (var i = 0; i < _recordObjs.Length; i++)
        {
            var path = GetTransformPathName(transform, _recordObjs[i]);
            _objRecorders[i] = new ObjectAnimation(path, transform);
        }
    }

    void Update()
    {
        if (_recording)
        {
            _nowTime += Time.deltaTime;

            if (_recordLimitedFrames)
            {
                ++_currentFrame;

                if (_currentFrame > _maxFrames)
                {
                    StopRecording();
                    return;
                }
            }

            for (var i = 0; i < _objRecorders.Length; i++)
            {
                _objRecorders[i].AddFrame(_nowTime);
            }
        }
    }

    public void StartRecording(int maxFrames)
    {
        _recording = true;
        _nowTime = 0;
        _currentFrame = 0;
        lastClip = null;
        _recordLimitedFrames = maxFrames > 0;
        _maxFrames = maxFrames;
    }

    public void StopRecording()
    {
        _recording = false;
        BuildAnimationClip();
        SetupRecorders();
    }

    private void BuildAnimationClip()
    {
        lastClip = new AnimationClip()
        {
            name = clipName,
            legacy = true
        };

        for (var i = 0; i < _objRecorders.Length; i++)
        {
            var curves = _objRecorders[i].curves;

            for (var x = 0; x < curves.Length; x++)
            {
                lastClip.SetCurve(_objRecorders[i].pathName, typeof(Transform), curves[x].propertyName, curves[x].animCurve);
            }
        }

        lastClip.EnsureQuaternionContinuity();
    }

    public static string GetTransformPathName(Transform rootTransform, Transform targetTransform)
    {
        string returnName = targetTransform.name;
        Transform tempObj = targetTransform;

        // it is the root transform
        if (tempObj == rootTransform)
            return "";

        while (tempObj.parent != rootTransform)
        {
            returnName = tempObj.parent.name + "/" + returnName;
            tempObj = tempObj.parent;
        }

        return returnName;
    }
}
