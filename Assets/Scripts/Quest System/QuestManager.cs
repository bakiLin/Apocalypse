using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestManager : MonoBehaviour
{
    [Inject]
    private SaveManager saveManager;

    [SerializeField]
    private Toggle[] toggles;

    private void OnEnable()
    {
        saveManager.OnLoadQuestCompletion += LoadQuests;
    }

    private void OnDisable()
    {
        saveManager.OnLoadQuestCompletion -= LoadQuests;
    }

    private void LoadQuests(bool[] value)
    {
        for (int i = 0; i < toggles.Length; i++) 
            toggles[i].isOn = value[i];
    }

    public void CompleteQuest(int id)
    {
        toggles[id].isOn = true;
        saveManager.SetQuestCompletion(id);
    }
}
