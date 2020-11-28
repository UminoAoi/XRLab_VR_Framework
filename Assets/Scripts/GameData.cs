using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    static int finishedGames = 0;
    static int numberForFinish = 4;

    public static void FinishGame() {
        finishedGames++;
    }

    public static bool FinishedAllGames() {
        return finishedGames >= numberForFinish;
    }

}
