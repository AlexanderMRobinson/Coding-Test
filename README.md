					Coding Test
Router patch check
------------------
You work for a team managing a network, which contains a large number of routers
. All these routers now need a firmware patch to be installed, and your job is 
to write a program to determine which routers can be patched, based on a 
number of criteria:

 The router has not already been patched
 The current version of the router OS is 12 or above
 There are no other routers which share the same IP address
 There are no other routers which share the same hostname

Your program will be run with a single command line argument, which will be the 
name of a CSV (Comma Separated Value) file containing a list of routers. 
Here’s an example of the format of the file: 

Hostname,IP Address,Patched?,OS Version,Notes 
A.example.COM,1.1.1.1,NO,11,Faulty fans 
b.example.com,1.1.1.2,no,13,Behind the other routers so no one sees it 
C.EXAMPLE.COM,1.1.1.3,no,12.1, 
d.example.com,1.1.1.4,yes,14, 
c.example.com,1.1.1.5,no,12,Case a bit loose 
e.example.com,1.1.1.6,no,12.3, 
f.example.com,1.1.1.7,No,12.200, 
g.example.com,1.1.1.6,no,15.0,Guarded by sharks with lasers on their heads

You can safely assume that the file contents are in a valid CSV format, and are 
as simple as the example above. A line will not contain any commas other than 
the ones separating fields, and none of the fields are surrounded by quotes. 
You should ignore blank lines, but otherwise there will always be the correct 
number of fields (5) in a line. The data in all the fields is not, however, 
guaranteed to be valid. Data which is valid should still be taken into account. 
In this example only two routers can be patched: b.example.com and 
f.example.com. Your program should print out a list of routers that can be 
patched, in the following format: 
b.example.com (1.1.1.2), OS version 13[Behind the other routers so no one sees it] 
f.example.com (1.1.1.7), OS version 12.200

To keep things fair for people working in different languages, your code should 
parse the CSV itself (do not use a built-in CSV parser, even if one is available
in the standard library).

Assumptions
-----------
1) All IP addresses will conform to the IPV4 standard, as opposed to the IPV6 
   standard, and therefore follow the format of:- 
   a.b.c.d - Where a through d represent integer values between 0 and 255
   
2) Hostnames will contain characters from the following range only:-
   ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789.-
   
3) A router will can only be patched if and only if, it fulfils all the 
   following criteria:-
		- The router has not been previously patched (Patched? = No)
		- OS Version is 12+ (OS Version >= 12)
		- The IP address of the router is unique within the dataset
		- The hostname of the router is unique within the dataset
	Failure to fulfil all 4 of these will result in the router in question being
	specified as not requiring a patch.
	e.g If Patched? = No, OS version = 13 and IP Address is unique 
	but the Hostname is not ==> Router cannot be patched.   
   
4) Any data point that is deemed to be invalid by the system, will still be 
   considered (i.e. it will no be immediately discarded) however due to the 
   constraints detailed in Assumption 3, namely that all 4 criteria must be 
   fulfilled to be patched, an invalid data point will subsequently result in 
   the router being deemed ineligible for the patch.
   
5) The Top Level Domain of the Hostname (the word after the final fullstop) will
   only be from the following range:-
   .com, .uk, .org, .net, .info, .biz
   It should be noted that any hostname not using a TLD from this list will be 
   classified as invalid, even if it is a valid domain, and therefore the router 
   associated with this hostname will subsequently not require patching.
   This was done due to the time constraints, and more could easily be 
   added at a later date. 

6) All input data, with exception being the Notes, is considered case 
   insensitive and so will be displayed as lower case in the ouput.
   e.g (A.HOST.COM, 1.1.1.1, NO, 12, ThIs Is A TeSt) 
       Would result in the output:-
       a.host.com (1.1.1.1), OS Version 12 [ThIs Is A TeSt]

Folder Structure
----------------
 ----- README.MD - Current Document 
 |
 |
 ----- Source - Folder containing the source files for the main software 
 |              solution and unit tests.
 |
 ----- Test - Folder containing a second software solution produced to generate
              random datasets, according to the specifications outlined in the 
			  PDF and the assumptions previously stated. This folder also 
			  contains a PowerShell script that was created to automatically 
			  run and compare the outputs of the dataset generator and the main
              software solution. (Only works when CSVGenerator and CodingTest 
			  solutions have been built in Debug mode.)

Build & Run
-----------
To compile this application under Linux, using Mono, run the following command
from a shell within the folder Source\CodingTest\ :-

xbuild CodingTest.csproj

This will produce a bin\Debug folder within which is the compiled executable 
(CodingTest.exe).

To run this file move to this directory and execute the following command:- 

mono CodingTest.exe <path to csv file>

The same process can be applied for the CSVGenerator solution.
	  