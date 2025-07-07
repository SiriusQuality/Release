
import sys, getopt
from Translator.translator import Translator
#python PythonTranslatorExcelAgMIP.py -i "test_data/input/pierre_june_2020/Sardaigne_AgMIPformat2.xlsx" -o "Sardaigne_AgMIPformat2.json"
#A simple script that get console arguments and call translator method

def close():
   sys.exit(2)
def arguments_validation():
    
    argv=sys.argv[1:]
    inputfile = ''
    outputfile = ''
    try:
        opts, args = getopt.getopt(argv,"i:o:")
    
    except getopt.GetoptError:
        print ('please enter the required parameter:', 'script -i <inputfile> -o <outputfile>')
        close()

    for opt, arg in opts:
        if opt == '-i':
            inputfile = arg
        elif opt == "-o" :
            outputfile = arg
            
    if inputfile=='':
        print ('please enter the required parameter:', 'script -i <inputfile> ')
        close()
    if outputfile=='':
        outputfile = 'agmip_data.json'

    return inputfile, outputfile

inputfile, outputfile =arguments_validation() # validate user arguments in console

Translator.translate(inputfile, outputfile)
