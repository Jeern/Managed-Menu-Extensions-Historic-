## Project Description
The Main goal of "Managed Menu Extensions" is to provide easy access to adding Right Click menus in the Visual Studio Solution Explorer. This means developers will no longer have to use the cumbersome Visual Studio Add-In model. It should be as easy as inheriting an abstract class, and copying the assembly to a specific location.

The second goal is to explore the new .NET 3.5 System.AddIn namespace. The Extension model will be implemented using the functionality provided in this namespace.

The second beta release provides Add-In functionality for Visual Studio 2008, later editions will feature Add-In functionality for Windows Explorer and perhaps even for SharpDevelop. 

A release of Managed Menu Extensions consists of a number of MSI packages. One designed for each specific environment, one containing the contract, wiews, and adapters (from the System.AddIn namespace), and one containing af few simple samples (one sample shows how to use Linq to XML to create a simple XML document with class info when right clicking a class file).

No matter what environment (of the above mentioned) your a developing right click menus for, you can do it by using the same simple programming model.

One of the interesting points is that the same Add-In can potentially provide Right Click menus for many different environments. E.g. you do not have to develop different Add-Ins for Windows Explorer and Visual Studio, although you will rarely need the same functionality in both environments Managed Menu Extensions will give you the opportunity to do so if needed.

Further ideas might "pop up" later.

Managed Menu Extensions is concieved and implemented by Jesper Niedermann

## Update: Managed Menu Extensions has been re-developed for Visual Studio 2010 at [http://mme.codeplex.com](http://mme.codeplex.com)


## The System.AddIn namespace

You can read about the .NET 3.5 System.AddIn namespace at the CLR Add-In team blog: [http://blogs.msdn.com/clraddins/](http://blogs.msdn.com/clraddins/)

Very briefly the CLR Add-In team wants you to follow the Architecture on this figure, when you design applications that can host Add-Ins:

![](Home_SystemAddInArchitecture.jpg)

## Elements of Managed Menu Extensions

Managed Menu Extensions consists of the following elements:

* MSI packages for different environments that supplies the basic functionality to target a specific environments Right Click menus. The first example of this installs a standard VS2008 AddIn.
* ManagedMenuExtensions.msi installs the basic AddIn functionality (this time System.AddIn functionality). One of the assemblies - ManagedMenuAddInViews contains the asbtract class which should be inherited when you want to provide your own right click menus.
* ManagedMenuExtensionsSamples.msi installs 3 samples that demonstrates how to implement your own right click menus.

## Architecture of Managed Menu Extensions

The design of the Managed Menu Extensions follows the CLR Add-In teams recommendations (it has to) and is summarized on this figure:
![](Home_MMExtArchitecture.jpg)

## Folder Structure

The following folder structure is used for the assemblies of the project:

..\AddIns\<AddIn1>\
..\AddIns\<AddIn2>\
..\AddInsDisabled\<AddIn3>
..\AddInsDisabled\<AddIn4>
..\AddInSideAdapters\
..\AddInViews\   - and placed in GAC
..\Contracts\
..\HostSideAdapters\

Host placed in GAC
HostView placed in GAC

VS2008 AddIn placed in _C:\Documents and Settings\<username>\My Documents\Visual Studio 2008\Addins\_ or in _C:\Documents and Settings\All Users\Application Data\Microsoft\MSEnvShared\AddIns\_ if installed for all users.

As can be seen some assemblies are placed in the GAC. This is for ease of use. When You implement a Managed Menu Extensions AddIn you just refer to the ManagedMenuAddInView.dll in the GAC.

The AddInsDisabled folders might be confusing to those who know the System.AddIn Framework. In Managed Menu Extensions the plan is to provide a very simple tool that enables/disables Add-Ins by copying them to/from said folders.

## Additional information
This powerpoint contains some of the information on this page: [ManagedMenuExtension.ppt](Home_ManagedMenuExtension.ppt)
