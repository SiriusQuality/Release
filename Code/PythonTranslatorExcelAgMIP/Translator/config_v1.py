#!/usr/bin/python3

class Config:
    """A class to define how to scan the JSON file"""
    @classmethod
    def get_configuration(cls):

        dict = [   
            {
                "name":"experiments",
                "path":["experiments"],
                "type":"list",
                "sheetName":"Metadata",
                "transformHeader":"toLowerCase",
                "headerRow":"1",
                #"excludeSubset":["crid","cul_name"]
            },
            {
                "name":"initial_conditions",
                "type":"map",
                "path":["experiments","initial_conditions"],
                "sheetName":"Init_conditions",
                "transformHeader":"toLowerCase",
                "headerRow":"1"

            },
            
            {
                "name":"residue",
                "type":"map",
                "path":["experiments","initial_conditions"],
                "sheetName":"Residue",
                "transformHeader":"toLowerCase",
                "headerRow":"1"

            },

            {
                "name":"tillage",
                "type":"map",
                "path":["experiments","initial_conditions"],
                "sheetName":"Tillage",
                "transformHeader":"toLowerCase",
                "headerRow":"1"

            },

            {
                "name":"initial_conditions_soil",
                "type":"list",
                "path":["experiments","initial_conditions","soilLayer"],
                "sheetName":"Init_conditions_Soil_layers",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },
            {
                "name":"events",
                "type":"list",
                "path":["experiments","management","events"],
                "eventsType":["Plantings", "Irrigations" , "Fertilizers", "Other", "Metadata"],
                "eventsName":["planting", "irrigation" , "fertilizer", "other", "planting"],
                "sheetConfig":{'Metadata':{'takeSubset':["id","crid","cul_name"]}},
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },

            # {
            #     "name":"plantingEventMetadata",
            #     "type":"list",
            #     "path":["experiments","management","events"],
            #     "eventsType":["Metadata"],
            #     "eventsName":["planting"],
            #     "transformHeader":"toLowerCase",
            #     "headerRow":"1",
            #     "takeSubset":["id","crid","cul_name"]
            # },


            
            {
                "name":"weathers",
                "path":["weathers"],
                "type":"list",
                "sheetName":"Weather_meta",
                "transformHeader":"toLowerCase",
                "headerRow":"1" 
            },
            {
                "name":"dailyWeather",
                "type":"list",
                "path":["weathers","dailyWeather"],
                "sheetName":"Weather_daily",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },
            {
                "name":"soils",
                "path":["soils"],
                "type":"list",
                "sheetName":"Soils_meta",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },
            {
                "name":"soilLayer",
                "type":"list",
                "path":["soils","soilLayer"],
                "sheetName":"Soil_layers",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },
            {
                "name":"summary",
                "type":"map",
                "path":["experiments","observed"],
                "sheetPattern":"Summary",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            },
            {#always after summary, because it creates the map object
                "name":"observations",
                "type":"list",
                "path":["experiments","observed","timeSeries"],
                "sheetPattern":"Obs",
                "transformHeader":"toLowerCase",
                "headerRow":"1"
            }
        ]
        return dict