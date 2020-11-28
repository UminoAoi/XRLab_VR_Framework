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
            GameData.FinishGame();

            if (GameData.FinishedAllGames())
                SceneManager.LoadScene("R_001_Finish");
            else
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
