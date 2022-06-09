using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }

    public static void StaticLoadBattle()
    {
        SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
    }

    public void LoadTeamBuilder()
    {
        SceneManager.LoadScene("TeamBuilder", LoadSceneMode.Single);
    }

    public void LoadJSON()
    {
        SceneManager.LoadScene("JSONGenerator", LoadSceneMode.Single);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }
}
