using Common;
using System;
using System.Linq;
using System.Text;
using Entity;

namespace Application
{
    public class AccountLogic : BaseLogic<Sys_User>
    {
        public void Checked(string uName, string uPwd)
        {
            if (string.IsNullOrEmpty(uName))
                throw new MessageBox("请输入用户名");
            if (string.IsNullOrEmpty(uPwd))
                throw new MessageBox("请输入密码");

            var _User = this.clientdb.GetSingle(u => u.LoginName == uName);

            if (!_User.Id.ValidZero())
                throw new MessageBox("用户不存在");
            if (_User.UserPwd.Trim() != uPwd)//DesEncrypt.Decode(uPwd))
                throw new MessageBox("密码错误");
            //获取角色Id

            var _Account = new Account();
            //_Account.RoleID = _Sys_Role.Role_ID.ToGuid();
            _Account.UserID = _User.Id;
            _Account.UserName = _User.UserName;
            //如果是超级管理员 帐户
            //_Account.IsSuperManage = _Sys_Role.Role_ID == AppConfig.Admin_RoleID.ToGuid();

            BaseClass.SetSession("User", _Account);
        }
    }
}
