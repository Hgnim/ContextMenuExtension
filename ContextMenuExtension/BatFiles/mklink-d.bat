reg delete HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink /f
reg add HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink /f /ve /d "创建高级快捷方式(符号链接)"
reg add HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink\command /f /ve /d "cmd /C \"mklink /d \"%%L - link\" %%L\""