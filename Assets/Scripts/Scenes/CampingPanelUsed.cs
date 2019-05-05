using PlayoVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class CampingPanelUsed : MonoBehaviour
{
    SceneLoadManager sceneLoader;

    private void Start()
    {
        sceneLoader = GameObject.Find("SceneLoadManager").GetComponent<SceneLoadManager>();
        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(Used);
    }

    private void Used(object sender, InteractableObjectEventArgs e)
    {
        var avatarSpawnManager = GameObject.Find("Networking").GetComponent<AvatarSpawnManager>();
        avatarSpawnManager.enabled = false;
        Debug.Log("Used camping panel");
        sceneLoader.ChangeToCampingScene();
    }
}
