using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAppTest.Models
{
    public class MemberModel
    {
        public class DoLoginIn
        {
            public string UserID { get; set; }
            public string UserPwd { get; set; }
        }

        /// <summary>
        /// 登入回傳
        /// </summary>
        public class DoLoginOut
        {
            public string ErrMsg { get; set; }
            public string ResultMsg { get; set; }
        }
    }
}
