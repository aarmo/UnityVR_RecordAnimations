using UnityEngine;

public class RecorderController : MonoBehaviour
{
    public AnimationRecorder[] recordables;
    public Animation[] targets;
    public string[] recordingNames = new[] { "activate", "idle1", "idle2", "idle3", "idle4", "attack1", "block1", "attack2", "block2", "attack3", "block3", "attack4", "block4" };

    public bool recordLimitedFrames = false;
    public int recordFrames = 1000;
    
    public SteamVR_TrackedController controller;
    public ControllerButton holdToRecord = ControllerButton.Grip;

    int _recordingIndex = 0;
    bool _recording = false;

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

    public bool StartRecording()
    {
        if (_recordingIndex >= recordingNames.Length) return false;

        foreach (var r in recordables)
        {
            r.StartRecording(recordLimitedFrames ? recordFrames : -1);
        }
        _recording = true;
        Debug.Log("Start recording: " + recordingNames[_recordingIndex]);
        return true;
    }

    public bool StopRecording()
    {
        if (!_recording) return false;

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
                    anim.AddClip(clip, recordingNames[_recordingIndex]);                    
                    anim.Play(recordingNames[_recordingIndex]);
                }
            }
        }
        Debug.Log("Recording stopped: " + recordingNames[_recordingIndex]);
        _recordingIndex++;
        _recording = false;
        return true;
    }
}