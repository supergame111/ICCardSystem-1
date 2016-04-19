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
    public class SystemFunctionController : Controller
    {
        private DALBase baseDAL = new DALBase(new ICModelContainer());
        //
        // GET: /SystemFunction/
       
        //修改密码
        public ActionResult ChangePassword()
        {
            ViewBag.bh = Session["user_id"];
            return View();
        }
        [HttpPost]
        public string ChangePassword(ChangePasswordView changeInfo)
        {
            string bh = changeInfo.bh;
            string oldpassword = changeInfo.oldpassword;
            string newpassword = changeInfo.newpassword;
            czry Czy = baseDAL.Get<czry>().FirstOrDefault(c=>c.bh==bh);
            if (oldpassword != Czy.dm)
                return "原密码错误！";
            else
            {
                Czy.dm = newpassword;
                if (baseDAL.SaveAllChanges())
                    return "密码修改成功！";
                else
                    return "密码修改失败！";
            }        
        }

        //选择添加或修改界面
        public ActionResult AddOrChange() {
            return View();
        }


        //用户类型添加

        [HttpPost]
        public string UserKindsAdd(djgl djglInfo)
        {
            int count = 0;
            count = baseDAL.Get<djgl>().Count();
            djgl djgl = new djgl
            {
                yhlx = count + 1,
                qj = djglInfo.qj,
                ms = djglInfo.ms,
                ckxl = djglInfo.ckxl,
                yxl = djglInfo.yxl,
                bx = djglInfo.bx
            };
            baseDAL.AddItem<djgl>(djgl);
            if (baseDAL.SaveAllChanges())
                return "添加成功！";
            else
                return "添加失败！";
        }

        [HttpPost]
        public JsonResult userChangeBtn(djgl djglInfo)
        {
            List<djgl> djgllist=null;
            djgllist = baseDAL.Get<djgl>().ToList();           
            return Json(djgllist);
        }

        [HttpPost]
        public string UserKindsChange(djgl djglInfo)//修改气价时调用
        {
            string ms = djglInfo.ms;
            djgl djgl = baseDAL.Get<djgl>().FirstOrDefault(c => c.ms.Equals(ms));
            djgl.qj = djglInfo.qj;
            djgl.ckxl = djglInfo.ckxl;
            djgl.yxl = djglInfo.yxl;
            djgl.bx = djglInfo.bx;
            if (baseDAL.SaveAllChanges())
                return "修改成功！";
            else
                return "修改失败！";
        }

        [HttpPost]
        public JsonResult UserKinds_Change(djgl djglInfo)//用户类型select选项改变时调用
        {
            djgl djgl = null;
            djgl=baseDAL.Get<djgl>().FirstOrDefault(c => c.yhlx==djglInfo.yhlx);          
            return Json(djgl);
        }
                        
        [HttpPost]
        public JsonResult JdlxChangeBtn(jdlx jdlxInfo)
        {
            List<jdlx> jdlxlist = null;
            jdlxlist = baseDAL.Get<jdlx>().ToList();
            return Json(jdlxlist);
        }

        //建档类型添加或者修改
        [HttpPost]
        public string JdlxAdd_Change(jdlx jdlxInfo)
        {
            //添加
            if (jdlxInfo.jdlxId == -1)
            {
                int count=baseDAL.Get<jdlx>().Count();
                jdlx newJdlx = new jdlx{ 
                    jdlxId=count+1,
                    ms=jdlxInfo.ms
                };
                baseDAL.AddItem<jdlx>(newJdlx);
                if (baseDAL.SaveAllChanges())
                {
                    return "添加成功！";
                }
                else
                    return "添加失败！";
            }
            else
            {
                jdlx jdlx = baseDAL.Get<jdlx>().FirstOrDefault(c=>c.jdlxId==jdlxInfo.jdlxId);
                jdlx.ms = jdlxInfo.ms;
                if (baseDAL.SaveAllChanges())
                {
                    return "修改成功！";
                }
                else
                {
                    return "修改失败！";
                }
            }
        }

        //维修单管理
        public ActionResult RepairInfoManage()
        {
            return View();
        }

        //返回维修单数据
        public JsonResult RepairInfoShow(wxdj wxdjInfo)
        {
            DateTime dt = DateTime.Now;
            string today = dt.ToShortDateString();
            string todayB = today + " 00:00:00";
            string todayE = today + " 23:59:59";
            DateTime begin = Convert.ToDateTime(todayB);
            DateTime end = Convert.ToDateTime(todayE);
            var wxdjlist = baseDAL.Get<wxdj>().Where(c => c.xlsj >= begin).
                            Where(c => c.xlsj <= end).
                             Where(c => c.ywbh == wxdjInfo.ywbh).ToList().
                                Select(c =>
                                {
                                    if (c.hb == 0)
                                    {
                                        return new
                                        {
                                            xlsj = c.xlsj.ToString("yyyy-MM-dd HH:mm:ss"),
                                            bzm = c.bzm,
                                            hb = "否",
                                            czy = c.czy,
                                            wxdbh = c.wxdbh
                                        };
                                    }
                                    else return new
                                    {
                                        xlsj = c.xlsj.ToString("yyyy-MM-dd HH:mm:ss"),
                                        bzm = c.bzm,
                                        hb = "是",
                                        czy = c.czy,
                                        wxdbh = c.wxdbh
                                    };
                                });
            return Json(wxdjlist);
        }

        //删除维修单
        [HttpPost]
        public string RepairInfoDelete(wxdj wxdjInfo)
        {
            wxdj wxdj = baseDAL.Get<wxdj>().FirstOrDefault(c=>c.wxdbh==wxdjInfo.wxdbh);
            baseDAL.DeleteItem(wxdj);
            if (baseDAL.SaveAllChanges())
                return "删除成功！";
            else
                return "删除失败！";
        }
    
    }
}
