using UnityEngine;

public class RecorderController : MonoBehaviour
{
    public UnityAnimationRecorder[] recordables;
    public Animation[] targets;
    
    public bool recordLimitedFrames = false;
    public int recordFrames = 1000;

    public SteamVR_TrackedController controller;
    public ControllerButton holdToRecord = ControllerButton.Grip;

    bool _recording = false;
    string _logMessage = "";

    private void Start()
    {
        foreach (var r in recordables)
        {
            r.SetupRecorders();
        }
    }

    void Update()
    {
        if (controller == null) return;

        var buttonPressed = false;        
        switch (holdToRecord)
        {
            case ControllerButton.Scripted:
                return;

            case ControllerButton.Grip:
                buttonPressed = controller.gripped;
                break;

            case ControllerButton.Trigger:
                buttonPressed = controller.triggerPressed;
                break;

            case ControllerButton.Pad:
                buttonPressed = controller.padPressed;
                break;
        }
        
        if (buttonPressed && !_recording)
        {
            StartRecording();
        }
        else if (_recording && !buttonPressed)
        {
            StopRecording();
        }
    }

    public void StartRecording()
    {
        Debug.Log("Starting recording...");
        foreach (var r in recordables)
        {
            r.StartRecording(recordLimitedFrames ? recordFrames : -1);
        }
        _recording = true;
    }

    public void StopRecording()
    {
        Debug.Log("Ending recording...");
        for (var i = 0; i < recordables.Length; i++)
        {
            var r = recordables[i];
            r.StopRecording();
            var clip = r.lastClip;

            if (i < targets.Length)
            {
                var t = targets[i];
                var anim = t.GetComponent<Animation>();
                if (anim != null && clip != null)
                {
                    clip.legacy = true;
                    anim.AddClip(clip, "recordedClip");
                    anim.wrapMode = WrapMode.PingPong;
                    anim.Play("recordedClip");
                }
            }
        }
        _recording = false;
    }

    public enum ControllerButton
    {
        None,
        Scripted,
        Grip,
        Trigger,
        Pad
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
