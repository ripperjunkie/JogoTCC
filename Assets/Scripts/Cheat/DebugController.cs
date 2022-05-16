using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=vLKeqS1PeTU (usado como referencia)

public class DebugController : MonoBehaviour
{
    private bool _showConsole;
    private string _text;
    public List<MemberInfo> commandAttributes = new List<MemberInfo>();
    private PlayerMaster _playerMaster;

    public static bool flyMode;
    public static bool godMode;

    private void Awake()
    {
        FindAndRegisterAttributes();
        _playerMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) OnToggleDebug();
    }

    private void FindAndRegisterAttributes()
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
                        if (attribute != null)
                        {
                            commandAttributes.Add(member);
                        }
                    }
                }
            }
        }
    }

#region Commands

    [CommandAttribute]
    public void Fly()
    {
        print("FlyMode");
        flyMode = !flyMode;
        if(flyMode)
        {
            //fly
            CharFly();
            return;
        }
        CharStopFly();
        
    }

    [CommandAttribute]
    public void God()
    {
        print("God");
    }

    public void OpenLevel(string _levelName)
    {
        print("Level to open " + _levelName);
    }

#endregion

    [ContextMenu("Debug")]
    public void OnToggleDebug()
    {
        _showConsole = !_showConsole;
        if (!_showConsole)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

    }

    private void OnGUI()
    {

       if(!_showConsole) { return; }

        float y = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = Color.black;
        _text = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _text);

        if (Event.KeyboardEvent("Enter") == null) return;

        if(Event.current.Equals(Event.KeyboardEvent("Enter")))
        {
            //check for string
            HandleInput(_text);
        }
    }

    private void HandleInput(string _id)
    {
        foreach (MethodInfo _item in commandAttributes)
        {
            if(_id == _item.Name)
            {
                //_item.Invoke(this, new object[] {}); //invoke method where attributes is residing 
                _item.Invoke(this, null);
                ClearConsole();
            }
            else
            {
                print("invalid command");
            }
        }

    }

    public void ClearConsole()
    {
        _text = "";
    }

    [ContextMenu("Debug Commands List")]
    public void SeeCommandsList()
    {
        foreach (MethodInfo _item in commandAttributes)
        {
            print(_item.Name);
        }
    }

    public void CharFly()
    {
        Collider collider = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (collider)
        {
            collider.isTrigger = true;

        }
        if(rb)
        {
            rb.useGravity = false;
        }
    }

    public void CharStopFly()
    {
        Collider collider = GetComponent<Collider>();
        Rigidbody rb = GetComponent<Rigidbody>();

        if (collider)
        {
            collider.isTrigger = false;
        }
        if (rb)
        {
            rb.useGravity = true;
        }
    }
}
