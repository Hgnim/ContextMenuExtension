reg delete HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink /f
reg add HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink /f /ve /d "�����߼���ݷ�ʽ(Ӳ����)"
reg add HKEY_CLASSES_ROOT\*\shell\Hgnim.CME.HardLink\command /f /ve /d "cmd /C \"mklink /h \"%%L - link\" %%L\""