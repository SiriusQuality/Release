  P  Ŕ   k820309    Ź          2021.8.0    éAd                                                                                                          
       C:\Users\raihauti\Documents\SiriusApril\SiriusCode\Code\MIN3P\icesheet\m_ice_sheet.F90 M_ICE_SHEET                                                        u #CREATE_ICE_SHEET                                                           u #DESTROY_ICE_SHEET                                                          u #SET_ICE_SHEET                                                          u #COMPUTE_PICE_ICE_SHEET                                                           u #COMPUTE_PW_ICE_SHEET    #COMPUTE_PW_ICE_SHEET_B                                                           u #COMPUTE_DPICEDT_ICE_SHEET                                                           u #READ_ICE_SHEET_BLOCK_ICE_SHEET                                                           u #GET_NEW_BC_ICE_SHEET 	   #GET_NEW_BC_ICE_SHEET_B 
                                                          u #MODIFY_FOR_PERMAFROST_K_ICE_SHEET    #MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET                                                           u #WRITE_ICE_SHEET                                                          u #GIVE_STAGE_ICE_SHEET    #GIVE_STAGE_ICE_SHEET_B                                                          u #CHECK_POINTER_VR    #CHECK_POINTER_AR    #CHECK_POINTER_VI    #CHECK_POINTER_VLOGICAL               @                                     
                  @                                                        @                                     
                  @                                                               @                                'H                   #NAME    #A    #B    #LOGKXX    #LOGKYY    #LOGKZZ    #TEMP    #LSOURCE     #ZSOURCE !   #DENSICE "   #DENSFRESH #   #NSTAGES $   #ISBC %   #TIMESTAGES &   #HSTAGES '   #LSTAGES (   #THICKPERMSTAGES )   #L1PERMSTAGES *   #L2PERMSTAGES +   #HSLOPE ,   #LSLOPE -   #FACPW .   #FACPICE /   #L1PERMSLOPE 0   #L2PERMSLOPE 1   #THICKPERMSLOPE 2   #ISCONSTANT 3                                                   d                                                                      h          
                                                   p          
                                                   x          
                                                             
                                                             
                                                             
                                                              
                                              !             	   
                                              "     ¨       
   
                                              #     °          
                                               $     ¸                                                       %            Ŕ                             &                                                                                    &                            
            &                   &                                                                                    '            h                
            &                   &                                                                                    (            Č                
            &                   &                                                                                    )            (                
            &                   &                                                                                    *                            
            &                   &                                                                                    +            č                
            &                   &                                                                                    ,            H                
            &                                                                                    -                            
            &                                                                                    .            Ř                
            &                                                                                    /                             
            &                                                                                    0            h                
            &                                                                                    1            °                
            &                                                                                    2            ř                
            &                                                                                       3     @            #         @     X                                                 #THIS 4             
D                                 4     H              #T_ICE_SHEET    #         @     X                                                 #THIS 5             
D @                               5     H              #T_ICE_SHEET    #         @     X                                                #THIS 6   #NAME 7   #LSOURCE 8   #ZSOURCE 9   #A :   #B ;   #LOGKXX <   #LOGKYY =   #LOGKZZ >   #TEMP ?   #DENSICE @   #DENSFRESH A   #NSTAGES B   #ISBC C   #TIMESTAGES D   #HSTAGES E   #LSTAGES F   #FACPW G   #FACPICE H   #L1PERMSTAGES I   #L2PERMSTAGES J   #THICKPERMSTAGES K   #ISCONSTANT L   #ISERROR M             
D @                               6     H              #T_ICE_SHEET              
                                7                    1           
                                 8     
                
                                 9     
                
                                 :     
                
                                 ;     
                
                                 <     
                
                                 =     
                
                                 >     
                
                                 ?     
                
                                 @     
                
                                 A     
                
@ @                               B                    
                                  C                        p          5  p        r B       5  p        r B                              
                                 D                    
    p          p          5  p        r B       p          5  p        r B                              
                                 E                    
    p          p          5  p        r B       p          5  p        r B                              
                                 F                    
    p          p          5  p        r B       p          5  p        r B                              
                                 G                    
    p          5  p        r B       5  p        r B                              
                                 H                    
    p          5  p        r B       5  p        r B                              
                                 I                    
    p          p          5  p        r B       p          5  p        r B                              
                                 J                    
    p          p          5  p        r B       p          5  p        r B                              
                                 K                    
    p          p          5  p        r B       p          5  p        r B                               
                                  L                     D                                 M            #         @     X                                                #THIS N   #PICE O   #TIME P   #TIMEB Q   #L R   #TYPEDATA S   #ISERROR T             
                                  N     H             #T_ICE_SHEET              D                                O     
                 
                                 P     
                
                                 Q     
                
                                 R     
                
                                S                    1           D                                 T            #         @     X                                                 #THIS U   #PW V   #TIME W   #L X   #Z Y   #TYPEDATA Z   #ISERROR [             
  @                               U     H             #T_ICE_SHEET              D                                V     
                 
  @                              W     
                
                                 X     
                
                                 Y     
                
                                Z                    1           D                                 [            #         @     X                                                 #THIS \   #PW ]   #TIME ^   #TIMEB _   #L `   #Z a   #TYPEDATA b   #ISERROR c             
  @                               \     H             #T_ICE_SHEET              D                                ]     
                 
  @                              ^     
                
  @                              _     
                
                                 `     
                
                                 a     
                
                                b                    1           D                                 c            #         @     X                                              
   #THIS d   #DPICEDT e   #TIME1 f   #TIME2 g   #L h   #STORCOEFF i   #DENSITY j   #BETA k   #TYPEDATA l   #ISERROR m             
  @                               d     H             #T_ICE_SHEET              D                                e     
                 
  @                              f     
                
  @                              g     
                
  @                              h     
                
                                 i     
                
                                 j     
                
                                 k     
                
  @                             l                    1           D @                               m            #         @     X                                                 #THIS n   #IDAT o   #ISTEMP p   #ICETIMELINE q   #ISERROR r             
D @                               n     H              #T_ICE_SHEET              
@ @                               o                     
                                  p                   D @                              q                   
 $              &                   &                                                     D @                               r            #         @     X                             	                    #THIS s   #IVOLBC t   #VALUEBC v   #IVOLBC1 w   #VALUEBC1 x   #NVOLBC u   #XVOL y   #ZVOL {   #NVOL z   #TIME |   #ISERROR }             
  @                               s     H             #T_ICE_SHEET             
D                                 t                         p          5  p        r u       5  p        r u                              
D                                v                    
     p          5  p        r u       5  p        r u                              
D                                 w                         p          5  p        r u       5  p        r u                              
D                                x                    
     p          5  p        r u       5  p        r u                               
D                                 u                     
                                 y                    
    p          5  p 	       r z       5  p 	       r z                              
                                 {                    
    p          5  p 	       r z       5  p 	       r z                               
                                  z                     
  @                              |     
                D                                 }            #         @     X                             
                    #THIS ~   #IVOLBC    #VALUEBC    #IVOLBC1    #VALUEBC1    #NVOLBC    #XVOL    #ZVOL    #NVOL    #TIME    #TIMEB    #ISERROR              
  @                               ~     H             #T_ICE_SHEET             
D                                                          p          5  p        r        5  p        r                               
D                                                    
     p          5  p        r        5  p        r                               
D                                                           p          5  p        r        5  p        r                               
D                                                    
 !    p          5  p        r        5  p        r                                
D                                                      
                                                     
 "   p          5  p 	       r        5  p 	       r                               
                                                     
 #   p          5  p 	       r        5  p 	       r                                
                                                       
  @                                   
                
  @                                   
                D                                             #         @     X                                                 #THIS    #KXX    #KYY    #KZZ    #NCELLS    #TIME    #X    #Z    #NTHREADS    #NUMOFLOOPS_THRED    #ISERROR              
  @                                    H             #T_ICE_SHEET             
D                                                    
 .    p          5  p        r        5  p        r                               
D                                                    
 /    p          5  p        r        5  p        r                               
D                                                    
 0    p          5  p        r        5  p        r                                
                                                       
  @                                   
               
                                                     
 1   p          5  p        r        5  p        r                               
                                                     
 2   p          5  p        r        5  p        r                                
                                                      
                                                      D                                             #         @     X                                                 #THIS    #TEMP    #X    #Z    #TIME    #ISERROR              
  @                                    H             #T_ICE_SHEET              
D                                     
                 
                                      
                
                                      
                
  @                                   
                D                                             #         @     X                                                 #THIS    #IOUTPUT    #ISERROR              
                                       H             #T_ICE_SHEET              
                                                       D                                             #         @     X                                                #THIS    #ISTAGE    #TIME     #ISBE Ą             
                                       H             #T_ICE_SHEET              D                                                       
                                       
                D                                 Ą            #         @     X                                                #THIS ˘   #ISTAGE Ł   #TIME ¤   #TIMEB Ľ   #ISBE Ś             
                                  ˘     H             #T_ICE_SHEET              D                                 Ł                      
                                 ¤     
                
                                 Ľ     
                D                                 Ś            #         @     X                                                #PA §   #NDIM ¨   #ISALLOCATE Š            DP                              §                   
 5              &                                                     
                                 ¨                     
                                  Š           #         @     X                                                #PA Ş   #NDIM1 Ť   #NDIM2 Ź   #ISALLOCATE ­            DP                              Ş                   
 6              &                   &                                                                                     Ť                                                      Ź                                                       ­            #         @     X                                                 #PA Ž   #NDIM Ż   #ISALLOCATE °            DP                              Ž                    3              &                                                     
                                 Ż                     
                                  °           #         @     X                                                #PA ą   #NDIM ˛   #ISALLOCATE ł            DP                               ą                    4              &                                                     
                                 ˛                     
                                  ł                  k      fn#fn      V       gen@CREATE_    a  W       gen@DESTROY_    ¸  S       gen@SET_ "     \       gen@COMPUTE_PICE_     g  v       gen@COMPUTE_PW_ %   Ý  _       gen@COMPUTE_DPICEDT_ *   <  d       gen@READ_ICE_SHEET_BLOCK_        v       gen@GET_NEW_BC_ +            gen@MODIFY_FOR_PERMAFROST_    §  U       gen@WRITE_     ü  v       gen@GIVE_STAGE_ -   r         gen@CHECK_POINTER_ICE_SHEET_      @       MAX_UPDATE_PW "   P  @       B_CHECK_UPDATE_PW       @       MAX_UPDATE_PICE $   Đ  @       B_CHECK_UPDATE_PICE      ź      T_ICE_SHEET !   Ě  P   a   T_ICE_SHEET%NAME    	  H   a   T_ICE_SHEET%A    d	  H   a   T_ICE_SHEET%B #   Ź	  H   a   T_ICE_SHEET%LOGKXX #   ô	  H   a   T_ICE_SHEET%LOGKYY #   <
  H   a   T_ICE_SHEET%LOGKZZ !   
  H   a   T_ICE_SHEET%TEMP $   Ě
  H   a   T_ICE_SHEET%LSOURCE $     H   a   T_ICE_SHEET%ZSOURCE $   \  H   a   T_ICE_SHEET%DENSICE &   ¤  H   a   T_ICE_SHEET%DENSFRESH $   ě  H   a   T_ICE_SHEET%NSTAGES !   4     a   T_ICE_SHEET%ISBC '   Č  Ź   a   T_ICE_SHEET%TIMESTAGES $   t  Ź   a   T_ICE_SHEET%HSTAGES $      Ź   a   T_ICE_SHEET%LSTAGES ,   Ě  Ź   a   T_ICE_SHEET%THICKPERMSTAGES )   x  Ź   a   T_ICE_SHEET%L1PERMSTAGES )   $  Ź   a   T_ICE_SHEET%L2PERMSTAGES #   Đ     a   T_ICE_SHEET%HSLOPE #   d     a   T_ICE_SHEET%LSLOPE "   ř     a   T_ICE_SHEET%FACPW $        a   T_ICE_SHEET%FACPICE (         a   T_ICE_SHEET%L1PERMSLOPE (   ´     a   T_ICE_SHEET%L2PERMSLOPE +   H     a   T_ICE_SHEET%THICKPERMSLOPE '   Ü  H   a   T_ICE_SHEET%ISCONSTANT !   $  R       CREATE_ICE_SHEET &   v  Y   a   CREATE_ICE_SHEET%THIS "   Ď  R       DESTROY_ICE_SHEET '   !  Y   a   DESTROY_ICE_SHEET%THIS    z  }      SET_ICE_SHEET #   ÷  Y   a   SET_ICE_SHEET%THIS #   P  L   a   SET_ICE_SHEET%NAME &     @   a   SET_ICE_SHEET%LSOURCE &   Ü  @   a   SET_ICE_SHEET%ZSOURCE       @   a   SET_ICE_SHEET%A     \  @   a   SET_ICE_SHEET%B %     @   a   SET_ICE_SHEET%LOGKXX %   Ü  @   a   SET_ICE_SHEET%LOGKYY %     @   a   SET_ICE_SHEET%LOGKZZ #   \  @   a   SET_ICE_SHEET%TEMP &     @   a   SET_ICE_SHEET%DENSICE (   Ü  @   a   SET_ICE_SHEET%DENSFRESH &     @   a   SET_ICE_SHEET%NSTAGES #   \  ´   a   SET_ICE_SHEET%ISBC )     Ô   a   SET_ICE_SHEET%TIMESTAGES &   ä  Ô   a   SET_ICE_SHEET%HSTAGES &   ¸  Ô   a   SET_ICE_SHEET%LSTAGES $     ´   a   SET_ICE_SHEET%FACPW &   @  ´   a   SET_ICE_SHEET%FACPICE +   ô  Ô   a   SET_ICE_SHEET%L1PERMSTAGES +   Č   Ô   a   SET_ICE_SHEET%L2PERMSTAGES .   !  Ô   a   SET_ICE_SHEET%THICKPERMSTAGES )   p"  @   a   SET_ICE_SHEET%ISCONSTANT &   °"  @   a   SET_ICE_SHEET%ISERROR '   đ"         COMPUTE_PICE_ICE_SHEET ,   #  Y   a   COMPUTE_PICE_ICE_SHEET%THIS ,   Ü#  @   a   COMPUTE_PICE_ICE_SHEET%PICE ,   $  @   a   COMPUTE_PICE_ICE_SHEET%TIME -   \$  @   a   COMPUTE_PICE_ICE_SHEET%TIMEB )   $  @   a   COMPUTE_PICE_ICE_SHEET%L 0   Ü$  L   a   COMPUTE_PICE_ICE_SHEET%TYPEDATA /   (%  @   a   COMPUTE_PICE_ICE_SHEET%ISERROR %   h%         COMPUTE_PW_ICE_SHEET *   ő%  Y   a   COMPUTE_PW_ICE_SHEET%THIS (   N&  @   a   COMPUTE_PW_ICE_SHEET%PW *   &  @   a   COMPUTE_PW_ICE_SHEET%TIME '   Î&  @   a   COMPUTE_PW_ICE_SHEET%L '   '  @   a   COMPUTE_PW_ICE_SHEET%Z .   N'  L   a   COMPUTE_PW_ICE_SHEET%TYPEDATA -   '  @   a   COMPUTE_PW_ICE_SHEET%ISERROR '   Ú'         COMPUTE_PW_ICE_SHEET_B ,   r(  Y   a   COMPUTE_PW_ICE_SHEET_B%THIS *   Ë(  @   a   COMPUTE_PW_ICE_SHEET_B%PW ,   )  @   a   COMPUTE_PW_ICE_SHEET_B%TIME -   K)  @   a   COMPUTE_PW_ICE_SHEET_B%TIMEB )   )  @   a   COMPUTE_PW_ICE_SHEET_B%L )   Ë)  @   a   COMPUTE_PW_ICE_SHEET_B%Z 0   *  L   a   COMPUTE_PW_ICE_SHEET_B%TYPEDATA /   W*  @   a   COMPUTE_PW_ICE_SHEET_B%ISERROR *   *  ˝       COMPUTE_DPICEDT_ICE_SHEET /   T+  Y   a   COMPUTE_DPICEDT_ICE_SHEET%THIS 2   ­+  @   a   COMPUTE_DPICEDT_ICE_SHEET%DPICEDT 0   í+  @   a   COMPUTE_DPICEDT_ICE_SHEET%TIME1 0   -,  @   a   COMPUTE_DPICEDT_ICE_SHEET%TIME2 ,   m,  @   a   COMPUTE_DPICEDT_ICE_SHEET%L 4   ­,  @   a   COMPUTE_DPICEDT_ICE_SHEET%STORCOEFF 2   í,  @   a   COMPUTE_DPICEDT_ICE_SHEET%DENSITY /   --  @   a   COMPUTE_DPICEDT_ICE_SHEET%BETA 3   m-  L   a   COMPUTE_DPICEDT_ICE_SHEET%TYPEDATA 2   š-  @   a   COMPUTE_DPICEDT_ICE_SHEET%ISERROR /   ů-         READ_ICE_SHEET_BLOCK_ICE_SHEET 4   .  Y   a   READ_ICE_SHEET_BLOCK_ICE_SHEET%THIS 4   Ř.  @   a   READ_ICE_SHEET_BLOCK_ICE_SHEET%IDAT 6   /  @   a   READ_ICE_SHEET_BLOCK_ICE_SHEET%ISTEMP ;   X/  ¤   a   READ_ICE_SHEET_BLOCK_ICE_SHEET%ICETIMELINE 7   ü/  @   a   READ_ICE_SHEET_BLOCK_ICE_SHEET%ISERROR %   <0  Ç       GET_NEW_BC_ICE_SHEET *   1  Y   a   GET_NEW_BC_ICE_SHEET%THIS ,   \1  ´   a   GET_NEW_BC_ICE_SHEET%IVOLBC -   2  ´   a   GET_NEW_BC_ICE_SHEET%VALUEBC -   Ä2  ´   a   GET_NEW_BC_ICE_SHEET%IVOLBC1 .   x3  ´   a   GET_NEW_BC_ICE_SHEET%VALUEBC1 ,   ,4  @   a   GET_NEW_BC_ICE_SHEET%NVOLBC *   l4  ´   a   GET_NEW_BC_ICE_SHEET%XVOL *    5  ´   a   GET_NEW_BC_ICE_SHEET%ZVOL *   Ô5  @   a   GET_NEW_BC_ICE_SHEET%NVOL *   6  @   a   GET_NEW_BC_ICE_SHEET%TIME -   T6  @   a   GET_NEW_BC_ICE_SHEET%ISERROR '   6  Ň       GET_NEW_BC_ICE_SHEET_B ,   f7  Y   a   GET_NEW_BC_ICE_SHEET_B%THIS .   ż7  ´   a   GET_NEW_BC_ICE_SHEET_B%IVOLBC /   s8  ´   a   GET_NEW_BC_ICE_SHEET_B%VALUEBC /   '9  ´   a   GET_NEW_BC_ICE_SHEET_B%IVOLBC1 0   Ű9  ´   a   GET_NEW_BC_ICE_SHEET_B%VALUEBC1 .   :  @   a   GET_NEW_BC_ICE_SHEET_B%NVOLBC ,   Ď:  ´   a   GET_NEW_BC_ICE_SHEET_B%XVOL ,   ;  ´   a   GET_NEW_BC_ICE_SHEET_B%ZVOL ,   7<  @   a   GET_NEW_BC_ICE_SHEET_B%NVOL ,   w<  @   a   GET_NEW_BC_ICE_SHEET_B%TIME -   ˇ<  @   a   GET_NEW_BC_ICE_SHEET_B%TIMEB /   ÷<  @   a   GET_NEW_BC_ICE_SHEET_B%ISERROR 2   7=  Â       MODIFY_FOR_PERMAFROST_K_ICE_SHEET 7   ů=  Y   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%THIS 6   R>  ´   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%KXX 6   ?  ´   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%KYY 6   ş?  ´   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%KZZ 9   n@  @   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%NCELLS 7   Ž@  @   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%TIME 4   î@  ´   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%X 4   ˘A  ´   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%Z ;   VB  @   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%NTHREADS C   B  @   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%NUMOFLOOPS_THRED :   ÖB  @   a   MODIFY_FOR_PERMAFROST_K_ICE_SHEET%ISERROR 5   C         MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET :   C  Y   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%THIS :   đC  @   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%TEMP 7   0D  @   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%X 7   pD  @   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%Z :   °D  @   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%TIME =   đD  @   a   MODIFY_FOR_PERMAFROST_TEMP_ICE_SHEET%ISERROR     0E  l       WRITE_ICE_SHEET %   E  Y   a   WRITE_ICE_SHEET%THIS (   őE  @   a   WRITE_ICE_SHEET%IOUTPUT (   5F  @   a   WRITE_ICE_SHEET%ISERROR %   uF  r       GIVE_STAGE_ICE_SHEET *   çF  Y   a   GIVE_STAGE_ICE_SHEET%THIS ,   @G  @   a   GIVE_STAGE_ICE_SHEET%ISTAGE *   G  @   a   GIVE_STAGE_ICE_SHEET%TIME *   ŔG  @   a   GIVE_STAGE_ICE_SHEET%ISBE '    H  }       GIVE_STAGE_ICE_SHEET_B ,   }H  Y   a   GIVE_STAGE_ICE_SHEET_B%THIS .   ÖH  @   a   GIVE_STAGE_ICE_SHEET_B%ISTAGE ,   I  @   a   GIVE_STAGE_ICE_SHEET_B%TIME -   VI  @   a   GIVE_STAGE_ICE_SHEET_B%TIMEB ,   I  @   a   GIVE_STAGE_ICE_SHEET_B%ISBE !   ÖI  j       CHECK_POINTER_VR $   @J     a   CHECK_POINTER_VR%PA &   ĚJ  @   a   CHECK_POINTER_VR%NDIM ,   K  @   a   CHECK_POINTER_VR%ISALLOCATE !   LK  v       CHECK_POINTER_AR $   ÂK  ¤   a   CHECK_POINTER_AR%PA '   fL  @   a   CHECK_POINTER_AR%NDIM1 '   ŚL  @   a   CHECK_POINTER_AR%NDIM2 ,   ćL  @   a   CHECK_POINTER_AR%ISALLOCATE !   &M  j       CHECK_POINTER_VI $   M     a   CHECK_POINTER_VI%PA &   N  @   a   CHECK_POINTER_VI%NDIM ,   \N  @   a   CHECK_POINTER_VI%ISALLOCATE '   N  j       CHECK_POINTER_VLOGICAL *   O     a   CHECK_POINTER_VLOGICAL%PA ,   O  @   a   CHECK_POINTER_VLOGICAL%NDIM 2   ŇO  @   a   CHECK_POINTER_VLOGICAL%ISALLOCATE 