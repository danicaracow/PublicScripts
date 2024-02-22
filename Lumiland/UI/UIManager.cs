using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text lumiesInNest_text;
    [SerializeField] private TMP_Text currentWorkers_text;
    [SerializeField] private TMP_Text currentSoldiers_text;
    [SerializeField] private TMP_Text currentHealth_text;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Actions.OnLumiDrop += UpdateLumiCount;
        Actions.OnWorkerSpawn += UpdateWorkersCount;
        Actions.OnWorkerSpawn += UpdateLumiCount;
        Actions.OnSoldierSpawn += UpdateSoldiersCount;
        Actions.OnSoldierSpawn += UpdateLumiCount;
        Actions.OnWorkerEaten += UpdateWorkersCount;
        Actions.OnNestDamaged += UpdateNestHealth;
    }

    private void Start()
    {
        UpdateLumiCount();
        UpdateNestHealth();
    }

    private void UpdateLumiCount()
    {
        lumiesInNest_text.text = PlayerWorldVariables._lumisInNest.ToString();
    }

    private void UpdateWorkersCount()
    {
        currentWorkers_text.text = PlayerWorldVariables.currentWorkersCount.ToString();
        //lumiesInNest_text.text = PlayerWorldVariables.lumisInNest.ToString();
    }

    private void UpdateSoldiersCount()
    {
        currentSoldiers_text.text = PlayerWorldVariables.currentSoldiersCount.ToString();
        //lumiesInNest_text.text = PlayerWorldVariables.lumisInNest.ToString();
    }

    private void UpdateNestHealth()
    {
        currentHealth_text.text = PlayerWorldVariables._nestHealth.ToString();
    }
}
