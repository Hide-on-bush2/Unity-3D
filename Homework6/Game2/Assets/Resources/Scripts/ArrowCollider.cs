using Scene;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollider : MonoBehaviour {
    private ISceneController scene;

    void Start () {
        scene = SceneController.getInstance() as ISceneController;
	}
	
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "target")
        {
            gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.SetActive(false);
            int points = other.gameObject.name[other.gameObject.name.Length - 1] - '0';
            scene.addScore(points);
            scene.showTips(points);
            
        }
    }
}
