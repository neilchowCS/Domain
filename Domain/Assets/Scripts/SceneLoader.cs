using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


static class SceneLoader
{
    public static void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }

    public static void LoadTeamBuilder()
    {
        SceneManager.LoadScene("TeamBuilder", LoadSceneMode.Single);
    }

    public static void LoadJSON()
    {
        SceneManager.LoadScene("JSONGenerator", LoadSceneMode.Single);
    }

    public static void LoadCollection()
    {
        SceneManager.LoadScene("CollectionManager", LoadSceneMode.Single);
    }
}
