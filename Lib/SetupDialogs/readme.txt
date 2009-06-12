The content of 1033.zip must be copied to

C:\Program Files\Microsoft Visual Studio 9.0\Common7\Tools\Deployment\VsdDialogs\1033\

in order to provide good looking setup dialogs.

Main effect is that the default text is deleted so that the Managed Menu Extension Icon
is visible.

Warning: You only want to do this if you want to generate ManagedMenuExtension msi files.
Make sure to make a backup of 

C:\Program Files\Microsoft Visual Studio 9.0\Common7\Tools\Deployment\VsdDialogs\1033\

to be able to get back to the default setup.

1033 is the number associated with the english culture, for other cultures, 
similar corrections should be made to the wid files of the approiate folders.

OBS: The setup dialogs in this Zip file corresponds to those of VS2008 sp1 for other settings it might not work.