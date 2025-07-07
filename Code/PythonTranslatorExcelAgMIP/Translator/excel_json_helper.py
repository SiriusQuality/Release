#!/usr/bin/python3

import json 
import datetime
from typing import Dict, List


## returns the ne
def rename(renames, header):
    found_rename = next((rename_config for rename_config in renames \
        if rename_config["name"] == header), None)

    if found_rename: 
        return found_rename["newName"] 
    return header

def renames_function(renames, headers):
    return [rename(renames,header) for header in headers]
        

map_functions={
    "toLowerCase" :  lambda x: x.lower(),
    "reduceByIds" : lambda x_list: list({frozenset(item.items()) : item for item in x_list}.values())
}
SEPARATOR=","

class ExcelJsonHelper:
    """A class helper with some methods for specific pruporses related to xslx to json manipulation """
    
    @classmethod
    def simple_template(cls,key_name):
        return "\""+key_name+"\":{}"
        
    # create a dictionary to grouped all the items linked with an ID field_id, weather_id or soil_id
    # it is to avoid search in every iteration the elements with the specific ID
    @classmethod
    def group_by_linker(cls,list_objects):
        TRT_COLUMN= 0
        temp_map={}
        for x in list_objects:

            try:
                columnid= list(x.keys())[TRT_COLUMN]
                trtid=x[columnid] # assuming that linked id is in column 0 
                if trtid in temp_map:
                    temp_map[trtid]=temp_map[trtid]+ [x]
                else: 
                    temp_map[trtid]=[x]
            except IndexError:
                print("index error: current value is not a key. Hint: review completly each sheet to find additional cells filled ")
                print(x, TRT_COLUMN, trtid, columnid)
                print(list_objects)
                exit()
            
        return temp_map
        

    ##read an excel sheet and map to a json object list 
    @classmethod
    def get_data_json_like(cls,data,excel_helper):
        
        sheet_name=data.get('sheetName')

        PLOT_CONFIG= "experiments"
        config_name = data.get('name')

        header_row=int(data.get('headerRow'))

        ## functions
        header_transformation=data.get('transformHeader')
        set_transformation = data.get('transformSet')
        take_subset = data.get('takeSubset')
        exclude_subset = data.get('excludeSubset')
        rename_header = data.get('renameHeader') # list of thing to rename
        new_columns = data.get('newColumns')
        

        is_open_sheet= excel_helper.activate_sheet(sheet_name)

        if not is_open_sheet: 
            return []
        #validate that the first row is not empty
        if(excel_helper.validate_empty_cell(header_row)):
            print ("validation error")
            return []
        
        
        header_cells=excel_helper.get_row_values(header_row) # get atribute names using the header row        
        header_cells=list(map( map_functions[header_transformation], header_cells)) #pass to lower case header row
        if rename_header:
            header_cells= renames_function(rename_header,header_cells)

        
        json_template=list(map( cls.simple_template , header_cells)) # template for creating json data key:{%s} format 
        
        json_template=SEPARATOR.join( json_template )
        
        start=header_row+1
        end = excel_helper.get_last_row_index()
        
        json_list_experiments=[]
        
        for i in range(start, end): # based on the template create row values
            values=excel_helper.get_row_values(i)
            types=excel_helper.get_row_types(i)
            
            # format date types
            for index,x in enumerate(types):
                
                if(x==excel_helper.XL_CELL_DATE):

                    myDate=datetime.datetime(*excel_helper.date_as_tuple(values[index]))
                    myDateStr="{:%Y%m%d}".format(myDate)

                    myDateStr= "\"%s\""%myDateStr
                    
                    values[index]=myDateStr
                    

                elif(x==excel_helper.XL_CELL_TEXT):
                    values[index]= values[index].replace("\n", " ")
                    values[index]= "\"%s\""%values[index]

                elif(x==excel_helper.XL_CELL_EMPTY):
                    values[index]= "\"\""

                elif(x==excel_helper.XL_CELL_NUMBER):
                    if values[index].is_integer():
                        values[index]=int(values[index])
                    values[index]= "\"%s\""%values[index]


            new_row=json_template.format(*values)
            
           
            new_row_dic= json.loads("{%s}"%new_row)
           
            # remove keys that have empty values 
            new_row_dic_filtred = dict(filter(lambda elem: elem[1]  != '', new_row_dic.items()))

            
            if take_subset is not None:
                new_row_dic_filtred = dict(filter(lambda elem: elem[0] in take_subset , new_row_dic_filtred.items()))
            
            if exclude_subset is not None:
                new_row_dic_filtred = dict(filter(lambda elem: elem[0] not in exclude_subset , new_row_dic_filtred.items()))

            if new_columns is not None:
                for new_column in new_columns: 
                    col= new_column["name"]
                    from_col_conf=new_column["from"]
                    from_col_value=new_row_dic_filtred[from_col_conf]
                    new_row_dic_filtred.update({col:from_col_value})

            json_list_experiments.append(new_row_dic_filtred)

           
        
        if set_transformation: # only for plot
            
            reduce_list =  map_functions[set_transformation](json_list_experiments)

            if config_name == PLOT_CONFIG:
                for item in reduce_list:
                    
                    item.update( {"plot_id":item[take_subset[0]]+"-"+ item[take_subset[1]]} )
                    
            return reduce_list
        return json_list_experiments
    
    
    @classmethod
    def create_path(cls,keys_list, dictionary): # dictionary is updated

        if len(keys_list)==0:
            return 
        key=keys_list.pop(0)
        if  key in dictionary :
            return cls.create_path(keys_list, dictionary[key])
        else:
            dictionary[key]={}
            return cls.create_path(keys_list, dictionary[key])

    @classmethod
    def write_json(cls,my_dict,outputfile):
        my_json = json.dumps(my_dict)
        
        f = open(outputfile,"w")
        f.write(my_json)
        f.close()

        print("json file created")      

    ## create json structure
    ## previously I know that the path exists create_path method
    @classmethod
    def recursivity_json_path(cls,path_list, json_object, value, type_item ):
        
        item= path_list.pop(0) # path_list len should be >= 1
        # if item not in json_object:
        #     json_object[item]={}
        
        if len(path_list)==0: # it was the last item
            if type_item=="map" :
                json_object[item].update(value)
            elif type_item=="list": 
                if isinstance(json_object[item], dict): # because all the elements are instanced as dict
                    json_object[item]=[]
                json_object[item].append(value.copy())
            
        else:
            cls.recursivity_json_path(path_list, json_object[item],value, type_item)

    # read entities that are in several sheets 
    @classmethod
    def several_sheets_reader(cls,sheets_list_names, base_data,  excel_helper, apply_func=''):
        listObjects=[]

        for index,sheetName in enumerate(sheets_list_names):
            local_data={}
            local_data["sheetName"]=sheetName
            local_data['headerRow']=base_data['headerRow']
            local_data['transformHeader']=base_data['transformHeader']
            local_data['renameHeader']=base_data.get('renameHeader')

            more_settings=  base_data.get("sheetConfig")
            if more_settings is not None:
                sheet_settings= more_settings.get(sheetName)
                if sheet_settings is not None:
                    local_data['takeSubset']=sheet_settings.get('takeSubset')
                    local_data['excludeSubset']=sheet_settings.get('excludeSubset')
                    local_data['renameHeader']=sheet_settings.get('renameHeader')

            sheetElementsList=cls.get_data_json_like(local_data,excel_helper)

            if apply_func=="addEvents" :
                sheetElementsList=cls.add_events(sheetElementsList ,base_data["eventsName"][index])

            if sheetName in ["Plots","Planting","Genotypes"]:
                listObjects=cls.flatten_planting_events(listObjects , sheetElementsList)
            else:
                listObjects=listObjects + sheetElementsList

            print(sheetName, len(listObjects))
        
        return listObjects
    @classmethod
    def add_events(cls,myList, event_name):
        def loc_fun(x):# add the event name to each element as other value
            x["event"]= event_name 
            return x

        myList= list(map(loc_fun , myList))
        return myList


    # given two dictionaries check if for any of the ids they are equal
    @classmethod
    def validate_multilevel_ids(cls,ids, element1,element2):
        for id in ids:
            if id in element1 and id in element2:
                if element1[id]==element2[id]:
                    print("is true",id)
                    return True
        
        return False
                

    # from a list of sheets configuration like events in config.py 
    # 
    @classmethod
    def flatten_planting_events(cls,accum_list,sheet_list):

        EVENT_TYPE = "planting"
        IDS = [ "treat_id","eid", "cul_name"] # multilevel logic
        # assumption only there is a plating event
         
        
        for sheet_element in sheet_list:
            
            if(sheet_element["event"]==EVENT_TYPE):
                # print("sheet element",sheet_element)
                # print("current events",accum_list[0])
                find_index= next(index for index, accum_element in enumerate(accum_list) \
                    if cls.validate_multilevel_ids(IDS,accum_element,sheet_element))
                    

                if find_index is None: 
                    accum_list.append(sheet_element)
                else:
                    if IDS[0] in sheet_element: del sheet_element[IDS[0]]
                    if IDS[1] in sheet_element: del sheet_element[IDS[1]]
                    accum_list[find_index]= {**sheet_element, **accum_list[find_index]}
            else:
                accum_list.append(sheet_element)

        return accum_list     


    # a function to delete keys from an specific depth
    @classmethod
    def remove_keys_level(cls,element, starting_level, delete_keys, recursive=True):
        if isinstance(element, dict):
            if starting_level<=0: # start deleting 
                
                for delete_key in delete_keys:
                    if delete_key in element:
                        del element[delete_key]
                
                if not recursive: # to avoid delete the keys in the internal nodes
                    
                    return


            for key, value in element.items():
                cls.remove_keys_level(value, starting_level-1, delete_keys,recursive)
        
        elif isinstance(element, list):
            for item in element:
                cls.remove_keys_level(item, starting_level-1, delete_keys,recursive)
        
        else: return


    # a function to get keys and values from the parents
    @classmethod
    def get_items_from_parent(cls,current_elements, keys, keys_values_parent=dict()):

        KEY_EVENT="event"
        keys_values_parent = keys_values_parent.copy() # hack memory issue
        
        
        if KEY_EVENT in current_elements and current_elements[KEY_EVENT]=="planting":
            current_elements.update(keys_values_parent)
        else:
            if isinstance(current_elements, list):
                
                for value in current_elements:
                    cls.get_items_from_parent(value,keys, keys_values_parent)
            elif isinstance(current_elements, dict): 
                
                extracted_dic=cls.search_keys_in_dict(current_elements,keys)
                
                keys_values_parent.update(extracted_dic)
                for key,value in current_elements.items():
                    if isinstance(value,dict) or isinstance(value,list) :
                        cls.get_items_from_parent(value,keys, keys_values_parent) 
    
    @classmethod
    def search_keys_in_dict(cls,element:Dict,keys:List):
        
        result = dict()
        for key in keys:
            if key in element:
                value= element[key]
                result.update({key:value})
        return result