using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Application
{
    public class MenuLogic:BaseLogic<Sys_Menu>
    {
        public string CreateMenu(int menuId)
        {
            StringBuilder str = new StringBuilder();
            //var menulist = this.GetAllList();
            var menulist = clientdb.AsQueryable().ToList();
            var parentlist = new List<Sys_Menu>();

            if (menuId == 0)
                parentlist = menulist.Where(m => m.Menu_ParentID == null || m.Menu_ParentID == 0).ToList();
            else
                parentlist = menulist.Where(m => m.Menu_ParentID == menuId).ToList();

            if (parentlist.Count > 0)
            {
                
                foreach (var menu in parentlist)
                {
                    str.Append("<li data-name='app' class='layui-nav-item'>");
                    str.Append("<a lay-tips='" + menu.Menu_Name + "'>");
                    str.Append("<i class='layui-icon "+ menu.Menu_Icon + "'></i>");
                    str.Append("<cite>"+ menu.Menu_Name+"</cite>");
                    str.Append(" <span class='layui-nav-more'></span></a>");
                    
                    var childmenu = menulist.FindAll(m => m.Menu_ParentID == menu.Id);
                    foreach (var cmenu in childmenu)
                    {
                        str.Append(@"<dl class='layui-nav-child'><dd><a id = 'menu_id_" + cmenu.Id + "' data-id='" + cmenu.Id + "' class='three_menu_name' lay-href='"+ cmenu.Menu_Url+"'>"+ cmenu.Menu_Name+"</a></dd></dl>");
                    }
                    str.Append("</li>");
                }
            }
            return str.ToString();
        }
    }
}
