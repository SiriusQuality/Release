#!/usr/bin/python3


# experiment items in agmip json are plots for this code

LEVELS_EXPERIMENT = ["trt_name", "eid"]
LEVELS_WEATHER = ["wst_id"]
LEVELS_SOIL = ["soil_id"]

EXPAND_ORDER=["experiment_description","treatments","experiments", "weathers"]

DATES_COLUMNS = ["sdat","icdat","date"]
class Config:
    """A class to define how to scan the JSON file"""

    @classmethod
    def get_expand_order(cls):
        return EXPAND_ORDER

    @classmethod
    def get_dates(cls):
        return EXPAND_ORDER
    @classmethod
    def get_configuration(cls):

        # the item's order is important
        dict = [   
            {#plot
                "name":"experiments",
                "path":["experiments"],
                "type":"list",
                "sheetName":"Plots",
                "transformHeader":"toLowerCase",
                "transformSet":"reduceByIds",
                "headerRow":"2",
                "levels": LEVELS_EXPERIMENT,
                "expand": [
                    {"col":"trt_name","config_name":"treatments"},
                    {"col":"cul_name","config_name":"crops"}
                ],
                "renameHeader": [
                    {"name":"treat_id" , "newName":"trt_name"}
                ],
                "takeSubset": ["trt_name", "cul_name"],
                #"newColumns":[{"name":"trt_name" , "from":"treat_id"}]
                #"excludeSubset":["crid","cul_name"]
            },
            {
                "name":"initial_conditions",
                "type":"map",
                "path":["experiments","initial_conditions"],
                "sheetName":"initial_conditions",
                "levels": LEVELS_EXPERIMENT,
                "transformHeader":"toLowerCase",
                "headerRow":"2"
            },
            {
                "name":"treatments",
                "type":"expandable",
                "path":[],
                "sheetName":"Treatments",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "expand": [{"col":"eid","config_name":"experiment_description"}],
                "renameHeader": [
                    {"name":"wst_id" , "newName":"wst_id_temp"},
                    {"name":"wst_dataset" , "newName":"wst_id"},

                    {"name":"trt_name" , "newName":"trt_long_name"},
                    {"name":"treat_id" , "newName":"trt_name"}
                ],
                "excludeSubset":["wst_id_temp"]
            },
            {
                "name":"experiment_description",
                "type":"expandable",
                "path":[],
                "sheetName":"Experiment_description",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "expand": [{"col":"eid","config_name":"fields"}],
                "typing":[
                    {"col":"plyr","type":"integer"},
                    {"col":"hayr","type":"integer"}
                ]
            },
            {
                "name":"fields",
                "type":"expandable",
                "path":[],
                "sheetName":"Fields",
                "transformHeader":"toLowerCase",
                "headerRow":"2"
            },

            {
                "name":"initial_conditions_soil",
                "type":"list",
                "path":["experiments","initial_conditions","soilLayer"],
                "sheetName":"initial_condition_layers",
                "transformHeader":"toLowerCase",
                "levels": LEVELS_EXPERIMENT,
                "headerRow":"2"
            },
            {
                "name":"residue",
                "type":"map",
                "path":["experiments","initial_conditions"],
                "sheetName":"Residue",
                "transformHeader":"toLowerCase",
                "levels": LEVELS_EXPERIMENT,
                "headerRow":"2"

            },
            {
                "name":"events",
                "type":"list",
                "path":["experiments","management","events"],
                "eventsType":["Planting_events", "Irrigation_events" , "Fertilizer_events", "Tillage_events",
                                "Harvest_events"],
                "eventsName":["planting", "irrigation" , "fertilizer","tillage","harvest"],
                "sheetConfig":{
                    'Planting_events':{
                        "renameHeader": [
                            {"name":"pdate" , "newName":"date"},
                            {"name":"treat_id" , "newName":"trt_name"}
                        ],
                        "excludeSubset":["sdat"]
                    },
                    'Harvest_events':{
                        "renameHeader": [
                            {"name":"hadat" , "newName":"date"},
                            {"name":"treat_id" , "newName":"trt_name"}
                        ]
                    }
                    ,
                    'Irrigation_events':{
                        "renameHeader": [
                            {"name":"idate" , "newName":"date"},
                            {"name":"treat_id" , "newName":"trt_name"}
                        ]
                    }
                    ,
                    'Fertilizer_events':{
                        "renameHeader": [
                            {"name":"fedate" , "newName":"date"},
                            {"name":"treat_id" , "newName":"trt_name"}
                        ]
                    }
                    ,
                    'Tillage_events':{
                        "renameHeader": [
                            {"name":"tdate" , "newName":"date"},
                            {"name":"treat_id" , "newName":"trt_name"}
                        ]
                    }
                },
                "transformHeader":"toLowerCase",
                "levels": LEVELS_EXPERIMENT,
                "headerRow":"2"
            },
            ## soils
            {
                "name":"soils",
                "path":["soils"],
                "type":"list",
                "sheetName":"Soil_metadata",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_SOIL
            },
            {
                "name":"soilLayer",
                "type":"list",
                "path":["soils","soilLayer"],
                "sheetName":"Soil_profile_layers",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_SOIL
            },

            
            {
                "name":"crops",
                "type":"expandable",
                "path":[],
                "sheetName":"Genotypes",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "takeSubset": [ "cul_name", "crid"]
            },

            # weather
            {
                "name":"weatherStations",
                "path":[],
                "type":"expandable",
                "sheetName":"Weather_stations",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_WEATHER,
                "renameHeader": [
                    {"name":"wst_id" , "newName":"wst_id_temp"}
                ]
            },
            {
                "name":"weathers",
                "type":"list",
                "path":["weathers"],
                "sheetName":"Treatments",
                "transformHeader":"toLowerCase",
                "transformSet":"reduceByIds",
                "headerRow":"2",
                "levels": LEVELS_WEATHER,
                "takeSubset": ["wst_id","wst_id_temp"],
                "renameHeader": [
                    {"name":"wst_id" , "newName":"wst_id_temp"},
                    {"name":"wst_dataset" , "newName":"wst_id"}
                ],
                "expand": [
                    {"col":"wst_id_temp","config_name":"weatherStations", "delete_after":True}
                ],
            },
            {
                "name":"dailyWeather",
                "type":"list",
                "path":["weathers","dailyWeather"],
                "sheetName":"Weather_daily",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_WEATHER,
                "renameHeader": [
                    {"name":"wst_dataset" , "newName":"wst_id"}
                ]
            },

            # #time series data
            {
                "name":"summary",
                "type":"map",
                "path":["experiments","observed"],
                "sheetPattern":"Summary",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_EXPERIMENT,
                "renameHeader": [
                    {"name":"treat_id" , "newName":"trt_name"}
                ]
            },
            {#always after summary, because it creates the map object
                "name":"observations",
                "type":"list",
                "path":["experiments","observed","timeSeries"],
                "sheetPattern":"Obs",
                "transformHeader":"toLowerCase",
                "headerRow":"2",
                "levels": LEVELS_EXPERIMENT,
                
                "renameHeader": [
                    {"name":"treat_id" , "newName":"trt_name"}
                ]
            }
        ]
        return dict