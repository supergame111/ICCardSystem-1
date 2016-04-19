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
    public class ReportPrintController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());

        public ActionResult MonthReportPrint()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MonthReportPrint(DataQueryContent queryTime)
        {
            string str1 = queryTime.bDate;
            string str2 = queryTime.eDate;
            str1 += " 00:00:00";
            str2 += " 23:59:59";
            string str3 = str1;
            String str4 = queryTime.bDate;
            str4 += " 23:59:59";
            DateTime bdate = Convert.ToDateTime(str1);
            DateTime edate = Convert.ToDateTime(str2);
            DateTime bdate_B = Convert.ToDateTime(str3);
            DateTime bdate_E = Convert.ToDateTime(str4);                 
            if (bdate > edate)
                return Json("输入时间不符！");
            else
            {
                //预冲账报表         
                List<yysj> yysjlist = baseDAL.Get<yysj>().Where(c => c.skrq >= bdate).
                    Where(c => c.skrq <= edate).
                    Where(c => c.zfbz == 1).ToList();
                List<YchongzhangReport> yczlist = new List<YchongzhangReport>();
                for (int i = 0; i < yysjlist.Count; i++)
                {
                    YchongzhangReport ychongzhang = new YchongzhangReport();
                    ychongzhang.ywbh = yysjlist[i].ywbh;
                    ychongzhang.skcs = yysjlist[i].skcs;
                    ychongzhang.skql = yysjlist[i].skql;
                    ychongzhang.skje = (double)yysjlist[i].skje;
                    ychongzhang.skrq = yysjlist[i].skrq.ToString("yyyy-MM-dd");
                    ychongzhang.fpbh = yysjlist[i].fpbh;
                    yczlist.Add(ychongzhang);
                } 
                //代售点月报表
                List<MonthReport> monthReportList = new List<MonthReport>();
                while (bdate_E <= edate)
                {
                    var date_Yysj = baseDAL.Get<yysj>().Where(c => c.skrq >= bdate_B).
                        Where(c => c.skrq <= bdate_E);
                    int date_cs = date_Yysj.Count();
                    if (date_cs > 0)
                    {
                        double date_ql = date_Yysj.Sum(c => c.skql);
                        double date_je = (double)date_Yysj.Sum(c => c.skje);
                        MonthReport mR = new MonthReport
                        {
                            date = bdate_B.ToString("yyyy-MM-dd"),
                            date_cs = date_cs,
                            date_ql = date_ql,
                            date_je = date_je
                        };
                        monthReportList.Add(mR);
                        bdate_B = bdate_B.AddDays(1);
                        bdate_E = bdate_E.AddDays(1);
                    }
                    else
                    {
                        bdate_B = bdate_B.AddDays(1);
                        bdate_E = bdate_E.AddDays(1);
                    }
                }
                MonthAndYczReport monthAndYczReport = new MonthAndYczReport();
                monthAndYczReport.monthReportList = monthReportList;
                monthAndYczReport.yczList = yczlist;
                if (monthReportList.Count() == 0)
                    return Json("无营业数据！");
                else
                    return Json(monthAndYczReport);
            }
        }


        public ActionResult PrintInvoice()
        {
            return View();
        }

        [HttpPost]
        public string PrintData()
        {
            if (Session["ywbhP"] != null)
            {
                int ywbh = (int)Session["ywbhP"];
                yhxx yHxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.ywbh == ywbh);
                DateTime dt = (DateTime)yHxx.gqrq;
                Session["gqrqP"] = dt.ToString("yyy-MM-dd");
                Session["hmP"] = yHxx.hm;
                Session["zzP"] = yHxx.zz;
                djgl dJgl = baseDAL.Get<djgl>().FirstOrDefault(c => c.yhlx == yHxx.yhlx);
                Session["qjP"] = dJgl.qj;
                return "../ReportPrint/Print";
            }
            else
                return "";
        }
        public ActionResult Print()
        {
            ViewBag.ywbh = Session["ywbhP"];
            ViewBag.gqrq=Session["gqrqP"];
            ViewBag.hm = Session["hmP"];
            ViewBag.zz = Session["zzP"];
            ViewBag.fpbh = Session["fpbhP"];
            ViewBag.czy = Session["user_name"];
            ViewBag.gql = Session["gqlP"];
            ViewBag.qjMoney = Session["qjMoneyP"];
            ViewBag.moneyTotal = Session["moneyTotalP"];
            ViewBag.returnMoney = Session["returnMoneyP"];
            ViewBag.qj=Session["qjP"];
            return View();
        }

    }
}
