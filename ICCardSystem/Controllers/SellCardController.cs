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
    public class SellCardController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        
        //
        // GET: /SellCard/
        //返回查询视图
        public ActionResult FindNewUser()
        {
            var djglList = baseDAL.Get<djgl>().ToList();
            var jdlxList = baseDAL.Get<jdlx>().ToList();
            ViewBag.djglList = djglList;
            ViewBag.jdlxList = jdlxList;
            return View();
        }

        //返回查询结果
        [HttpPost]
        public ActionResult FindNewUser(SearchConditionView searchCondition)
        {
            List<yhxx> yhxxlist = null;
            
            
            if (searchCondition.Condition1 != null )
            {
                yhxxlist = baseDAL.Get<yhxx>().Where(c => c.zz.Contains(searchCondition.Condition1) &&
                    c.zz.Contains(searchCondition.Condition2) && c.zz.Contains(searchCondition.Condition3)).ToList();
            }
            else
            {
                int number = int.Parse(searchCondition.Condition2);
                yhxxlist = baseDAL.Get<yhxx>().Where(c => c.kpbh == number).ToList();
            }

            //ViewBag.yhxxlist = yhxxlist;
            
            
            return Json(yhxxlist)  ;
        }

        //修改资料
        [HttpPost]
        public JsonResult Update(yhxx newYhxx)
        {
            var newUser = baseDAL.Get<yhxx>().FirstOrDefault(c => c.kpbh == newYhxx.kpbh);
            newUser.hm = newYhxx.hm;
            newUser.tel = newYhxx.tel;
            newUser.bz = newYhxx.bz;
            if (baseDAL.SaveAllChanges())
                return Json(newUser);
            else
                return Json("修改用户资料失败！");
        }

        //生成业务编号
        [HttpPost]
        public JsonResult UpdateNewUser(yhxx newYhxx)
        {

            int ywbhNum;
            int total = baseDAL.Get<bhIndex>().Count();
            if (total == 0)
                ywbhNum = 1;
            else
                ywbhNum = total + 1;
            var bhIndex = new bhIndex
            {
                ywbh = ywbhNum
            };
            baseDAL.AddItem<bhIndex>(bhIndex);

            int yhlxNum = (int)newYhxx.yhlx;
            
            djgl djgl = baseDAL.Get<djgl>().FirstOrDefault(c => c.yhlx == yhlxNum);

            if (newYhxx.hm != null) {
                
                var newUser = baseDAL.Get<yhxx>().FirstOrDefault(c => c.kpbh == newYhxx.kpbh);
                newUser.ywbh = ywbhNum;
                newUser.fkrq = DateTime.Now;
                newUser.gqcs = 0;
                Session["ywbh"] = newUser.ywbh;
                Session["hm"] = newUser.hm;
                Session["zz"] = newUser.zz;
                Session["yhlx"] = newUser.yhlx;
                Session["tel"] = newUser.tel;
                Session["yhlxms"] = djgl.ms;
                Session["qj"] = djgl.qj;
                Session["gqcs"] = newUser.gqcs+1;
                baseDAL.SaveAllChanges();

            }
            else
            {
                
                Session["ywbh"] = ywbhNum;
                Session["hm"] = null;
                Session["zz"] = null;
                Session["yhlx"] = newYhxx.yhlx;
                Session["tel"] = null;
                Session["yhlxms"] = djgl.ms;
                Session["gqcs"] = 0;
                baseDAL.SaveAllChanges();

            }
            return Json(ywbhNum);
        }
  
        //返回新户售卡视图
        public ActionResult NewCardCharge()
        {
            ViewBag.ywbh = Session["ywbh"];
            ViewBag.hm = Session["hm"];
            ViewBag.zz = Session["zz"];
            ViewBag.yhlx = Session["yhlxms"];
            ViewBag.tel = Session["tel"];
            ViewBag.gqcs = Session["gqcs"];
            ViewBag.qj = Session["qj"];
            return View();
        }

        //返回新户充值结果
        [HttpPost]
        public string NewCardCharge(SellInfo sellInfo)
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

        //
    }
        
}
