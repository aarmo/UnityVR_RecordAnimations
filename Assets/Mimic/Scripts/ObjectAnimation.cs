using UnityEngine;

public class ObjectAnimation
{
    public Transform transform;
    public CurveContainer[] curves;
    public string pathName = "";

    public ObjectAnimation(string hierarchyPath, Transform trans)
    {
        pathName = hierarchyPath;
        transform = trans;

        curves = new CurveContainer[7];

        curves[0] = new CurveContainer("localPosition.x");
        curves[1] = new CurveContainer("localPosition.y");
        curves[2] = new CurveContainer("localPosition.z");

        curves[3] = new CurveContainer("localRotation.x");
        curves[4] = new CurveContainer("localRotation.y");
        curves[5] = new CurveContainer("localRotation.z");
        curves[6] = new CurveContainer("localRotation.w");
    }

    public void AddFrame(float time)
    {

        curves[0].AddValue(time, transform.localPosition.x);
        curves[1].AddValue(time, transform.localPosition.y);
        curves[2].AddValue(time, transform.localPosition.z);

        curves[3].AddValue(time, transform.localRotation.x);
        curves[4].AddValue(time, transform.localRotation.y);
        curves[5].AddValue(time, transform.localRotation.z);
        curves[6].AddValue(time, transform.localRotation.w);
    }
}
