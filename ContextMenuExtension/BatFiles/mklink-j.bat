reg delete HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink /f
reg add HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink /f /ve /d "�����߼���ݷ�ʽ(Ŀ¼����)"
reg add HKEY_CLASSES_ROOT\Directory\shell\Hgnim.CME.DirLink\command /f /ve /d "cmd /C \"mklink /j \"%%L - link\" %%L\""