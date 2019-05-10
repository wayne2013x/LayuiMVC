using System;
using System.Linq;
using System.Text;

namespace Entity
{
    ///<summary>
    ///
    ///</summary>
    public class Sys_MenuFunction : BaseModel
    {
           public Sys_MenuFunction(){


           }
           /// <summary>
		   /// 
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public int? MenuId {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public int? FunctionId {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public DateTime? CreateTime {get;set;}

    }
}
