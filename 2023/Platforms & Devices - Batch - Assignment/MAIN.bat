:: FILE HEADER.
@ECHO off
::File name: MAIN.bat.
::Purpose:   To learn about batch script and complete the assignment given.
::My info:   Jenna Boyes, 1000056739.
::Course:    Bachelor of Information Technology, Year 1, Semester 1.


::LAYOUT:
::Q#, name of chosen command(s), desc command.
::actual command to complete actions needed.
::pause script to allow for marking.
::clear screen.



::A1     Output a line to the screen that displays your name.
ECHO A1,      I will use the command ECHO,      It allows me to write to the console.
ECHO Jenna Boyes
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A2     Outputs the file name to the screen (not hard coded!).
ECHO A2,      I will use the command DIR,      It allows me to see the files and folders within the same directory as this file.
DIR
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A3     Create a folder on the root of the C: drive called “My Batch Script File Assignment”.
ECHO A3,      I will use the command CHDIR and MKDIR,      They allow me to change the current directory and then make a new one.
CHDIR /
MKDIR "My Batch Script File Assignment"
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A4     Create a folder within “My Batch Script File Assignment” called “Input”.
ECHO A4,      I will use the command CHDIR and MKDIR again,      They allow me to change the current directory and then make a new one.
CHDIR "My Batch Script File Assignment"
MKDIR Input
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A5     Create another folder within “My Batch Script File Assignment” called “Processing”.
ECHO A5,      I will use the command MKDIR,      This allows me to make a new directory.
MKDIR Processing
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A6     Create another folder within “My Batch Script File Assignment” called “Output”.
ECHO A6,      I will use the command MKDIR,      This allows me to make a new directory.
MKDIR Output
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A7     List all hidden files in the root directory of the C: drive – output the listing to a file called “Input Data.txt” in the “Input” subfolder.
ECHO A7,      I will use the command DIR,      This allows me to see the files in a chosen directory.
DIR C:\ /A:H > "Input Data.txt"
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A8     Make a backup copy of Input Data.txt on the root of the C: drive, and with the same name, but with extension “.bak”.
ECHO A8,      I will use the command COPY,      This allows me to copy selected file(s) to a specified location.
COPY "Input Data.txt" C:\"Input Data.bak"
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A9     Go to the root directory of the C: drive.
ECHO A9,      I will use the command CHDIR,      This allows me to change the current directory.
CHDIR C:\
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A10     Update the folder search path for batch script file execution to include the “Processing” subfolder, and then display the folder search path.
ECHO A10,      I will use the command PATH and ECHO,      This allows me to display or set a search path for executable files and display a message.
PATH C:\"My Batch Script File Assignment"\Processes ;%PATH%
ECHO %PATH%
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A11     Change the command prompt to include the time, the words “Hello World” and the ‘>’ character
ECHO A11,      I will use the command PROMPT,      This allows me to change the Windows command prompt.
PROMPT $T Hello World$G
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A12     Create a new command window, with red coloured text and green background 
ECHO A12,      I will use the command CMD,      This allows me to start a new instance of CMD.
CMD /T:24
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A13     Create another new command window, with blue coloured text and a bright white background,
          ::and with a prompt that includes the Windows version number.
ECHO A13,      I will use the commands CMD and PROMPT,      This allows me to start a new instance of CMD and change the Windows command prompt.
CMD /T:F1 /K PROMPT $V$G
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A14     List all the folders (not files) in C:\WINDOWS\System32, sorted into alphabetical order – 
          ::output the listing to a file called “Batch Script File Output Data.txt” in the “Output” subfolder.
ECHO A14,      I will use the command DIR,      This allows me to display a list of files and subdirectories in a directory.
DIR C:\WINDOWS\System32 /A:D /O:N > C:\"My Batch Script File Assignment"\Output\“Batch Script File Output Data.txt”
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A15     List all text files whose names are up to seven characters long on the whole C: drive – make the listing output in wide format – 
          ::the listing output must be appended to the end of “Batch Script File Output Data.txt”
ECHO A15,      I will use the command DIR,      This allows me to display a list of files and subdirectories in a directory.
DIR ???????.* /A:-D-L /S /W >> C:\"My Batch Script File Assignment"\Output\“Batch Script File Output Data.txt”
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A16     Delete folder “My Batch Script File Assignment”, together with all subfolders and their contents.
ECHO A16,      I will use the command RMDIR,      This allows me to display a list of files and subdirectories in a directory.
RMDIR /S /Q C:\"My Batch Script File Assignment" 
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A17     Output to the screen the configuration information relating to your network settings.
ECHO A17,      I will use the command NET,      This allows me to display or set information about the local network and users.
NET USER User 
::you will have to change the username (User) to work on your PC
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A18     Create a local user called “Bob”, then create a local group called “Awesome Users” and add Bob to it.
ECHO A18,      I will use the command NET,      This allows me to display or set information about the local network and users.
NET USER Bob /ADD
NET LOCALGROUP "Awesome Users" /ADD
NET LOCALGROUP "Awesome Users" Bob /ADD
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A19     Delete the group “Awesome Users” and delete the user “Bob”.
ECHO A19,      I will use the command NET,      This allows me to display or set information about the local network and users.
NET USER Bob /DELETE
NET LOCALGROUP "Awesome Users" /DELETE
PAUSE 
CLS
::------------------------------------------------------------------------------------



::A20     Create a task to be scheduled at 10 pm every Sunday that will check the C disk for errors 
          ::and write the results to a file called “chkdskResults.txt” on the desktop.
ECHO A20,      I will use the command SCHTASKS,      This allows me to schedule commands and programs to run on this computer.
SCHTASKS /CREATE /SC WEEKLY /D SUN /ST 22:00 /TN "Error Checking" /TR C:\ErrorChecking.bat >> C:\Users\User\Desktop\“chkdskResults.txt”
::   C:\ErrorChecking.bat has the command:     CHKDSK C: 
PAUSE 
CLS
::------------------------------------------------------------------------------------
