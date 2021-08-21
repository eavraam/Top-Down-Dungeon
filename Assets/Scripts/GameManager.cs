using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> expTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int gold;
    public int experience;

    // AWAKE Function
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

    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // Is the Weapon Max Level?
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
        
    }


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
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);

        /* Can also be done with a Struct/Class that has all the needed info,
         * then JsonUtility.ToJson(..mentioned Class instance..),
         * and finally Save with PlayerPrefs(JSON). (As shown below in example)
         */
        //PlayerData _playerData = new PlayerDate();
        //void WritePlayerDataWithJson()
        //{
        //    var jsonData = JsonUtility.ToJson(_playerData);
        //    PlayerPrefs.SetString("PlayerData", jsonData);
        //}

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

        /*
         * In case of JSON save, we load using FromJson to get a Json,
         * and save it in a list. (As shown below in example)
         * 
         * REMINDER: Read- and WritePlayerDataWithJson() should be inside the same
         * class and must be able to see _playerData instance.
         */
        //void ReadPlayerDataWithJson()
        //{
        //    var jsonFromPrefs = PlayerPrefs.GetString("PlayerData");
        //    _playerData = JsonUtility.FromJson<PlayerData>(jsonFromPrefs);
        //}

        //Change Player Skin
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.weaponLevel = int.Parse(data[3]);

        Debug.Log("Load State.");
    }
}
