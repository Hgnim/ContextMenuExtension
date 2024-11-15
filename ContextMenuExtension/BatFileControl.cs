using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace ContextMenuExtension
{
	internal class BatFileControl
	{
		readonly static string tempDir = $@"{System.IO.Path.GetTempPath()}ContextMenuExtension\";
		readonly static string batFileDir = tempDir+@"BatFiles\";
		/// <summary>
		/// 检查所需文件夹是否存在且如果不存在就创建
		/// </summary>
		public void CheckDir()
		{
			Directory.CreateDirectory(tempDir);
			Directory.CreateDirectory(batFileDir);
		}
		/// <summary>
		/// 执行
		/// </summary>
		/// <param name="batFiles">文件</param>
		public void RunBat(string[] batFiles)
		{
			foreach (string file in batFiles)
			{		
				Assembly assembly = Assembly.GetExecutingAssembly();
				string resPath = $"ContextMenuExtension.BatFiles.{file}";
				Stream stream = assembly.GetManifestResourceStream(resPath)!;
				Stream outFile = File.Create(batFileDir + file);
				stream.CopyTo(outFile);
				outFile.Close();
				stream.Close();
			}

			string allFileString = "\"";
			for(int i = 0; i < batFiles.Length; i++) 
			{
				allFileString +=batFileDir+ batFiles[i];
				if (i+1 < batFiles.Length)
					allFileString += " & ";
			}
			allFileString += "\"";

			static void ProcessOutputData(object sender, DataReceivedEventArgs e)
			{
				if(e.Data!=null)
				Console.WriteLine(e.Data);
			}
			Process process = new()
			{
				StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,//不使用系统shell程序启动  
					RedirectStandardInput = true,//接受来自调用程序的输入信息
					RedirectStandardOutput = true, //重定向输出  
					RedirectStandardError = true,// 输出错误		
					CreateNoWindow = true,//不创建窗口 								
					FileName = "cmd.exe"/*,
					Arguments = " /c " + allFileString*/
				}				
			};
			//绑定输出事件
			process.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputData);
			process.ErrorDataReceived += new DataReceivedEventHandler(ProcessOutputData);
			try
			{
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.StandardInput.AutoFlush = true;
				//输入命令
				process.StandardInput.WriteLine($"cmd /c {allFileString}");
				process.StandardInput.WriteLine("exit");
				process.WaitForExit();
			}
			catch { Console.WriteLine("执行失败，发生未知错误");return; }
			Console.WriteLine("\r\n执行完毕\r\n");
		}
	}
}
