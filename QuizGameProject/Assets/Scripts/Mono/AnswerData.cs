using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerData : MonoBehaviour {

    #region Variables

    [Header("UI Elements")]
    [SerializeField]    TextMeshProUGUI infoTextObject      = null;
    [SerializeField]    Image           toggle              = null;

    [Header("Textures")]
    [SerializeField]    Sprite          uncheckedToggle     = null;
    [SerializeField]    Sprite          checkedToggle       = null;

    [Header("References")]
    [SerializeField]    GameEvents      events              = null;

    private             RectTransform   _rect               = null;
    public              RectTransform   Rect
    {
        get
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return _rect;
        }
    }

    private             int             _answerIndex        = -1;
    public              int             AnswerIndex         { get { return _answerIndex; } }

    private             bool            Checked             = false;

    #endregion

    /// <summary>
    /// Function that is called to update the answer data.
    /// </summary>
    public void UpdateData (string info, int index)
    {
        infoTextObject.text = info;
        _answerIndex = index;
    }
    /// <summary>
    /// Function that is called to reset values back to default.
    /// </summary>
    public void Reset ()
    {
        Checked = false;
        UpdateUI();
    }
    /// <summary>
    /// Function that is called to switch the state.
    /// </summary>
    public void SwitchState ()
    {
        Checked = !Checked;
        UpdateUI();

        if (events.UpdateQuestionAnswer != null)
        {
            events.UpdateQuestionAnswer(this);
        }
    }
    /// <summary>
    /// Function that is called to update UI.
    /// </summary>
    void UpdateUI ()
    {
        if (toggle == null) return;

        toggle.sprite = (Checked) ? checkedToggle : uncheckedToggle;
    }
}