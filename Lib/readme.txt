To deploy this manually when you ve generated your own assemblies, 
using the Solution ManagedMenuExtensions.sln:

Copy ManagedMenuVS2008.dll, ManagedMenusVS2008.AddIn, ManagedMenuHostViews.dll 
and ManagedMenuHost.dll	to \Documents and Settings\<username>\My Documents\Visual Studio 2008\Addins\
Make the folder if it does not exist.

Nothing else is required. 

The Solution ManagedMenuExtensionsRelease.sln can only be build if you have the keyfile Jern.pfx
which is of course not publicly available. If you want to build it, you must use your own keyfile instead.
Since the Keyfile is needed to Sign the assemblies:

ManagedMenuHost.dll, ManagedMenuHostViews.dll amd ManagedMenuAddInViews.dll

In the release version they are supposed to be installed in the GAC.