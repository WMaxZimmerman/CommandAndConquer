# This is not replaced by https://github.com/WMaxZimmerman/NTrospection/tree/master/src/NTrospection.CLI

# CommandAndConquer
This library is designed to make the process of creating and managing Console Applications as easy as possible. CommandAndConquer will handle processing all command line arguments, wiring up and execution of methods, and user documentation. If you are tired of having to manage every possible command line argument on how to map those to methods, then this is a library you should absolutely starting using.

To start using CommandAndConquer there are two routes (three if you include pulling down this source code) that you can take.

This library targets .net framework and is not currently being worked on. The [dotnet core version](https://github.com/WMaxZimmerman/NTrospection/tree/master/src/NTrospection.CLI) is currently my main focus as that is where most of my development has been.

# Using The Template
The easiest way to start using CommandAndConquer is to install [this template](https://marketplace.visualstudio.com/items?itemName=wmaxzimmerman.CommandAndConquerCLI01). After you have installed the template you can use it to create a new project. That project will have all of the needed boiler plate to be able to start creating a CommandAndConquer application. If you go this route you will probably notice how light the template really is.  The 'Program.cs' file only has only line of added code (Not including usings) and there is only one additional class. That class can be found in the 'Controllers' folder and will be name 'ExampleController.cs'. This class is designed to be an example of how you can setup controllers and commands for the CommandAndConquer framework. If you need more information than the examples provided in the code you can read on here.

# The Hard Way
If you already have an exisitng Console Application or you just hate super easy and convenient templates, you can setup the boiler plate manually with limited effort. To setup CommandAndConquer manually you will need to first install [this nuget package](https://www.nuget.org/packages/WMZ.CommandAndConquer.CLI/). Now that we have the nuget package ready we will want to update 'Program.cs' file. Inside of the 'Main' method of the 'Program.cs' you will want to add this line:

```
Processor.ProcessArgs(args);
```

This will send all of your command line arguments to CommandAndConquer to be validated and processed. Once we have that done we have finished all of the boiler plate for CommandAndConquer. Now we can move on to writing the code for your application.

# Controllers
A controller in CommandAndConquer is simply a class that is able to be accessed from the command line. To create a controller you just need to add the attribute 'CliController' to a public static class. This will let CommandAndConquer know that it is a controller. When creating the attribute it is good to note that it has two parameters:
Name - The name of the controller and the command line argument to be entered to access the controller.
Description - A description of the functionality provided by the controller. This can be output via the help function in a terminal.

# Commands
A command is a method that can be accessed by CommandAndConquer. Similar to controllers this must be a public static method and will need to have the attribute 'CliCommand'. This attribute also has two parameters just like its controller counterpart.
Name - The name of command and the command line argument to be entered to access the command.
Description - A description of the functionality of the method. This can be output via the help function in a terminal.    
NOTE: If you have async processes in your method you should use a Wait/Result if you need the application to wait before returning.

# Parameters
To wire in your parameters you have to do... nothing. Yes, nothing. No more if/select statements to decide a code path. Simiply put the parameters in the method like normal and let CommandAndConquer handle the rest. CommandAndConquer will use the variable name that you set in your code as the input for the terminal as well as provide you type safetly during execution. CommandAndConquer is smart enough to know what types your parameters are and will output all possible documentation when prompted via the help function. This include the name of the parameter, its type, weither it is optional or required (based on if it has a default value), and even its possible options if it is an enum. That is a lot of working and ridiculous wiring that you no longer have to do!  However, if you do want to add a bit more details to your paramters you can use the "CliParamter" attribute. This attribute allows you to specify an alias and description for a parameter. Currently the alias is required to be passed to the constructor and the description is optional. If specified the description will be output when the help command is used. You are also able to change the "paramDetail" config value to "detailed". If you do, you will also be able to see what value was assigned as a default for optional parameters.

One special case is when you have a boolean argument. You are now able to use just the argument without specifying a value. If you do this the value will be set as true.

# Usage
Once you have setup the boiler plate and have at least one controller and method (Done for you if you used the template) you can build your executable and navigate to it in a terminal and start to run your application. To execute your code you will use the pattern:

```
<yourprogam.exe> <controller> <command> --<parameter> <value>
```

Notice the '--' infront of the <parameter>. This is the format that lets CommandAndConquer know when a paramter starts and will add the following arguments to it as values. It is good to know that if you have a List/Array/IEnumerable as your type that it will pass in all values until the next parameter. If the parameter is not a IEnumerable than it will only get the first value assigned and the others will be ignored.

# Error Handling
You might be thinking, "That all sounds great, but what if it recieves bad input?" CommandAndConquer is setup to handle invalid input gracefully.  It will valid all of the inputs before even attempting to execute your code, and if any of the inputs violate type or arguments it will notify the user and stop any further execution before reaching your code. CommandAndConquer will also gracefully handle errors that might happen in your code (Not that that would happen cause your code is flawless). If an error occurs in your code it will output the error message and the stack trace to the terminal.

# Application Loop
You can also enable the application loop for you project. To do so you will need to add the "applicationLoopEnabled" key to your appsettings section of your config with a value of "true". After you have done this your application is setup to use the Application Loop. But, what is the Application Loop you may be asking. The Application Loop is where you execute your application with no parameters. This puts you into a loop that keeps you in the context of your application until you pass it the exit string "exit". This will allow you to not have to continue executing new instances of your application, and will allow you to leave off the application part of calls.  This is mostly useful when you will be performing several different command for your application.

# Configuration
If you don't like the defaults that I setup you can change the 'helpString' and the 'argumentPrefix' in the 'App.config' file. To do this simply add these lines:

```
<add key="helpString" value="?" />
<add key="argumentPrefix" value="--" />
<add key="paramDetail" value="simple" />
<add key="applicationLoopEnabled" value="true" />
<add key="inputIndicator" value=">" />
<add key="exitString" value="exit" />
```

If you don't have an 'appsettings' section of your 'App.config' you can simply add one and then add the lines there. You should keep in mind that if you change these values then some of this documentation may no longer apply as it assumes you are using the defaults.

# Hello World

Program.cs:  
  
```  
using CommandAndConquer.Core;  
  
public class Program  
{  
  public method Main(string[] args)  
  {  
     Processor.ProcessArgs(args);  
  }  
}  
```  

Controller:  
  
```  
using CommandAndConquer.CLI.Attributes;  
  
[CliController("example", "this is the description")]  
public static class ExampleController  
{  
  [CliCommand("command", "this is the description for the command")]  
  public static void MethodToBeExecuted(string name)  
  {  
    System.Console.WriteLine("Hello, " + name);  
  }  
}  
```  
 Usage:
 
 ```
 helloworld.exe example command --name World  
 ```
