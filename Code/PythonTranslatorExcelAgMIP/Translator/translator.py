

from Translator.config import Config
from Translator.excel_helper import ExcelHelper
from Translator.excel_json_helper import ExcelJsonHelper

import sys

def close():
   sys.exit(2)

#CONSTANS
EVENTS_LABEL_LIST=["events"] # special process for events section in excel file 
OBSERVATIONS_LABEL="observations" # special process for observatioins 
SUMMARY_LABEL="summary" # special process for observations 


class Translator:
    """
    A class to run all the process related to the translation. 
    
    It depends on  Config, ExcelHelper, ExcelJsonHelper
    Step 1 extract data, using configuration defined in Config manipulate the excel file and create Json-like items
    Step 2 join by IDs and created paths, then data should be linked with others sheets to create corresponding path in the consolidated JSON file  
    Step 3 format a cleaned, some items are list or dictionaries, in this process Json data is cleaned to be uniform
    
    """
    @classmethod
    def translate(cls, excel_name, outputfile):
        
        child_grouper={}# grouper for children ex {initial_conditions:[List], management:[list]}
        json_parameters= Config.get_configuration()# template to define the json structure
        my_excel_helper = ExcelHelper()

        
        my_excel_helper.load_file(excel_name)
        
        sheets_names= my_excel_helper.get_sheets_names()
       

        #Step 1 extract data from excel
        for data in json_parameters:
            # events process

            data_name = data["name"]
            if(data_name in EVENTS_LABEL_LIST):
               
                list_objects=ExcelJsonHelper.several_sheets_reader(data["eventsType"], data,  my_excel_helper, "addEvents")
                child_grouper["events"]=list_objects
            
            #  observed data process    
            elif (data_name in [OBSERVATIONS_LABEL,SUMMARY_LABEL ] ):
                
                sheetsList=list(filter(lambda x: data["sheetPattern"] in x, sheets_names ))
                list_objects=ExcelJsonHelper.several_sheets_reader(sheetsList, data,  my_excel_helper)
                child_grouper[data_name]=list_objects

            # other sheets
            else:    
                list_objects=ExcelJsonHelper.get_data_json_like(data,my_excel_helper)
                child_grouper[data_name]=list_objects
        
        print("Step 1 finished (loading)")
        ## step 2 expand method
        EXPAND_ORDER = Config.get_expand_order()
        

        for expandable_item in EXPAND_ORDER:
            local_config=next((item for item in json_parameters 
                                if item["name"] == expandable_item),None)

            config_name = local_config["name"]
            config_expand = local_config["expand"]
            
            
            
            for exp in config_expand:
                col= exp["col"]
                config_name_expand= exp["config_name"]
                config_bool_delete= exp.get("delete_after")

                for item_to_expand in child_grouper[config_name]:

                    
                    currentExpansion= next((item for item in child_grouper[config_name_expand] 
                                            if item[col] == item_to_expand[col]),None)
                    # we assume it exists
                    if currentExpansion:
                        item_to_expand.update(currentExpansion)
                        if config_bool_delete:
                            del item_to_expand[col]                    #else: 
                        #print("Warning:expand sheet:%s didn't have id:%s"%(config_name_expand,item_to_expand[col]))
                del child_grouper[config_name_expand] # one expand aplies only for one sheet

        
        """
        experiments: [{}],
        initial_conditions: [{}]
        """
        print("Step 2 finished (expanding)")
        ## step 3 organice data joining based on ids that link elements, create paths when applies
        for local_data in  json_parameters: 
            

            if len(local_data["path"]) >1 :# has a long path, it is not a root element
                
                data_levels=local_data["levels"]
                data_type=local_data["type"]
                base_path=local_data["path"].pop(0) # remove first elements, root element
        
                for item in child_grouper[local_data["name"]]:
                    # identify level id
                    selected_level = None
                    for level in data_levels:
                        if level in item and item: # it exist and it is not empty 
                            selected_level = level
                            break # one level is enough
                    
                    if not selected_level:
                        print("item not link with experiments:", item)
                        print("sheet",local_data["name"])
                        
                   
                    #locate into the map
                    for itemParent in child_grouper[base_path]:
                        # it can either experiment or treatments IDs
                        # some items don't have the selected level id
                        
                        if selected_level in itemParent \
                            and  itemParent[selected_level]==item[selected_level]:

                            temp_path1=list(local_data["path"])
                            temp_path2=list(local_data["path"])

                            
                            ExcelJsonHelper.create_path(temp_path1,itemParent)
                            
                            
                            ExcelJsonHelper.recursivity_json_path(temp_path2,itemParent, item, data_type)
            
                    
                if local_data["name"] in child_grouper.keys():
                    del child_grouper[local_data["name"]]  # memory clean


        print("Step 3 finished (Joining and linking)")


        DELETE_THIS=["eid","trt_name", "wst_id", 'soil_id']

        

        ExcelJsonHelper.remove_keys_level(child_grouper,3,DELETE_THIS)

        ExcelJsonHelper.get_items_from_parent(child_grouper,["cul_name","crid"])

        ExcelJsonHelper.remove_keys_level(child_grouper,2,['crid','cul_name'], False)

        print("Step 4 finished json methods(deleting ids, get from parent)")
        

        #ExcelJsonHelper.validate_dates_format(child_grouper, Config.get_dates())

        ExcelJsonHelper.write_json(child_grouper,outputfile)