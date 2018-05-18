Sirius

I. Public repository
--------------------

SVN repository: http://sema.unfuddle.com/svn/sema_sirius2009

This repository contains the code and all input files.
This repository doesn't contain output files and template excel files.

Repository structure:

	\Sirius
        	\Sirius-Todo.xls
		\Readme.txt
		
		\Code
			\SiriusModel
				\import
				\"sub packages"
				\bin [1]
				\obj [1]

			\SiriusView
				\"sub packages"
				\bin [1]
				\obj [1]

        	\Released
			\SiriusView.exe
			\"dll and other files used by SiriusView.exe"

		\Data
			\Project1
				\Weather
				\"input files"

			\Project2
				\Weather
				\"input files"
                   
		\Output [2]
			\Project1
			\Project2

		\Template [1]
			\Project1
			\Project2
 
[1] : warning, not versioned. 
[2] : warning, no versioned files in this directory, only sub directories allowed.

II. Public ftp
--------------

ftp: ftp.rothamsted.bbsrc.ac.uk

This ftp contains only excel template files.

Log with the specified login and password to view Sirius file sharing.

ftp structure:

	\Readme.txt
	\Template
		\Project1
		\Project2