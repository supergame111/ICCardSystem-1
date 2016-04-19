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
    public class RechargeController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /Recharge/
        //返回充值界面
        public ActionResult chongzhi()
        {

            return View();
        }

        //返回充值结果
        [HttpPost]
        public string chongzhi(SellInfo sellInfo)
        {
            yhxx yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.ywbh == sellInfo.ywbh);
            yhxx.gqcs += 1;
            if (yhxx.gqzl == null)
                yhxx.gqzl = sellInfo.gql;
            else
                yhxx.gqzl += sellInfo.gql;
            yhxx.gqrq = DateTime.Now;
            //向营业数据表插入信息
            yysj newYysj = new yysj();
            newYysj.ywbh = sellInfo.ywbh;
            newYysj.skcs = baseDAL.Get<yysj>().Where(c => c.ywbh == sellInfo.ywbh).Count() + 1;
            newYysj.skrq = (DateTime)yhxx.gqrq;
            newYysj.skje = (decimal)sellInfo.qjMoney;
            newYysj.skql = sellInfo.gql;
            newYysj.czy = (string)Session["user_id"];

            newYysj.bb = 1;
            newYysj.hh = 1;
            newYysj.sjkb = 1;
            newYysj.time = DateTime.Now;
            newYysj.gzzbh = 1;
            newYysj.lstbz = 1;

            newYysj.bqbz = 0;
            newYysj.zfbz = 0;
            string fpbh = "IC0010701 ";
            fpbh += baseDAL.Get<yysj>().Count().ToString();
            newYysj.fpbh = fpbh;
            baseDAL.AddItem<yysj>(newYysj);
            //放入session以备打印
            Session["ywbhP"] = sellInfo.ywbh;
            Session["fpbhP"] = fpbh;
            Session["gqlP"] = sellInfo.gql;
            Session["qjMoneyP"] = sellInfo.qjMoney;
            Session["moneyTotalP"] = sellInfo.moneyTotal;
            Session["returnMoneyP"] = sellInfo.returnMoney;
            if (baseDAL.SaveAllChanges())
                return "充值成功！";
            else
                return "充值失败！";
        } 
    }
}
