using UnityEngine;
using System.Collections;

public class LineObject : MonoBehaviour {

	private float deltaTime = 0.0f;
	private float touchInterval = 0.01f;
	private bool touchEnable = true;

	public float deltaMin = 0.1f;

//	public GameObject lineObject;
	private LineRenderer lineRenderer;
	private int lineIndex = 1;

	private bool controll = true;

	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		Debug.Log(lineRenderer);
		lineRenderer.enabled = false;
	}

	void Update () {
		if (Input.GetMouseButton(0) && touchEnable && controll) {
			touchEnable = false;
			touch();
		}

		if (Input.GetMouseButtonUp(0)) {
			controll = false;
		}

		deltaTime += Time.deltaTime;
		if (deltaTime > touchInterval) {
			deltaTime = 0;
			touchEnable = true;
		}
	}

	Vector3 lastPoint;
	void touch() {
		var screenPoint = Input.mousePosition;
		screenPoint.z = 10.0f;
		var worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);
		Debug.Log(worldPoint);

		lineRenderer.enabled = true;
		if (lineIndex < 2) {
			lineRenderer.SetVertexCount(lineIndex);
			lineRenderer.SetPosition(lineIndex-1, worldPoint);
			lineIndex++;
		} else {
			Vector3 direct = worldPoint - lastPoint;
			if (direct.magnitude > deltaMin) {
				int nSegAdd = 1 + (int) (direct.magnitude / deltaMin);
				float seg = direct.magnitude / ((float)nSegAdd);
				for (int i = 1; i < nSegAdd; i++) {
					lineRenderer.SetVertexCount(lineIndex);
					lineRenderer.SetPosition(lineIndex-1, lastPoint + direct.normalized * seg * i);
					lineIndex++;
				}
			}

			lineRenderer.SetVertexCount(lineIndex);
			lineRenderer.SetPosition(lineIndex-1, worldPoint);
			lineIndex++;
		}
		lastPoint = worldPoint;
	}
}
