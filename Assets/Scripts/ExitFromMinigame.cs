using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitFromMinigame : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    [SerializeField]
    GameObject exitblock;

    private void OnTriggerEnter(Collider other) {
        if (other.name.Equals("HeadCollider")) {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void ChangeExitAvailibity(bool isExitBlocekd) {

        if (isExitBlocekd)
            exitblock.SetActive(true);
        else
            exitblock.SetActive(false);
    }
}
