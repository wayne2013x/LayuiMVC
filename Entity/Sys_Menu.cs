using System;
using System.Linq;
using System.Text;

namespace Entity
{
    ///<summary>
    ///
    ///</summary>
    public class Sys_Menu
    {
           public Sys_Menu(){


           }
           /// <summary>
		   /// 
           /// </summary>           
           public int Id {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Menu_Num {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Menu_Name {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Menu_Url {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Menu_Icon {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public int? Menu_ParentID {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public string Menu_IsShow {get;set;}

           /// <summary>
		   /// 
           /// </summary>           
           public DateTime? Menu_CreateTime {get;set;}

    }
}
