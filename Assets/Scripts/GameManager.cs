using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        /* If I run the code below and accidentally Load the current
         * Scene I am already in, the DontDestroyOnLoad gameObject
         * will create a duplicate and cause errors / bugs.
         * 
         * + Good Practice -> First Load a Loading Screen/Scene
         *   so that it has no duplicate like it to go back on.
         *   
         * ~ Meh Practice -> First check if I already have a 
         *   DontDestroyOnLoad gameObject. If yes, destroy itself.
         *   */

        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }



        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> expTable;

    //References
    public Player player;
    //public Weapon weapon...

    //Logic
    public int gold;
    public int experience;

    //Save State
    /*
     * INT preferedSkin
     * INT gold
     * INT experience
     * INT weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";


        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Save State.");
    }

    //Load State
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }


        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change Player Skin
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        //Change Weapon Level

        Debug.Log("Load State.");
    }
}
