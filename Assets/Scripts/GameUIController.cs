using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUIController : MonoBehaviour {

    [Header("Inventory")]
    public GameObject InventoryToolbar;
    public GameObject InventoryItemTemplate;
    public List<SOInventoryItem> InventoryItems;
    [HideInInspector]
    public GameObject InventoryHeldItem;

    [Header("Dialog")]
    public GameObject DialogToolbar;
    public GameObject DialogAvatar;
    public GameObject DialogText;
    public GameObject DialogTreeToolbar;
    public GameObject DialogTreeOptionTemplate;
    public SODialogTree CurrentDialogTree;
    public SOCutscene CurrentCutscene;
    private List<string> DialogEnabledOptions = new List<string>();
    private List<string> DialogDisabledOptions = new List<string>();

    [Header("Cursor")]
    public GameObject CursorTooltip;
    public Texture2D DefaultCursorTexture;
    public Texture2D InteractCursorTexture;
    public Texture2D TalkCursorTexture;

    public static GameUIController Instance;

    private void Awake()
    {
        Instance = this;
        CursorReset();
        CursorTooltipClearText();
        InventoryRedrawItems();
        DialogTreeClear();
        InventoryItemTemplate.SetActive(false);
        DialogTreeOptionTemplate.SetActive(false);
    }

    private void UpdateState()
    {
        if (CurrentCutscene)
        {
            DialogToolbar.SetActive(true);
            DialogTreeToolbar.SetActive(false);
            InventoryToolbar.SetActive(false);
            GameController.State = GameState.InCutscene;
        }
        else if (CurrentDialogTree)
        {
            GameController.State = GameState.InteractingWithToolbars;
            DialogToolbar.SetActive(true);
            DialogTreeToolbar.SetActive(true);
            InventoryToolbar.SetActive(false);
        }
        else
        {
            GameController.State = GameState.NavigatingScene;
            DialogToolbar.SetActive(false);
            DialogTreeToolbar.SetActive(false);
            InventoryToolbar.SetActive(InventoryItems.Count > 0);
        }
    }

    //INVENTORY

    private void InventoryRedrawItems()
    {
        UpdateState();

        foreach (Transform t in InventoryToolbar.transform)
        {
            if (t != InventoryItemTemplate.transform)
            {
                Destroy(t.gameObject);
            }
        }

        float margin = 16;
        float x = margin;
        foreach (SOInventoryItem i in InventoryItems) {
            Vector3 vector = InventoryItemTemplate.transform.position;
            vector.x = x;
            GameObject item = Instantiate(InventoryItemTemplate, vector, Quaternion.identity);
            item.transform.SetParent(InventoryToolbar.transform);
            item.name = i.name;
            item.GetComponent<Image>().overrideSprite = i.icon;
            item.SetActive(true);
            x += item.GetComponent<RectTransform>().sizeDelta.x + margin;
        }
    }

    public void InventoryResetHeldItem()
    {
        InventoryHeldItem.GetComponent<Image>().raycastTarget = true;
        InventoryHeldItem = null;
        InventoryRedrawItems();
    }

    public void InventoryRemoveItem(string itemName)
    {
        InventoryItems.RemoveAll(i => i.name.Equals(itemName));
        InventoryRedrawItems();
    }

    public void InventoryAddItem(string itemName)
    {
        if (!InventoryHasItemNamed(itemName))
        {
            InventoryItems.Add(SOInventoryItem.Load(itemName));
            InventoryRedrawItems();
        }
    }

    public bool InventoryIsHoldingItemNamed(string itemName)
    {
        return InventoryHeldItem && InventoryHeldItem.name.Equals(itemName);
    }

    public bool InventoryHasItemNamed(string itemName)
    {
        return InventoryItems.Exists(i => i.name.Equals(itemName));
    }

    public void InventoryObjectMouseClick(GameObject inventoryObject)
    {
        if (GameController.State != GameState.NavigatingScene)
            return;

        if (InventoryHeldItem)
        {
            Debug.Log("Combining items " + InventoryHeldItem.name + " & " + inventoryObject.name);
            return;
        }
        InventoryHeldItem = inventoryObject;
        InventoryHeldItem.GetComponent<Image>().raycastTarget = false;
    }

    public void InventoryObjectMouseEnter(GameObject inventoryObject)
    {
        if (GameController.State != GameState.NavigatingScene)
            return;

        CursorTooltipSetText(inventoryObject.name);
        CursorSet(InteractCursorTexture);
    }

    public void InventoryObjectMouseExit(GameObject inventoryObject)
    {
        if (GameController.State != GameState.NavigatingScene)
            return;

        CursorTooltipClearText();
    }

    //SCENE

    public void SceneObjectMouseEnter(SceneObjectController sceneObject)
    {
        if (GameController.State != GameState.NavigatingScene)
            return;

        //TODO Handle InteractableObject vs ObservableObject
        CursorTooltipSetText(sceneObject.tooltipText);
        if (sceneObject.type == SceneObjectType.Character)
        {
            CursorSet(TalkCursorTexture);
        } else
        {
            CursorSet(InteractCursorTexture);
        }
    }

    public void SceneObjectMouseExit(SceneObjectController sceneObject)
    {
        if (GameController.State != GameState.NavigatingScene)
            return;

        CursorTooltipClearText();
        CursorReset();
    }

    //CURSOR

    public void CursorTooltipSetText(string tooltip)
    {
        CursorTooltip.SetActive(true);
        CursorTooltip.GetComponent<Text>().text = tooltip;
    }

    public void CursorTooltipClearText()
    {
        CursorTooltip.SetActive(false);
    }

    public void CursorSet(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

    public void CursorReset()
    {
        Cursor.SetCursor(DefaultCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    //CUTSCENES

    public void CutsceneStart(SOCutscene cutscene)
    {
        InventoryToolbar.SetActive(false);
        CurrentCutscene = cutscene;
        StartCoroutine(QueueCutscene(cutscene));
        UpdateState();
    }

    private void CutsceneEndCurrent()
    {
        CurrentCutscene = null;
        UpdateState();
    }

    private IEnumerator QueueCutscene(SOCutscene cutscene)
    {
        foreach (CutsceneEntry entry in cutscene.Entries)
        {
            float entryDuration = entry.AudioClip ? entry.AudioClip.length + entry.ExtraDuration : entry.ExtraDuration;
            CutsceneSetDialog(entry);
            if (entry.Callback != null)
            {
                entry.Callback.Invoke();
            }
            // Manually wait for entryDuration seconds to support cancelling
            float remainingTimeDuration = entryDuration;
            while (remainingTimeDuration > 0)
            {
                remainingTimeDuration -= Time.deltaTime;
                if (entryDuration - remainingTimeDuration > 0.3 && Input.GetMouseButtonUp(0))
                {
                    GetComponent<AudioSource>().Stop();
                    break;
                }
                // Yield to return out of the Ienumeration
                yield return 0;
            }
            // Always clear up and hide dialog
            CutsceneClearDialog();
            DialogToolbar.SetActive(false);
        }
        CutsceneEndCurrent();
        if (cutscene.DialogTree)
        {
            DialogTreeDisplay(cutscene.DialogTree);
        }
        yield break;
    }

    private void CutsceneSetDialog(CutsceneEntry entry)
    {
        if (entry.Character) {
            DialogToolbar.SetActive(true);
            DialogText.GetComponent<Text>().text = entry.Text;
            if (!string.IsNullOrEmpty(entry.Text) && entry.Character.TalkAnimationClip) {
                DialogAvatar.GetComponent<Animator>().Play("Base Layer." + entry.Character.TalkAnimationClip.name);
            } else if (string.IsNullOrEmpty(entry.Text) && entry.Character.TalkAnimationClip)
            {
                    DialogAvatar.GetComponent<Animator>().Play("Base Layer." + entry.Character.IdleAnimationClip.name);
            } else
            {
                DialogAvatar.GetComponent<Image>().overrideSprite = entry.Character.avatar;
            }
            DialogText.GetComponent<Text>().color = entry.Character.textColor;
        }
        if (entry.AudioClip)
            GetComponent<AudioSource>().PlayOneShot(entry.AudioClip);
    }

    private void CutsceneClearDialog()
    {
        DialogText.GetComponent<Text>().text = "";
    }

    // DIALOG TREES

    public void DialogTreeDisplay(SODialogTree dialogTree)
    {
        CurrentDialogTree = dialogTree;
        DialogTreeRedraw();

        if (dialogTree.Character)
            DialogSetAvatar(dialogTree.Character);
    }

    public void DialogTreeClear()
    {
        CurrentDialogTree = null;
        DialogTreeRedraw();
    }

    public void DialogTreeRedraw()
    {
        UpdateState();

        foreach (Transform t in DialogTreeToolbar.transform)
        {
            if (t != DialogTreeOptionTemplate.transform)
            {
                Destroy(t.gameObject);
            }
        }

        if (!CurrentDialogTree)
        {
            return;
        }

        float margin = 0;
        float y = margin;
        int i = 0;
        foreach (DialogTreeOption o in CurrentDialogTree.Options)
        {
            if (DialogShouldIncludeOption(o))
            {
                Vector3 vector = DialogTreeOptionTemplate.transform.position;
                vector.y = y;
                GameObject option = Instantiate(DialogTreeOptionTemplate, vector, Quaternion.identity);
                option.GetComponent<Text>().text = o.Text;
                option.name = o.Text;
                option.GetComponent<GameUIObjectController>().index = i;
                y += option.GetComponent<RectTransform>().rect.height;
                option.transform.SetParent(DialogTreeToolbar.transform, true);
                option.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                option.SetActive(true);
            }
            i++;
        }
    }

    public void DialogTreeMouseClick(GameObject dialogTreeOption)
    {
        if (!CurrentDialogTree)
        {
            return;
        }

        int index = dialogTreeOption.GetComponent<GameUIObjectController>().index;
        DialogTreeOption option = CurrentDialogTree.Options[index];

        DialogTreeClear();

        if (option.Cutscene)
        {
            CutsceneStart(option.Cutscene);
        }
        else if (option.DialogTree)
        {
            DialogTreeDisplay(option.DialogTree);
        }
    }

    public void DialogTreeMouseEnter(GameObject dialogTreeOption)
    {
        dialogTreeOption.GetComponent<Text>().color = new Color(1, 1, 0.2f);
    }

    public void DialogTreeMouseExit(GameObject dialogTreeOption)
    {
        dialogTreeOption.GetComponent<Text>().color = new Color(1, 1, 1);
    }

    private bool DialogShouldIncludeOption(DialogTreeOption option)
    {
        if ((option.DisabledByDefault && !DialogEnabledOptions.Contains(option.tag)) || DialogDisabledOptions.Contains(option.tag))
        {
            return false;
        }
        return true;
    }

    public void DialogEnableOption(string tag = "")
    {
        if (DialogDisabledOptions.Contains(tag))
            DialogDisabledOptions.Remove(tag);

        if (!DialogEnabledOptions.Contains(tag))
            DialogEnabledOptions.Add(tag);
    }

    public void DialogDisableOption(string tag = "")
    {
        if (DialogEnabledOptions.Contains(tag))
            DialogEnabledOptions.Remove(tag);

        if (!DialogDisabledOptions.Contains(tag))
            DialogDisabledOptions.Add(tag);
    }

    private void DialogSetAvatar(SOCharacter character)
    {
        DialogToolbar.SetActive(true);
        if (character.IdleAnimationClip)
        {
            DialogAvatar.GetComponent<Animator>().Play("Base Layer." + character.IdleAnimationClip.name);
        }
        else
        {
            DialogAvatar.GetComponent<Image>().overrideSprite = character.avatar;
        }
    }

    //UPDATES

    private void Update()
    {
        if (GameController.State == GameState.InCutscene && Cursor.visible)
        {
            Cursor.visible = false;
        }
        else if (GameController.State != GameState.InCutscene && !Cursor.visible)
        {
            Cursor.visible = true;
        }

        if (GameController.State == GameState.InCutscene && CursorTooltip.activeSelf)
        {
            CursorTooltipClearText();
            CursorReset();
            return;
        }

        if (InventoryHeldItem)
        {
            if (Input.GetMouseButton(1)) 
            {
                InventoryResetHeldItem();
            } else
            {
                Vector3 heldInventoryItemVector = Input.mousePosition;
                heldInventoryItemVector.z = InventoryHeldItem.transform.position.z;
                InventoryHeldItem.transform.position = heldInventoryItemVector;
            }
        }

        if (CursorTooltip.activeSelf)
        {
            Vector3 tooltipVector = Input.mousePosition;
            tooltipVector.z = CursorTooltip.transform.position.z;
            CursorTooltip.transform.position = tooltipVector;
        }
    }
}

