# CommandAndConquer
A library to handle the wiring up of classes and function for a command line application

To Use this library pull down the NuGet package "WMZ.MVCLI.CORE" to a console application.
inside the "main" function of your program.cs put this line

Processor.ProcessArgs(args);

You will then need to create a Folder in that project called "Controllers".  This is where you will put all of the classes that you want to act as controllers.  Be sure to put the attribut "CliController" on the class for it to be found.

After doing that you can add public static methods to this class giving them the attribut "CliCommand".

Once you have setup all of this you will then be able to access your code through the command line.  the format to do so is  
"<program.exe <controller> <command> -<parameter> <parameter_value>"  
To access the build in help function use '?' for either <controller> or <command>.

To download a template to make getting started easier please go [here](https://marketplace.visualstudio.com/items?itemName=wmaxzimmerman.CommandAndConquerCLI01)  
