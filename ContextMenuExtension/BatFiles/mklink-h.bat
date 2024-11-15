reg delete HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink /f
reg add HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink /f /ve /d "创建高级快捷方式(硬链接)"
reg add HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink\command /f /ve /d "cmd /C \"mklink /h \"%%L - link\" %%L\""