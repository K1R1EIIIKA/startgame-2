using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	public float curveStrength = 0.01f;

    private int _curveStrengthID;

    private void OnEnable()
    {
        _curveStrengthID = Shader.PropertyToID("_CurveStrength");
    }

	void Update()
	{
		Shader.SetGlobalFloat(_curveStrengthID, curveStrength);
	}
}
