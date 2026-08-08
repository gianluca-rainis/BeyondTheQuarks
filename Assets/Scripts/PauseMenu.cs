using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public Image quarkTable;
    public InventoryUI inventoryUI;
    public GameObject firstSlotInventory;
    public Player player;

    public Sprite[] quarkSprites;

    private PlayerMovements playerMovements;

    private bool isPaused = false;

    void Awake()
    {
        SaveData data = SaveSystem.GetSavedData() ?? new SaveData();

        switch (data.currentLevel)
        {
            case 0:
                quarkTable.sprite = quarkSprites[0];
                break;
            case 1:
                quarkTable.sprite = quarkSprites[1];
                break;
            case 2:
                quarkTable.sprite = quarkSprites[2];
                break;
            case 3:
                quarkTable.sprite = quarkSprites[3];
                break;
            case 4:
                quarkTable.sprite = quarkSprites[4];
                break;
            default:
                quarkTable.sprite = quarkSprites[0];
                break;
        }

        PausePanel.SetActive(false);

        playerMovements = player.GetComponent<PlayerMovements>();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PausePanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;

        playerMovements.enableMovement = true;
    }

    public void Pause()
    {
        PausePanel.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;

        playerMovements.enableMovement = false;

        if (firstSlotInventory != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSlotInventory);
        }

        inventoryUI.Refresh();
    }

    public void SaveGame()
    {
        SaveData data = SaveSystem.GetSavedData() ?? new SaveData();

        player.SavePlayer(ref data);

        data.sceneName = SceneManager.GetActiveScene().name;

        SaveSystem.SaveGame(data);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
