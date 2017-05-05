using UnityEngine;

public class UnityObjectPosRotAnimation
{
    public Transform transform;
    public UnityCurveContainer[] curves;
	public string pathName = "";

	public UnityObjectPosRotAnimation( string hierarchyPath, Transform trans )
    {
		pathName = hierarchyPath;
        transform = trans;
        
        curves = new UnityCurveContainer[7];

		curves [0] = new UnityCurveContainer( "localPosition.x" );
		curves [1] = new UnityCurveContainer( "localPosition.y" );
		curves [2] = new UnityCurveContainer( "localPosition.z" );

		curves [3] = new UnityCurveContainer( "localRotation.x" );
		curves [4] = new UnityCurveContainer( "localRotation.y" );
		curves [5] = new UnityCurveContainer( "localRotation.z" );
		curves [6] = new UnityCurveContainer( "localRotation.w" );
	}

	public void AddFrame ( float time )
    {

		curves [0].AddValue (time, transform.localPosition.x);
		curves [1].AddValue (time, transform.localPosition.y);
		curves [2].AddValue (time, transform.localPosition.z);

		curves [3].AddValue (time, transform.localRotation.x);
		curves [4].AddValue (time, transform.localRotation.y);
		curves [5].AddValue (time, transform.localRotation.z);
		curves [6].AddValue (time, transform.localRotation.w);
	}
}
