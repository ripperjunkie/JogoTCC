using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class DebugController : MonoBehaviour
{


    bool showConsole;
    public string text;
    public string[] commandsList = {"Hello", "Bye"};

    public List<MemberInfo> commandAttributes = new List<MemberInfo>();

    private void Awake()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assembly in assemblies)
        {
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
                MemberInfo[] memberInfo = type.GetMethods(flags);

                foreach (MethodInfo member in memberInfo)
                {
                    if (member.CustomAttributes.ToArray().Length > 0)
                    {
                        CommandAttribute attribute = member.GetCustomAttribute<CommandAttribute>();
                        if(attribute != null)
                        {
                           // member.Invoke(attribute, null);
                            commandAttributes.Add(member);
                        }
                    }
                }
            }
        }

        foreach(MethodInfo item in commandAttributes)
        {
           print("Name: " + item.Name);
            item.Invoke(this, new object[] { 100});
            //print("Module: " + item.Module);
            //print("MemberType: " + item.MemberType);
            //print("MetadataToken: " + item.MetadataToken);
            //print("CustomAttributes: " + item.CustomAttributes);
            //print("DeclaringType: " + item.DeclaringType);
        }
    }

    [ContextMenu("Dummy Test")]
    public void DummyTest()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OnToggleDebug();
    }

    [CommandAttribute]
    public void TestCommand(int health = 0)
    {
        print("TestCommand" + " health: " + health);
    }

    [ContextMenu("Debug")]
    public void OnToggleDebug()
    {
     //   print("Debug");
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
