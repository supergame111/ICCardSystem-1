using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ICCardSystem.Common;
using ICCardSystem.Models;

namespace ICCardSystem.Utility
{
    /// <summary>
    /// 权限管理类
    /// </summary>
    public sealed class BaseHelper
    {
        // 权限基础初始化
        public static void InitData()
        {
            var _context = new ICModelContainer();
            // 等级
            //try
            //{
            //    if (_context.AdminLevels.Count() == 0)
            //    {
            //        _context.AdminLevels.Add(new AdminLevel() { Id = (byte)AdminLevelEnum.SuperAdmin, Name = "超级管理员" });
            //        _context.AdminLevels.Add(new AdminLevel() { Id = (byte)AdminLevelEnum.Admin, Name = "管理员" });
            //        _context.AdminLevels.Add(new AdminLevel() { Id = (byte)AdminLevelEnum.Operator, Name = "操作员" });
            //        _context.SaveChanges();
            //    }
            //}
            //catch (System.Exception ex) { LogHelper.WriteLog(ex); }
            // 管理员   
            try
            {
                //添加操作员
                if (_context.czrySet.Count() == 0)
                {
                    _context.czrySet.Add(new czry() { bh = "001", xm = "root", dm = "root", zcyy = 1 });
                    _context.czrySet.Add(new czry() { bh = "002", xm = "admin", dm = "admin", zcyy = 2 });
                    _context.SaveChanges();
                }
                //添加操作员
                if (_context.Admins.Count() == 0)
                {
                    //Console.WriteLine("为系统管理员添加初始化数据");
                    _context.Admins.Add(new Admin() { AdminLevel = 1, Name = "张三", Password = "root", WorkId = "root"});
                    _context.Admins.Add(new Admin() { AdminLevel = 1, Name = "李四", Password = "admin", WorkId = "admin" });
                    _context.SaveChanges();
                }
                //添加用户信息
                if (_context.yhxxSet.Count() == 0)
                {
                   // Console.WriteLine("为用户信息表添加初始化数据");
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园210栋2-1-1", zxh = "TX418", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园210栋1-1-1", zxh = "TX408", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖新澳阳光城10栋2-1-1", zxh = "TX308", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-1-1", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-1-2", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-1-3", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-1-4", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-2-1", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-2-2", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-2-3", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-2-4", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.yhxxSet.Add(new yhxx() { ywbh = 0, zz = "金银湖环湖路畔岛花园104栋2-3-1", zxh = "TX23", yhlx = 0, jdlx = 1 });
                    _context.SaveChanges();
                }
                //添加单价类型
                if (_context.djglSet.Count() == 0)
                {
                    _context.djglSet.Add(new djgl() { yhlx = 0, qj = (decimal)2.30, ms = "天然气", ckxl = 10000000, yxl = 100000, bx = 1, gs = 10 });
                    _context.djglSet.Add(new djgl() { yhlx = 1, qj = (decimal)2.20, ms = "煤气", ckxl = 10000000, yxl = 100000, bx = 2, gs = 11 });
                    _context.SaveChanges();
               
                }
                //添加建档类型
                if (_context.jdlxSet.Count() == 0)
                {
                    _context.jdlxSet.Add(new jdlx() {jdlxId=1,ms="新装IC卡表" });
                    _context.SaveChanges();
                }
                //添加维修信息
                if (_context.wxdjSet.Count() == 0)
                {
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now, gzdm = 3001, hb = 0, bzm = 10, BefBh = "4001", NowBh = "40002", syql = 5, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now, gzdm = 3001, hb = 1, bzm = 20, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now, gzdm = 3001, hb = 1, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now, gzdm = 3001, hb = 0, bzm = 30, BefBh = "4002", NowBh = "40003", syql = 40, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now, gzdm = 3001, hb = 1, bzm = 50, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(1), gzdm = 3001, hb = 1, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(2), gzdm = 3001, hb = 1, bzm = 40, BefBh = "4002", NowBh = "40003", syql = 50, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(3), gzdm = 3001, hb = 0, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(4), gzdm = 3001, hb = 1, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(5), gzdm = 3001, hb = 1, bzm = 60, BefBh = "4002", NowBh = "40003", syql = 20, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(6), gzdm = 3001, hb = 1, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(7), gzdm = 3001, hb = 0, bzm = 20, BefBh = "4002", NowBh = "40003", syql = 10, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(8), gzdm = 3001, hb = 1, bzm = 10, BefBh = "4002", NowBh = "40003", syql = 40, czy = "001" });
                    _context.wxdjSet.Add(new wxdj() { ywbh = 1, yh = "赵", zz = "金银湖环湖路畔岛花园210栋2-1-1", tel = "123", xlsj = DateTime.Now.AddDays(9), gzdm = 3001, hb = 0, bzm = 30, BefBh = "4002", NowBh = "40003", syql = 30, czy = "001" });
                    _context.SaveChanges();
                }
                //添加营业数据
                if (_context.yysjSet.Count() == 0)
                {
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 1, skrq = DateTime.Now.AddDays(1), skql = 10, skje = (decimal)23.00, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC00107010001" });
                    _context.yysjSet.Add(new yysj() { ywbh = 2, skcs = 3, skrq = DateTime.Now, skql = 20, skje = (decimal)46.00, czy = "002", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC00107010002" });
                    _context.yysjSet.Add(new yysj() { ywbh = 3, skcs = 4, skrq = DateTime.Now.AddDays(1), skql = 10, skje = (decimal)23.00, czy = "002", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC00107010003" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 2, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC00107010004" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 3, skrq = DateTime.Now.AddDays(1), skql = 35, skje = (decimal)80.50, czy = "002", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC00107010005" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 4, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC00107010006" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 5, skrq = DateTime.Now.AddDays(2), skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC00107010007" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 6, skrq = DateTime.Now.AddDays(1), skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC00107010008" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 7, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC00107010009" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 8, skrq = DateTime.Now.AddDays(2), skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC001070100010" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 9, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC001070100011" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 10, skrq = DateTime.Now.AddDays(3), skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC001070100012" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 11, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC001070100013" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 12, skrq = DateTime.Now.AddDays(1), skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 0, fpbh = "IC001070100014" });
                    _context.yysjSet.Add(new yysj() { ywbh = 1, skcs = 13, skrq = DateTime.Now, skql = 35, skje = (decimal)80.50, czy = "001", bb = 1, hh = 2, sjkb = 3, time = DateTime.Now, gzzbh = 4, lstbz = 5, bqbz = 0, zfbz = 1, fpbh = "IC001070100015" });
                    _context.SaveChanges();
                }
                //添加补气信息
                if (_context.BqxxbSet.Count() == 0)
                {
                    _context.BqxxbSet.Add(new Bqxxb() { ywbh = 1, gqzl = 30, bzm = 20, bcbqzl = 5, ybql = 0, bqwc = 0, wxdbh = 1 });
                    _context.BqxxbSet.Add(new Bqxxb() { ywbh = 1, gqzl = 75, bzm = 5, bcbqzl = 25, ybql = 25, bqwc = 1, wxdbh = 2 });
                    _context.SaveChanges();
                }
                //添加换表信息记录
                if (_context.hbjlSet.Count() == 0)
                {
                    _context.hbjlSet.Add(new hbjl() { ywbh = 1, bzm = 20, rq = DateTime.Now, czy = "002", wxdbh = 1, dqqm = 20, dqbh = "AC1234560001", cxbz = 0 });
                    _context.hbjlSet.Add(new hbjl() { ywbh = 1, bzm = 20, rq = DateTime.Now.AddDays(1), czy = "001", wxdbh = 1, dqqm = 20, dqbh = "AC1234560001", cxbz = 1 });
                    _context.hbjlSet.Add(new hbjl() { ywbh = 1, bzm = 5, rq = DateTime.Now.AddDays(2), czy = "002", wxdbh = 1, dqqm = 20, dqbh = "AC1234560001", cxbz = 0 });
                    _context.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.WriteLog(ex); 
            }
   
        }

        #region 私有成员

        /// <summary>
        /// 管理员权限等级枚举
        /// </summary>
        private enum AdminLevelEnum
        {
            SuperAdmin = 1,         // 超级管理员
            Admin,                  // 管理员
            Operator                // 操作员
        }

        #endregion

    }
}