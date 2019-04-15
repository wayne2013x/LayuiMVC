using System;
using System.Linq;
using System.Text;

namespace Entity
{
    ///<summary>
    ///用户表
    ///</summary>
    public class User : BaseModel
    {
        public User()
        {


        }
        /// <summary>
        /// 自增长Id
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// 用户名/昵称
        /// </summary>           
        public string UserName { get; set; }

        /// <summary>
        /// 登录名/账号
        /// </summary>           
        public string LoginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>           
        public string UserPwd { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>           
        public string UserPhone { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>           
        public string UserEmail { get; set; }

        /// <summary>
        /// 状态
        /// </summary>           
        public bool? IsActived { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>           
        public string CreateOn { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>           
        public string CreateUser { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>           
        public bool? IsDeleted { get; set; }

    }
}
