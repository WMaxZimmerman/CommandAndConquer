# CommandAndConquer
This library is designed to make the process of creating and managing Console Applications as easy as possible. CommandAndconquer will handle processing all command line arguments, wiring up and execution of methods, and user documetation. If you are tired of having to manage every possible command line argument on how to map those to methods, then this is a library you should absolutely starting using.

To start using CommandAndConquer there are two routes (three if you include pulling down this source code) that you can take.

# Using The Template
The easiest way to start using CommandAndConquer is to install [this template](https://marketplace.visualstudio.com/items?itemName=wmaxzimmerman.CommandAndConquerCLI01). After you have installed the template you can use it to create a new project. That project will have all of the needed boiler plate to be able to start creating a CommandAndConquer application. If you go this route you will probably notice how light the template really is.  The 'Program.cs' file only has only line of added code (Not including usings) and there is only one additional class. That class can be found in the 'Controllers' folder and will be name 'ExampleController.cs'. This class is designed to be an example of how you can setup controllers and commands for the CommandAndConquer framework. If you need more information than the examples provided in the code you can read on here.

# The Hard Way
If you already have an exisitng Console Application or you just hate super easy and convenient templates, you can setup the boiler plate manually with limited effort. To setup CommandAndConquer manually you will need to first install [this nuget package](https://www.nuget.org/packages/WMZ.CommandAndConquer.CLI/). After doing that you will want to add the following values to the 'appsettings' section of your 'App.config' file.

<add key="helpString" value="?" />  
<add key="argumentPrefix" value="--" />

If you do not have and 'appsettings' section you can simply add one. Now that we have the nuget package and our app.config ready we will want to update 'Program.cs' file. Inside of the 'Main' method of the 'Program.cs' you will want to add this line:

Processor.ProcessArgs(args);

This will send all of your command line arguments to CommandAndConquer to be validated and processed. Once we have that done we have finished all of the boiler plate for CommandAndConquer. Now we can move on to writing the code for your application.

# Controllers
A controller in CommandAndConquer is simply a class that is able to be accessed from the command line. To create a controller you just need to add the attribute 'CliController' to a public static class. This will let CommandAndConquer know that it is a controller. When creating the attribute it is good to note that it has two parameters:
Name - The name of the controller and the command line argument to be entered to access the controller.
Description - A description of the functionality provided by the controller. This can be output via the help function in a terminal.

# Commands
A command is a method that can be accessed by CommandAndConquer. Similar to controllers this must be a public static method and will need to have the attribute 'CliCommand'. This attribute also has two parameters just like its controller counterpart.
Name - The name of command and the command line argument to be entered to access the command.
Description - A description of the functionality of the method. This can be output via the help function in a terminal.

# Paramters
To wire in your paramters you have to do... nothing. Yes, nothing. No more if/select statements to decide a code path. Simiply put the parameters in the method like normal and let CommandAndConquer handle the rest. CommandAndConquer will use the variable name that you set in your code as the input for the terminal as well as provide you type safetly during execution. CommandAndConquer is smart enough to know what types your parameters are and will output all possible documentation when prompted via the help function. This include the name of the parameter, its type, weither it is optional or required (based on if it has a default value), and even its possible options if it is an enum. That is a lot of working and ridiculous wiring that you no longer have to do!

# Usage
Once you have setup the boiler plate and have at least one controller and method (Done for you if you used the template) you can build your executable and navigate to it in a terminal and start to run your application. To execute your code you will use the pattern:

<yourprogam.exe> <controller> <command> --<parameter> <value>

Notice the '--' infront of the <parameter>. This is the format that lets CommandAndConquer know when a paramter starts and will add the following arguments to it as values. It is good to know that if you have a List/Array/IEnumerable as your type that it will pass in all values until the next parameter. If the parameter is not a IEnumerable than it will only get the first value assigned and the others will be ignored.

# Error Handling
You might be thinking, "That all sounds great, but what if it recieves bad input?" CommandAndConquer is setup to handle invalid input gracefully.  It will valid all of the inputs before even attempting to execute your code, and if any of the inputs violate type or arguments it will notify the user and stop any further execution before reaching your code. CommandAndConquer will also gracefully handle errors that might happen in your code (Not that that would happen cause your code is flawless). If an error occurs in your code it will output the error message and the stack trace to the terminal.

# Configuration
If you don't like the defaults that I setup you can change the 'helpString' and the 'argumentPrefix' in the 'App.config' file. You should keep in mind that if you change these values then some of this documentation may no longer apply as it assumes you are using the defaults.
