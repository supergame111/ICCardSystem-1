using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ICCardSystem.Models;
using ICCardSystem.Models.VModels;
using ICCardSystem.Utility;
namespace ICCardSystem.Controllers
{
    public class ClearLastRechargeController : Controller
    {
        //
        // GET: /ClearLastRecharge/
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //返回处理视图
        public ActionResult YuChongZhangDeal()
        {
            return View();
        }
        //返回处理结果
        [HttpPost]
        public string YuChongZhangDeal(yysj yczInfo)
        {
            List<yysj> yysjList = baseDAL.Get<yysj>().
                Where(c => c.ywbh == yczInfo.ywbh).
                Where(c=>c.zfbz!=1).
                OrderBy(c=>c.skrq).ToList();
            yysj yysj_To_Change = yysjList[yysjList.Count - 1];
            yysj_To_Change.zfbz = 1;
            if (baseDAL.SaveAllChanges())
                return "此账已处理成功！";
            else
                return "此帐处理失败！";
        }
    }
}
