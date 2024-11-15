using static System.Net.Mime.MediaTypeNames;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ContextMenuExtension
{
	internal class Program
	{
		const string version = "1.0.1.20241115";
		static void Main(string[] args)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))//该软件只支持windows系统，判断操作系统
			{
				bool isAdmin = false;

				//判断当前登录用户是否为管理员
				if (new System.Security.Principal.WindowsPrincipal(System.Security.Principal.WindowsIdentity.GetCurrent())
					.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
					isAdmin = true;

				static string ReadUserInput()
				{
					Console.Write("输入: ");
					return Console.ReadLine()!.ToLower();
				}
				///全名: InputErrorTextOutput
				static void IErrTO()
				{
					Console.WriteLine("参数错误");
				}
				BatFileControl bfc = new();
				bfc.CheckDir();

				Console.WriteLine("欢迎使用右键菜单扩展工具\r\n" +
					$"版本: V{version}");
tree_root:;
				if (!isAdmin)
				{
					Console.WriteLine("\r\n当前会话没有管理员权限，可能无法正常使用，输入\"A\"以管理员身份重新启动该程序");
				}
				Console.WriteLine(
	@"
请输入选项的代号以选择需要进行的操作:
1.高级快捷方式(软链接与硬链接);
Q.退出
About.关于
"
	);
				switch (ReadUserInput())
				{
					case "1":
						{
tree_1:;
							Console.WriteLine(
@"
1.安装/修复
2.删除
B.返回上一级
"
								);
							switch (ReadUserInput())
							{
								case "1":
tree_1_1:;
									Console.WriteLine(
@"
选择文件夹的链接方式:
1.符号链接(推荐)
2.目录链接
B.返回上一级
"                                       
										);
									switch (ReadUserInput())
									{
										case "1":
											bfc.RunBat(["mklink-d.bat", "mklink-h.bat"]);
											goto tree_root;
											case "2":
											bfc.RunBat(["mklink-j.bat", "mklink-h.bat"]);
											goto tree_root;
										case "b":
											goto tree_1;
										default:
											IErrTO();
											goto tree_1_1;
									}
								case "2":
									bfc.RunBat(["mklink-dj-r.bat", "mklink-h-r.bat"]);
									goto tree_root;
								case "b":
									goto tree_root;
								default:
									IErrTO();
									goto tree_1;
							}
						}
					case "q":
						goto endProgram;
					case "a":
						if (!isAdmin)
						{
							ProcessStartInfo startInfo = new()//使用管理员身份重新启动该程序
							{
								UseShellExecute = true,
								CreateNoWindow = true,
								//设置运行文件
								FileName = Environment.ProcessPath,
								//设置启动参数
								//Arguments = String.Join(" ", Args);
								Verb = "RunAs" // 以管理员身份运行								
							};
							Process.Start(startInfo);
						}
						else
						{
							IErrTO();
							goto tree_root;
						}
						goto endProgram;
					case "about":
						Console.WriteLine(
@$"
程序名: 右键菜单扩展
别名: ContextMenuExtension
版本: V{version} 
Copyright (C) 2024 Hgnim, All rights reserved.
Github: https://github.com/Hgnim/ContextMenuExtension
"
			);
						goto tree_root;
					default:
						IErrTO();
						goto tree_root;
				}

endProgram:;
			}
			else
			{
				Console.WriteLine("无法继续，该软件只支持Windows操作系统");
				Console.WriteLine("点击任意键继续...");
				Console.ReadKey();
			}
		}
	}
}