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
    public class FaKaRechargeController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /FaKaRecharge/
        //返回输入IC卡号视图
        public ActionResult fakaRecharge()
        {
            return View();
        }

        //返回新户充值url地址
        [HttpPost]
        public String getUserByYwbh(yhxx postYhxx)
        {
            yhxx Yhxx = null;
            Yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.ywbh == postYhxx.ywbh);         
            if (Yhxx != null)
            {
                djgl djgl = baseDAL.Get<djgl>().FirstOrDefault(c => c.yhlx == Yhxx.yhlx);
                Session["ywbh"] = Yhxx.ywbh;
                Session["hm"] = Yhxx.hm;
                Session["zz"] = Yhxx.zz;
                Session["yhlxms"] = djgl.ms;
                Session["tel"] = Yhxx.tel;
                Session["gqcs"] = Yhxx.gqcs;
                Session["qj"] = djgl.qj;
                return "../SellCard/NewCardCharge";
            }
            else return "../FaKaRecharge/fakaRecharge";
        }

    }
}
