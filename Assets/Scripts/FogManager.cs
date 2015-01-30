using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogManager : AgentBasic {

	public User user;

	private static float DISTANCE = 5f;
	private bool hidden = false;
	private List<SkinnedMeshRenderer> skins = null;
	public bool PRINT = false;

	public void Awake() {
		// getMeshes(); // not to use with default gameobjects
	}

	public void Update() {
		float tmpDist = Vector2.Distance(user.getPosition2D(), getPosition2D());
		myDebugLog("hidden: " + hidden + " tmpDist: " + tmpDist);
		if (hidden && tmpDist < DISTANCE) {
			myDebugLog("calling unHide");
			unHide();
		} else if (!hidden && tmpDist > DISTANCE) {
			myDebugLog("calling hide");
			hide();
		}
	}

	private void myDebugLog(string s) {
		if (PRINT) {
			Debug.Log(s);
		}
	}

	public void hide() {
		hidden = true;

        if (skins != null) {
            showMySkins(false);
            return;
        }

		try {
			renderer.enabled = false;
		} catch (MissingComponentException) {
			myDebugLog("Renderer not found, using my method to hide the agent.");
			transform.position = new Vector3(getPosition2D().x, -2.5f, getPosition2D().y);
		}			
	}

	public void unHide() {
		hidden = false;

        if (skins != null)
        {
            showMySkins(true);
            return;
        }
        
        try {
			renderer.enabled = true;
		} catch (MissingComponentException) {
			myDebugLog("Renderer not found, using my method to unHide the agent.");
			transform.position = new Vector3(getPosition2D().x, 0.000000001f, getPosition2D().y);
		}
	}

	private void getMeshes() {
		skins = new List<SkinnedMeshRenderer>();

		LODGroup lodGroup = GetComponent<LODGroup>();
		SkinnedMeshRenderer skin;

		if (null != lodGroup)
		{
			Transform lodTransform = lodGroup.transform;
			foreach (Transform child in lodTransform)
			{
				skin = child.GetComponentInChildren<SkinnedMeshRenderer>();
				if(skin != null)
				{
					skins.Add(skin);
				}
			}
		}
	}

	private void showMySkins(bool b) {
		foreach (SkinnedMeshRenderer skin in skins) {
			skin.enabled = b;
		}
	}
}
