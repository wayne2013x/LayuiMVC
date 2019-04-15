using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Entity
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreateOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            IsDeleted = false;
        }
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateOn { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
