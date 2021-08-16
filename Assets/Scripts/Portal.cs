using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{

    public string[] sceneNames;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name == "Player")
        {
            //Teleport the Player
            GameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);
        }
    }
}
