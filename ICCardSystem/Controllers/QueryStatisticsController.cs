using ICCardSystem.Models;
using ICCardSystem.Models.VModels;
using ICCardSystem.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICCardSystem.Controllers
{
    public class QueryStatisticsController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /QueryStatistics/

        //IC卡自动查询
        public ActionResult ICCardAutoQuery()
        {
            return View();
        }

        //用户信息查询
        public ActionResult UserInfoQuery()
        {
            
            return View();
        }

        [HttpPost]
        public string UserInfoQuery(ChooseQuery choosequery)
        {     
           // Session["choosequery"] = choosequery;
            yhxx yhxx = null;
            string str1 = choosequery.ChooseContent;
            string str2 = choosequery.QueryContent;
            if (str1.Equals("ywbh"))
            {
                int ywbhNum = int.Parse(str2);
                yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.ywbh == ywbhNum);
            }
            else if (str1.Equals("kpbh"))
            {
                int kpbhNum = int.Parse(str2);
                yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.kpbh == kpbhNum);
            }
            else if (str1.Equals("hm"))
                yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.hm.Equals(str2));
            else if (str1.Equals("zz"))
                yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.zz.Equals(str2));
            else
                yhxx = baseDAL.Get<yhxx>().FirstOrDefault(c => c.tel.Equals(str2));
            
           // var djglList = baseDAL.Get<djgl>().ToList();
          //  ViewBag.djglList = djglList;
           // var Yhxxlist = Json(yhxxlist);
          //  ViewBag.yhxxlist = Yhxxlist;
            if (yhxx!=null){
                var djgl = baseDAL.Get<djgl>().FirstOrDefault(c => c.yhlx == yhxx.yhlx);
                Session["yhlx"] = djgl.ms;
                Session["yhxx"] = yhxx;
                return "../QueryStatistics/UserInfoQuery_Back";
            }               
            else
                return "用户不存在！";
        }

        public ActionResult UserInfoQuery_Back()
        {
            yhxx yhxx = (yhxx)Session["yhxx"];
            List<yhxx> yhxxlist=new List<yhxx>();
            yhxxlist.Add(yhxx);
            ViewBag.yhxxlist = yhxxlist;
            ViewBag.ywbh = yhxx.ywbh;
            ViewBag.hm = yhxx.hm;
            ViewBag.tel = yhxx.tel;
            ViewBag.kpbh = yhxx.kpbh;
            ViewBag.zz = yhxx.zz;
            ViewBag.gqcs = yhxx.gqcs;
            ViewBag.gqzl = yhxx.gqzl;
            ViewBag.yhlx = Session["yhlx"];
            DateTime dt = (DateTime)yhxx.gqrq;
            ViewBag.gqrq = dt.ToString("yyyy-MM-dd");
            ViewBag.zxh = yhxx.zxh;
            return View();
        }

        //购气明细
        [HttpPost]
        public string BuyDetail(yhxx Yhxx)
        {
            Session["ywbh"] = Yhxx.ywbh;
            Session["hm"] = Yhxx.hm;
            return "../QueryStatistics/BuyDetail_Back";
        }
        public ActionResult BuyDetail_Back()
        {
            int Ywbh=(int)Session["ywbh"];
            string hm=(string)Session["hm"];
            List<yysj> yysjlist = baseDAL.Get<yysj>().Where(c => c.ywbh == Ywbh).ToList();
            ViewBag.ywbh = Ywbh;
            ViewBag.hm = hm;
            ViewBag.yysjlist = yysjlist;
            var yysjlist1 = yysjlist.Select(c =>
            {
                return new
                {
                    skcs = c.skcs,
                    skql = c.skql,
                    skje = c.skje,
                    skrq = c.skrq.ToString("yyyy-MM-dd HH:mm:ss")
                };
            });
            ViewBag.yysjlist1 = yysjlist1;
            return View();
        }
       
        //维修信息
        [HttpPost]
        public string RepairInfoDetail(yhxx Yhxx)
        {
            Session["ywbh"] = Yhxx.ywbh;
            Session["hm"] = Yhxx.hm;
            return "../QueryStatistics/RepairInfoDetail_Back";
        }
        public ActionResult RepairInfoDetail_Back()
        {
            int Ywbh=(int)Session["ywbh"];
            List<wxdj> wxdjlist = null;
            wxdjlist = baseDAL.Get<wxdj>().Where(c => c.ywbh == Ywbh).ToList();
            var wxdjlist1 =wxdjlist.Select( c =>
            {
                if (c.hb == 0)
                    return new
                    {
                        xlsj = c.xlsj.ToString("yyyy-MM-dd HH:mm:ss"),
                        bzm = c.bzm,
                        syql = c.syql,
                        czy = c.czy,
                        hb = "否"
                    };
                else
                    return new
                    {
                        xlsj = c.xlsj.ToString("yyyy-MM-dd HH:mm:ss"),
                        bzm = c.bzm,
                        syql = c.syql,
                        czy = c.czy,
                        hb = "是"
                    };
            });
            ViewBag.ywbh = Session["ywbh"];
            ViewBag.hm =Session["hm"];
            ViewBag.wxdjlist=wxdjlist;
            ViewBag.wxdjlist1=wxdjlist1;
            return View();
        }

       //补气信息
        [HttpPost]
        public string FillInfoDetail(yhxx Yhxx)
        {
            Session["ywbh"] = Yhxx.ywbh;
            Session["hm"] = Yhxx.hm;
            return "../QueryStatistics/FillInfoDetail_Back";
        }
        public ActionResult FillInfoDetail_Back()
        {
            int Ywbh=(int)Session["ywbh"];
            string hm=(string)Session["hm"];
            List<Bqxxb> bqxxblist = baseDAL.Get<Bqxxb>().Where(c => c.ywbh == Ywbh).ToList();
            var bqxxblist1 = baseDAL.Get<Bqxxb>().Where(c=>c.ywbh==Ywbh).Join(
                         baseDAL.Get<wxdj>(),
                           b => b.wxdbh, w => w.wxdbh,
                             (b, w) => new
                             {
                                 gqzl = b.gqzl,
                                 bzm = b.bzm,
                                 bcbqzl = b.bcbqzl,
                                 ybql = b.ybql,
                                 czy = w.czy
                             });
            ViewBag.ywbh = Ywbh;
            ViewBag.hm = hm;
            ViewBag.bqxxblist = bqxxblist;
            ViewBag.bqxxblist1 = bqxxblist1;
            return View();
        }
        
        //换表明细
        [HttpPost]
        public string ChangeMeter(yhxx Yhxx)
        {
            Session["ywbh"] = Yhxx.ywbh;
            Session["hm"] = Yhxx.hm;
            return "../QueryStatistics/ChangeMeter_Back";
        }
        public ActionResult ChangeMeter_Back()
        {
            int Ywbh=(int)Session["ywbh"];
            string hm=(string)Session["hm"];
            List<hbjl> hbjllist = null;
            hbjllist = baseDAL.Get<hbjl>().Where(c => c.ywbh == Ywbh).ToList();
            var hbjllist1=hbjllist.Select(c=>new{
                bzm=c.bzm,
                rq=c.rq.ToString("yyyy-MM-dd HH:mm:ss"),
                czy=c.czy
            });
            ViewBag.ywbh = Ywbh;
            ViewBag.hm = hm;
            ViewBag.hbjllist = hbjllist;
            ViewBag.hbjllist1 = hbjllist1;
            return View();
        }

        //营业数据查询
        public ActionResult DataQuery()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult DataQuery(DataQueryContent querydata)
        {
            string bdate = querydata.bDate;
            string edate = querydata.eDate;
            string czyInfo = querydata.czy;
            string yczInfo = querydata.ycz;
            bdate += " 00:00:00";
            edate += " 23:59:59";
            DateTime bDate = Convert.ToDateTime(bdate);
            DateTime eDate = Convert.ToDateTime(edate);            
            if (bDate > eDate)
            {
                return Json("输入的时间不符！");
            }
            else
            {
                List<yysj> yysjlist = null;
                if (czyInfo.Equals("所有操作员"))
                {
                    if (yczInfo.Equals("正常数据"))
                        yysjlist = baseDAL.Get<yysj>().Where(c => c.skrq >= bDate).
                             Where(c => c.skrq <= eDate).
                             Where(c => c.zfbz != 1).ToList();
                    else
                        yysjlist = baseDAL.Get<yysj>().Where(c => c.skrq >= bDate).
                            Where(c => c.skrq <= eDate).
                            Where(c => c.zfbz == 1).ToList();
                }
                else
                {
                    string username = (string)Session["user_id"];
                    if (yczInfo.Equals("正常数据"))
                        yysjlist = baseDAL.Get<yysj>().Where(c => c.skrq >= bDate).
                            Where(c => c.skrq <= eDate).
                            Where(c => c.czy == username).
                            Where(c => c.zfbz != 1).ToList();
                    else
                        yysjlist = baseDAL.Get<yysj>().Where(c => c.skrq >= bDate).
                            Where(c => c.skrq <= eDate).
                            Where(c => c.czy == username).
                            Where(c => c.zfbz == 1).ToList();
                }               
                var yysjlist1 = yysjlist.Select(c =>
                {
                    return new
                    {
                        ywbh = c.ywbh,
                        skcs = c.skcs,
                        skql = c.skql,
                        skje = c.skje,
                        skrq = c.skrq.ToString("yyyy-MM-dd HH:mm:ss"),
                        czy = c.czy,
                        fpbh = c.fpbh
                    };
                });
                if (yysjlist1.Count() == 0)
                    return Json("无营业数据！");
                else
                    return Json(yysjlist1);
            }
        }
    }
}
