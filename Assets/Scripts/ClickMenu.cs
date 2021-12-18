using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ClickMenu : MonoBehaviour
{
    private bool click = false;
    public void TaskOnClick()
    {
        click = !click;
    }
}
