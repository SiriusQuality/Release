from Translator.translator import Translator


TEST1 = "test1"
INPUT1 ="/home/mistea/PhD/development/translator-excel-agmip/test_data/input/pierre_2021/%s.xlsx"%TEST1
OUTPUT1 = "/home/mistea/PhD/development/translator-excel-agmip/test_data/output/%s.json"%TEST1

#Translator.translate(INPUT1, OUTPUT1)

TEST2 = "test2"
INPUT2 ="/home/mistea/PhD/development/translator-excel-agmip/test_data/input/pierre_2021/%s.xlsx"%TEST2
OUTPUT2 = "/home/mistea/PhD/development/translator-excel-agmip/test_data/output/%s.json"%TEST2




#TEST3 = "Breedwheat Biotech panel trials for model calibration (T1.3)_2021-06-07"

LINUXPATH= "/home/mistea/PhD/development/translator-excel-agmip/test_data/"
WINDOWSPATH= "C:/Users/vargas/Documents/PhD/development/translator-excel-agmip/test_data/"
PATH= LINUXPATH

TEST3 = "Breedwheat Biotech panel trials for model calibration (T1.3)_2021-07-15"
INPUT3 =PATH+"input/pierre_july_2021/%s.xlsx"%TEST3
OUTPUT3 = PATH+"output/pierre_july_2021/%s.json"%TEST3

TEST4 = "SolACE_data_template_v3.2_(with sample data)_2021-07-15"
INPUT4 =PATH+"input/pierre_july_2021/%s.xlsx"%TEST4
OUTPUT4 = PATH+"output/pierre_july_2021/%s.json"%TEST4


TEST5 = "CIM_AgMIP_site_soil_crop_management"
INPUT5 =PATH+"input/sibylle_sept_2021/%s.xlsx"%TEST5
OUTPUT5 = PATH+"output/sibylle_sept_2021/%s.json"%TEST5

Translator.translate(INPUT3, OUTPUT3)
Translator.translate(INPUT4, OUTPUT4)
Translator.translate(INPUT5, OUTPUT5)
