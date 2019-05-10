using System;
using System.Linq;
using System.Text;

namespace Entity
{
    ///<summary>
    ///
    ///</summary>
    public class Sys_Function : BaseModel
    {
           public Sys_Function(){


           }
           /// <summary>
		   /// 
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Function_Num {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Function_Name {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Function_ByName {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public DateTime? Function_CreateTime {get;set;}

    }
}
