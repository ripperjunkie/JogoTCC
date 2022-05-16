using UnityEngine;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    public string text;
    public string[] commandsList = {"Hello", "Bye"};


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OnToggleDebug();
    }

    public void OnToggleDebug()
    {
        showConsole = !showConsole;
        if (!showConsole)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }


    private void OnGUI()
    {

       if(!showConsole) { return; }

        float y = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = Color.black;
        text = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), text);

        if (Event.KeyboardEvent("Enter") == null) return;

        if(Event.current.Equals(Event.KeyboardEvent("Enter")))
        {
            //check for string
            HandleInput(text);
        }
    }

    private void HandleInput(string _id)
    {
        foreach(var item in commandsList)
        {
            if(_id.Equals(item))
            {
                print("Valid command");
                text = "";
            }
            else
            {
                print("invalid command");
            }
        }
    }
}
