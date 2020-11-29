using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportTo : MonoBehaviour {
    [SerializeField]
    string sceneName;

    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("HeadCollider")) {

            SceneManager.LoadScene(sceneName);

        }
    }
}
