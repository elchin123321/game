using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public InputField num1;
    public InputField num2;
    public Text answer;
    private AndroidJavaClass pluginClass;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void usePlugin()
    {
        pluginClass = new AndroidJavaClass("com.example.unityplugin.MyPlugin");
        if (pluginClass != null) 
        {          
            int ans = pluginClass.CallStatic<int>("sum", Int32.Parse(num1.text), Int32.Parse(num2.text));
            answer.text = ans.ToString();
        };
    }
}