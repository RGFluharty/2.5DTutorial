using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform[] partySpawnPoints;
    [SerializeField] private Transform[] enemySpawnPoints;

    [Header("Battlers")]
    [SerializeField] private List<BattleEntities> allBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> enemyBattlers = new List<BattleEntities>();
    [SerializeField] private List<BattleEntities> playerBattlers = new List<BattleEntities>();

    [Header("UI")]
    [SerializeField] private GameObject[] enemySelectionButtons;
    [SerializeField] private GameObject battleMenu;
    [SerializeField] private GameObject enemySelectionMenu;
    [SerializeField] private TextMeshProUGUI actionText;

    private PartyManager partyManager;
    private EnemyManager enemyManager;
    private int currentPlayer;

    private const string ACTION_MESSAGE = "'s Action:";

    // Start is called before the first frame update
    void Start()
    {
        partyManager = GameObject.FindFirstObjectByType<PartyManager>();
        enemyManager = GameObject.FindFirstObjectByType<EnemyManager>();

        CreatePartyEntities();
        CreateEnemyEntities();
        ShowBattleMenu();
    }

    private void CreatePartyEntities()
    {
        List<PartyMember> currentParty = new List<PartyMember>();
        currentParty = partyManager.GetCurrentParty();

        for (int i = 0; i < currentParty.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();
            tempEntity.SetEntityValues(currentParty[i].MemberName, currentParty[i].CurrentHealth, currentParty[i].MaxHealth,
             currentParty[i].Init, currentParty[i].Str, currentParty[i].Level, true);

            BattleVisuals tempBattleVisuals = Instantiate(currentParty[i].MemberBattleVisualPrefab,
             partySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();

            tempBattleVisuals.SetStartingValues(currentParty[i].MaxHealth, currentParty[i].MaxHealth, currentParty[i].Level);
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            playerBattlers.Add(tempEntity);
        }
    }

    private void CreateEnemyEntities()
    {
        List<Enemy> currentEnemies = new List<Enemy>();
        currentEnemies = enemyManager.GetCurrentEnemies();

        for (int i = 0; i < currentEnemies.Count; i++)
        {
            BattleEntities tempEntity = new BattleEntities();

            tempEntity.SetEntityValues(currentEnemies[i].EnemyName, currentEnemies[i].CurrentHealth, currentEnemies[i].MaxHealth,
             currentEnemies[i].Init, currentEnemies[i].Str, currentEnemies[i].Level, false);

            BattleVisuals tempBattleVisuals = Instantiate(currentEnemies[i].EnemyVisualPrefab,
             enemySpawnPoints[i].position, Quaternion.identity).GetComponent<BattleVisuals>();

            tempBattleVisuals.SetStartingValues(currentEnemies[i].MaxHealth, currentEnemies[i].MaxHealth, currentEnemies[i].Level);
            tempEntity.BattleVisuals = tempBattleVisuals;

            allBattlers.Add(tempEntity);
            enemyBattlers.Add(tempEntity);
        }
    }

    public void ShowBattleMenu()
    {
        actionText.text = playerBattlers[currentPlayer].Name + ACTION_MESSAGE;
        battleMenu.SetActive(true);
    }

    public void ShowEnemySelectionMenu()
    {
        battleMenu.SetActive(false);
        SetEnemySelectionButtons();
        enemySelectionMenu.SetActive(true);
    }

    private void SetEnemySelectionButtons()
    {
        for (int i = 0; i < enemySelectionButtons.Length; i++)
        {
            enemySelectionButtons[i].SetActive(false);
        }
        for (int j = 0; j < enemyBattlers.Count; j++)
        {
            enemySelectionButtons[j].SetActive(true);
            enemySelectionButtons[j].GetComponentInChildren<TextMeshProUGUI>().text = enemyBattlers[j].Name;
        }
    }

}

[System.Serializable]   
public class BattleEntities
{
    public string Name;
    public int CurrentHealth;
    public int MaxHealth;
    public int Init;
    public int Str;
    public int Level;
    public bool IsPlayer;
    public BattleVisuals BattleVisuals;

    public void SetEntityValues(string name, int currentHealth, int maxHealth, int init, int str, int level, bool isPlayer)
    {
        Name = name;
        CurrentHealth = currentHealth;
        MaxHealth = maxHealth;
        Init = init;
        Str = str;
        Level = level;
        IsPlayer = isPlayer;
    }
}
