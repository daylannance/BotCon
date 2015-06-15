using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Threading;
using BotCon;

/// <summary>
/// A simple python behaviour that demonstrates how code can be ran in the runtime engine
/// </summary>
public class IGPI : MonoBehaviour 
{
	public static IGPI interpreter;
	public InputField consoleInput;
	public Text consoleOutput;
	public InputField scriptBodyField;
	public InputField scriptNameField;
    private string m_pyCode;
    private string m_pyOutput;

    private PythonEnvironment m_pyEnv;

    private const string INITIALIZATION_CODE =
@"import clr
clr.AddReference('UnityEngine')
clr.AddReference('BotCon')
import UnityEngine
import BotCon
from UnityEngine import *
from BotCon import *
list = GameObject.FindSceneObjectsOfType(Test)
print list";

	// Use this for initialization
	void Start () 
    {
		interpreter = this;
        m_pyEnv = new PythonEnvironment();
		m_pyEnv.igpi = this;
		m_pyOutput = string.Empty;
		PythonEnvironment.CommandResult result = m_pyEnv.RunCommand(INITIALIZATION_CODE);
		
		if (!string.IsNullOrEmpty(result.output))
		{
			m_pyOutput += "Python output : " + result.output + System.Environment.NewLine;
		}
        m_pyCode = "for i in xrange(5):\n  g = UnityEngine.GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Sphere)\n  g.name = 'PySphere' + str(i)\nprint 'Created 5 spheres'\nclr.AddReference('UnityEngine')\nclr.AddReference('DLLProjectForIronPython')\nimport UnityEngine\nimport IronPythonTest";
	}
	public void SaveScript()
	{
		if(!string.IsNullOrEmpty(scriptNameField.text))
		{
			try
			{
				string script_path = Path.Combine(Application.persistentDataPath, scriptNameField.text);
				script_path = Path.ChangeExtension(script_path, ".py");
				File.WriteAllText(script_path, scriptBodyField.text);
				if(File.Exists(script_path))
				{
					consoleOutput.text = "Saved to " + script_path;
				}
			}
			catch(System.Exception e)
			{
				consoleOutput.text = e.Message;	
			}
		}
	}
	public void SetPyOutput(PythonEnvironment.CommandResult result)
	{
		if (!string.IsNullOrEmpty(result.output))
		{
			m_pyOutput += "Python output : " + result.output + System.Environment.NewLine;
		}
		if (result.exception != null)
		{
			m_pyOutput += "Python exception : " + result.exception.Message;
		}
		consoleOutput.text = m_pyOutput;
	}
	public void LoadScript()
	{
		if(!string.IsNullOrEmpty(scriptNameField.text))
		{
			string script_path = Path.Combine(Application.persistentDataPath, scriptNameField.text);
			script_path = Path.ChangeExtension(script_path, ".py");
			if(File.Exists(script_path))
			{
				consoleOutput.text = "Loaded " + script_path;
				scriptBodyField.text = File.ReadAllText(script_path);
			}
			else {
				consoleOutput.text = script_path + " does not exist";
			}	
		}
	}
	public void TestScript()
	{
		consoleInput.text = scriptBodyField.text;
		RunConsoleCommand ();
	}
	public void RunConsoleCommand()
	{
		m_pyOutput = string.Empty;
		m_pyCode = consoleInput.text;
		RunCommand (m_pyCode);
		//PythonEnvironment.CommandResult result = m_pyEnv.RunCommand(m_pyCode);
//		if (!string.IsNullOrEmpty(result.output))
//		{
//			m_pyOutput += "Python output : " + result.output + System.Environment.NewLine;
//		}
//		if (result.exception != null)
//		{
//			m_pyOutput += "Python exception : " + result.exception.Message;
//		}
//		consoleOutput.text = m_pyOutput;
	}
	
	public void RunCommand(string command)
	{
		m_pyEnv.thread_command = command;
		Thread thread = new Thread(m_pyEnv.RunThreadCommand);
		thread.Start ();
	}
	void OnGUI()
	{
		if(!string.IsNullOrEmpty(BotCon.ComData.EngineToPyOutput))
		{
			consoleOutput.text = ComData.EngineToPyOutput;
		}
		//m_pyCode = GUI.TextArea(new Rect(50, 50, 600, 200), m_pyCode);
//		if (GUI.Button(new Rect(50, 270, 80, 40), "Run"))
//        {
//            m_pyOutput = string.Empty;
//            PythonEnvironment.CommandResult result = m_pyEnv.RunCommand(m_pyCode);
//            if (!string.IsNullOrEmpty(result.output))
//            {
//                m_pyOutput += "Python output : " + result.output + System.Environment.NewLine;
//            }
//            if (result.exception != null)
//            {
//                m_pyOutput += "Python exception : " + result.exception.Message;
//            }
//        }
        //GUI.TextArea(new Rect(50, 330, 600, 300), m_pyOutput);
    }

}
