
import xlrd 
import sys

def close():
   sys.exit(2)

class ExcelHelper:
    """A class helper to manage excel files"""
    def __init__(self):
        self.XL_CELL_DATE= xlrd.XL_CELL_DATE
        self.XL_CELL_TEXT= xlrd.XL_CELL_TEXT
        self.XL_CELL_EMPTY = xlrd.XL_CELL_EMPTY
        self.XL_CELL_NUMBER=xlrd.XL_CELL_NUMBER

        self.sheets_names= []

    def load_file(self, excel_name):
        try:
            # Give the location of the file 
            loc = (excel_name) 
            # To open Workbook 
            self.xlWorkbook = xlrd.open_workbook(loc) 
            self.sheets_names= self.xlWorkbook.sheet_names()
        except OSError :
            print("File not found <%s>"%(excel_name))
            close()
        except xlrd.XLRDError:
            print("Fail to open the excel file <%s>"%(excel_name))
            close()
    
    def activate_sheet(self, sheet_name):
        if sheet_name in self.sheets_names:
            self.sheet = self.xlWorkbook.sheet_by_name(sheet_name) # open the sheet
            self.sheet_name=sheet_name
            return True
        else:
            #print("No sheet named <'%s'>"%(sheetName))
            self.sheet=None
            return False

    def get_sheets_names(self):
        return self.sheets_names

    def validate_sheet_name(self, sheet_name):
        return False

    def validate_header_row(self, header_row):
        return False
    def get_row_values(self, row):
        return self.sheet.row_values(row)
    def get_row_types(self,row):
        return self.sheet.row_types(row)
        
    def get_last_row_index(self):
        return self.sheet.nrows 

    # return true when find some empty cell
    # search if some cell is empty
    def validate_empty_cell(self, row_index):
        
        FIRST_ROW=1 # row that contains the ids 
        LAST_COLUMN = self.sheet.ncols - 1
        def some_header_cell_empty(index_column=0):
            cell_type=self.sheet.cell_type(row_index, index_column)

            is_cell_empty= cell_type == xlrd.XL_CELL_EMPTY 
            if is_cell_empty:
                print(row_index, index_column)
                return True # there is an empty cell
            if(LAST_COLUMN==index_column):
                return False # there is not empty cell
            return some_header_cell_empty(index_column+1)    
            
        validation=some_header_cell_empty()
        if(validation):
            print("There is an empty cell in header "+self.sheet_name)
            
        return validation
    def date_as_tuple(self,value ):
        return xlrd.xldate_as_tuple(value, self.xlWorkbook.datemode)
