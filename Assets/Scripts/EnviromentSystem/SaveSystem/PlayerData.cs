using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    #region Fields

    [SerializeField] int level = 0;
    [SerializeField] int coin = 0;
    [SerializeField] float energy = 0;
    [SerializeField] float hp = 0;
    [SerializeField] float attack = 0;
    [SerializeField] bool doubleGrappling = false;
    [SerializeField] bool firetoPlayer = false;
    [SerializeField] bool grappleEnemy = false;
    public static bool ifsaved;

    public static bool enterScene;

    private Transform transitionpoint;

    [System.Serializable]
    class SaveData
    {
        public int playerLevel;
        
        public int playerCoin;
        
        public float playerEnergy;
        public float playerHP;
        public float playerAttack;

        public bool playerDoubleGrappling;
        public bool playerFiretoPlayer;
        public bool playerGrappleEnemy;


        public Vector3 playerPosition;

        public bool playerIfSaved;
    }

    const string PLAYER_DATA_KEY = "PlayerData";
    const string PLAYER_DATA_FILE_NAME = "PlayerData.sav";
    const string PLAYER_SHAKINGLIGHT_DATA_FILE_NAME = "ShakingLightData.sav";
    const string PLAYER_PLAYERSTATE_DATA_FILE_NAME = "PlayerStateData.sav";
    #endregion
    private void Start()
    {
        coin = 104;
        //Load_ShakingLight();
        if (ifsaved)
        {
            ifsaved = false;
            Load_ShakingLight();
            Load();
            CoinUI.CurrentCoinQuantity = coin;
        }
        if(!TransitionPoint.changedScene && ifsaved)
        {
            Load();
        }
    }
    private void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            level = SceneManager.GetActiveScene().buildIndex;
            coin = CoinUI.CurrentCoinQuantity;
            attack = PlayerAttack.temp_damage;
            hp = PlayerHealth.temp_health;
            energy = PlayerEnegy.temp_energy;
            doubleGrappling = GrapplingGun1.canDoubleGrappling;
        }
    }
    #region Properties

    public int Level => level;

    public int Coin => coin;

    public float Attack => attack;
    public float HP => hp;
    public float Energy => energy;

    public bool DoubleGrapple => doubleGrappling;
    public bool FireToPlayer => firetoPlayer;
    public bool GrappleEnemy => grappleEnemy;

    public Vector3 Position => transform.position;

    public bool IfSaved => ifsaved;
    #endregion

    #region Save and Load

    public void Save()
    {
        // SaveByPlayerPrefs();
        SaveByJson();
    }
    public void Save_PLayerState()
    {
        SaveByJson_PlayerState();
    }

    public void Save_ShakingLight()
    {
        SaveByJson_ShakingLight();
    }

    public void Load()
    {
        // LoadFromPlayerPrefs();
        LoadFromJson();
    }
    public void Load_PLayerState()
    {
        LoadByJson_PlayerState();
    }

    public void Load_ShakingLight()
    {
        LoadFromJson_ShakingLight();
    }

    #endregion

    #region PlayerPrefs

    void SaveByPlayerPrefs()
    {
        SaveSystem.SaveByPlayerPrefs(PLAYER_DATA_KEY, SavingData());
    }

    void LoadFromPlayerPrefs()
    {
        var json = SaveSystem.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        var saveData = JsonUtility.FromJson<SaveData>(json);
        LoadData(saveData);
    }

    #endregion

    #region JSON

    void SaveByJson()
    {
        SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, SavingData());
        // SaveSystem.SaveByJson($"{System.DateTime.Now:yyyy.dd.M HH-mm-ss}.sav", SavingData());
    }

    void SaveByJson_ShakingLight()
    {
        SaveSystem.SaveByJson(PLAYER_SHAKINGLIGHT_DATA_FILE_NAME, SavingData_ShakingLight());
        // SaveSystem.SaveByJson($"{System.DateTime.Now:yyyy.dd.M HH-mm-ss}.sav", SavingData());
    }

    void SaveByJson_PlayerState()
    {
        SaveSystem.SaveByJson(PLAYER_PLAYERSTATE_DATA_FILE_NAME, SavingData_State());
    }

    void LoadFromJson()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_DATA_FILE_NAME);

        LoadData(saveData);
    }

    void LoadFromJson_ShakingLight()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_SHAKINGLIGHT_DATA_FILE_NAME);

        LoadData_ShakingLight(saveData);
    }

    void LoadByJson_PlayerState()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(PLAYER_PLAYERSTATE_DATA_FILE_NAME);

        LoadData_State(saveData);
    }
    #endregion

    #region Help Functions

    SaveData SavingData()
    {
        var saveData = new SaveData();

        saveData.playerLevel = level;
        saveData.playerCoin = coin;
        saveData.playerPosition = transform.position;
        saveData.playerIfSaved = ifsaved;
        return saveData;
    }

    SaveData SavingData_State()
    {
        var saveData = new SaveData();

        saveData.playerCoin = coin;
        saveData.playerEnergy = energy;
        saveData.playerHP = hp;
        saveData.playerAttack = attack;
        saveData.playerDoubleGrappling = doubleGrappling;
        saveData.playerFiretoPlayer = firetoPlayer;
        saveData.playerGrappleEnemy = grappleEnemy;
        return saveData;
    }

    SaveData SavingData_ShakingLight()
    {
        var saveData = new SaveData();

        saveData.playerCoin = coin;
        return saveData;
    }
    

    void LoadData(SaveData saveData)
    {
        level = saveData.playerLevel;
        coin = saveData.playerCoin;
        transform.position = saveData.playerPosition;
        ifsaved = saveData.playerIfSaved;
    }

    void LoadData_State(SaveData saveData)
    {
        coin = saveData.playerCoin;
        energy = saveData.playerEnergy;
        hp = saveData.playerHP;
        attack = saveData.playerAttack;
        doubleGrappling = saveData.playerDoubleGrappling;
        firetoPlayer = saveData.playerFiretoPlayer;
        grappleEnemy = saveData.playerGrappleEnemy;
    }

    void LoadData_ShakingLight(SaveData saveData)
    {
        coin = saveData.playerCoin;
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Developer/Delete Player Data Prefs")]
    public static void DeletePlayerDataPrefs()
    {
        PlayerPrefs.DeleteKey(PLAYER_DATA_KEY);
    }

    [UnityEditor.MenuItem("Developer/Delete Player Data Save File")]
    public static void DeletePlayerDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    }
    #endif

    #endregion
}