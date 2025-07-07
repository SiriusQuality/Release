
import re

class Validator:
    """
    A class to run all the process related to JSON validation. 
    
    It depens on a formed JSON and return error message
    """
    @classmethod
    def validate_dates(cls,element,dates_attributes,message=""):
        if isinstance(element, dict):
            for key_date in dates_attributes:
                if key_date in element:
                    current_value = element[key_date]
                    if cls.validate_format_dates(current_value):
                        message = message + "date format error in %s"%key_date
            for key, value in element.items():
                cls.validate_dates(value)

        if isinstance(element, list):
            for item in element:
                cls.validate_dates(item)
    
    
    """
    @return boolean
    """
    @classmethod
    def validate_format_dates(cls,date_attr):
        regex = re.compile('[1-2]+[0-9]+[0-9]+[0-9]+[0-9]+[0-9]+[0-9]+[0-9]+', re.I)
        match = regex.match(str(date_attr))
        return bool(match)

        
