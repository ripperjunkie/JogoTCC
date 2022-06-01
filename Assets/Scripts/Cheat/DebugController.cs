using UnityEngine;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

//https://www.youtube.com/watch?v=vLKeqS1PeTU (usado como referencia)

public class DebugController : MonoBehaviour
{
    private bool _showConsole;
    private string _text;
    public List<MemberInfo> commandAttributes = new List<MemberInfo>();
    private PlayerMaster _playerMaster;

    public float _time = 1f;

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
        _playerMaster.godMode = !_playerMaster.godMode;
    }

    [CommandAttribute]
    public void OpenLevel(string _levelName)
    {
        //print("Level to open " + _levelName);
        SceneManager.LoadScene(_levelName);
    }

    [CommandAttribute]
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }

    [CommandAttribute]
    public void Slomo(float _timeSpeed)
    {
        Time.timeScale = _timeSpeed;
    }

    [CommandAttribute]
    public void DeleteSave()
    {
        File.Delete(Application.persistentDataPath + "/gamesave.save");
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
            string id = _id;
            if(_id.Contains(' '))
            {
                id = _id.Remove(_id.IndexOf(' '));
            }
            if (id == _item.Name)
            {
                string param = "";
                int number = 0;
                if (_id.Contains(' '))
                {
                    if (_item.GetParameters().Length > 0)
                    {
                        //Isso só irá funcionar para comandos com apenas 1 parametro mas mt provavelmente
                        //não teremos comandos com múltiplos parametros
                        if (_item.GetParameters()[0].ParameterType == typeof(float))
                        {
                            number = Convert.ToInt32(_id.Remove(0, _id.IndexOf(' ')));

                        }
                        else if(_item.GetParameters()[0].ParameterType == typeof(string))
                        {
                            param = Convert.ToString(_id.Remove(0, _id.IndexOf(' ')));
                        }

                    }
                }

                if (_item.GetParameters().Length > 0)
                {
                    if (_item.GetParameters()[0].ParameterType == typeof(float))
                    {
                        _item.Invoke(this, new object[] { number });
                    }
                    else if(_item.GetParameters()[0].ParameterType == typeof(string))
                    {
                        _item.Invoke(this, new object[] { param });

                    }
                }
                else
                {
                    _item.Invoke(this, null);
                }
                
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
