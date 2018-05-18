#include "stdafx.h"
#include <iostream>

//import the .tlb file create by COM registration

#import "C:\Documents and Settings\mabensalem\My Documents\Visual Studio 2010\Projects\CsharpRinerface\CsharpRinerface\bin\Release\CsharpRinerface.tlb.tlb" 

    void _cdecl MyCPPFunction(char ** strName)
    {
        //Initialize COM
        HRESULT hr = CoInitialize(NULL);

        //create COM interface pointer
        MyNamespace::MyInterfacePtr myPtr;

        //create instance of COM class using the interface pointer
        HRESULT hr2 = myPtr.CreateInstance(MyNamespace::CLSID_MyClass);

       //create variable to hold output from COM.
       BSTR output;

       //call the COM function
       myPtr->MyFunction(_bstr_t(strName[0]), &output);

       //convert the returned BSTR from .net into char*
       int length = (int) SysStringLen(output);
       char *tempBuffer;
       tempBuffer = (char *) malloc(1 + length);
       WideCharToMultibyte(CP_ACP, 0, output, -1, tempBuffer, length, NULL, NULL);
       tempBuffer[length] = '\0';

       //release interface
       myPtr->Release();

       //uninitialize COM
       if(hr == S_OK)
         CoUninitialize();

       //set output in input for returning to R (this is weird but the only way I could make it work)
       strName[0] = tempBuffer;
    }