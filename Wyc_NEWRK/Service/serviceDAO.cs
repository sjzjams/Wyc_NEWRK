using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Wyc_NEWRK.Service
{
   public class serviceDAO
    {
        
        /// <summary>
        /// 增加一条数据
        /// </summary>
      
        #region  Method

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(servicemodel.service model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into service(");
            strSql.Append("username,userage,usersex,userphoto,usermobile,useraddress,subtime,zhengzhuang,chats,eatime,telextract,eaname,userweb,theirpeople,flag,memo,bj,sfzy,typeflag,hfdata,sfyx,zzy,sfdh,deleflag,zhenduan,stas,zzystas,lasttime,flagbumen,xcbumen,kfbumen,bianyuan,urltext,bqname,byname,byphone,zydate,strurl,hotflag,menzhenname,menzhenphone,kefuname,kefuphone,memo2,flag2,jdflag,xcname,baobei)");
            strSql.Append(" values (");
            strSql.Append("@username,@userage,@usersex,@userphoto,@usermobile,@useraddress,@subtime,@zhengzhuang,@chats,@eatime,@telextract,@eaname,@userweb,@theirpeople,@flag,@memo,@bj,@sfzy,@typeflag,@hfdata,@sfyx,@zzy,@sfdh,@deleflag,@zhenduan,@stas,@zzystas,@lasttime,@flagbumen,@xcbumen,@kfbumen,@bianyuan,@urltext,@bqname,@byname,@byphone,@zydate,@strurl,@hotflag,@menzhenname,@menzhenphone,@kefuname,@kefuphone,@memo2,@flag2,@jdflag,@xcname,@baobei)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.VarChar,255),
					new SqlParameter("@userage", SqlDbType.VarChar,255),
					new SqlParameter("@usersex", SqlDbType.VarChar,255),
					new SqlParameter("@userphoto", SqlDbType.VarChar,255),
					new SqlParameter("@usermobile", SqlDbType.VarChar,255),
					new SqlParameter("@useraddress", SqlDbType.VarChar,255),
					new SqlParameter("@subtime", SqlDbType.DateTime),
					new SqlParameter("@zhengzhuang", SqlDbType.VarChar,255),
					new SqlParameter("@chats", SqlDbType.NText),
					new SqlParameter("@eatime", SqlDbType.DateTime),
					new SqlParameter("@telextract", SqlDbType.VarChar,255),
					new SqlParameter("@eaname", SqlDbType.VarChar,255),
					new SqlParameter("@userweb", SqlDbType.VarChar,255),
					new SqlParameter("@theirpeople", SqlDbType.VarChar,255),
					new SqlParameter("@flag", SqlDbType.Int,4),
					new SqlParameter("@memo", SqlDbType.VarChar,255),
					new SqlParameter("@bj", SqlDbType.VarChar,255),
					new SqlParameter("@sfzy", SqlDbType.VarChar,255),
					new SqlParameter("@typeflag", SqlDbType.VarChar,255),
					new SqlParameter("@hfdata", SqlDbType.DateTime),
					new SqlParameter("@sfyx", SqlDbType.VarChar,255),
					new SqlParameter("@zzy", SqlDbType.VarChar,255),
					new SqlParameter("@sfdh", SqlDbType.VarChar,255),
					new SqlParameter("@deleflag", SqlDbType.VarChar,255),
					new SqlParameter("@zhenduan", SqlDbType.VarChar,255),
					new SqlParameter("@stas", SqlDbType.VarChar,255),
					new SqlParameter("@zzystas", SqlDbType.VarChar,255),
					new SqlParameter("@lasttime", SqlDbType.DateTime),
					new SqlParameter("@flagbumen", SqlDbType.VarChar,255),
					new SqlParameter("@xcbumen", SqlDbType.VarChar,255),
					new SqlParameter("@kfbumen", SqlDbType.VarChar,255),
					new SqlParameter("@bianyuan", SqlDbType.VarChar,255),
					new SqlParameter("@urltext", SqlDbType.VarChar,255),
					new SqlParameter("@bqname", SqlDbType.VarChar,255),
					new SqlParameter("@byname", SqlDbType.VarChar,255),
					new SqlParameter("@byphone", SqlDbType.VarChar,255),
					new SqlParameter("@zydate", SqlDbType.DateTime),
					new SqlParameter("@strurl", SqlDbType.VarChar,255),
					new SqlParameter("@hotflag", SqlDbType.VarChar,255),
					new SqlParameter("@menzhenname", SqlDbType.VarChar,255),
					new SqlParameter("@menzhenphone", SqlDbType.VarChar,255),
					new SqlParameter("@kefuname", SqlDbType.VarChar,255),
					new SqlParameter("@kefuphone", SqlDbType.VarChar,255),
					new SqlParameter("@memo2", SqlDbType.VarChar,255),
					new SqlParameter("@flag2", SqlDbType.VarChar,255),
					new SqlParameter("@jdflag", SqlDbType.VarChar,255),
					new SqlParameter("@xcname", SqlDbType.VarChar,255),
					new SqlParameter("@baobei", SqlDbType.DateTime)};
            parameters[0].Value = model.username;
            parameters[1].Value = model.userage;
            parameters[2].Value = model.usersex;
            parameters[3].Value = model.userphoto;
            parameters[4].Value = model.usermobile;
            parameters[5].Value = model.useraddress;
            parameters[6].Value = model.subtime;
            parameters[7].Value = model.zhengzhuang;
            parameters[8].Value = model.chats;
            parameters[9].Value = model.eatime;
            parameters[10].Value = model.telextract;
            parameters[11].Value = model.eaname;
            parameters[12].Value = model.userweb;
            parameters[13].Value = model.theirpeople;
            parameters[14].Value = model.flag;
            parameters[15].Value = model.memo;
            parameters[16].Value = model.bj;
            parameters[17].Value = model.sfzy;
            parameters[18].Value = model.typeflag;
            parameters[19].Value = model.hfdata;
            parameters[20].Value = model.sfyx;
            parameters[21].Value = model.zzy;
            parameters[22].Value = model.sfdh;
            parameters[23].Value = model.deleflag;
            parameters[24].Value = model.zhenduan;
            parameters[25].Value = model.stas;
            parameters[26].Value = model.zzystas;
            parameters[27].Value = model.lasttime;
            parameters[28].Value = model.flagbumen;
            parameters[29].Value = model.xcbumen;
            parameters[30].Value = model.kfbumen;
            parameters[31].Value = model.bianyuan;
            parameters[32].Value = model.urltext;
            parameters[33].Value = model.bqname;
            parameters[34].Value = model.byname;
            parameters[35].Value = model.byphone;
            parameters[36].Value = model.zydate;
            parameters[37].Value = model.strurl;
            parameters[38].Value = model.hotflag;
            parameters[39].Value = model.menzhenname;
            parameters[40].Value = model.menzhenphone;
            parameters[41].Value = model.kefuname;
            parameters[42].Value = model.kefuphone;
            parameters[43].Value = model.memo2;
            parameters[44].Value = model.flag2;
            parameters[45].Value = model.jdflag;
            parameters[46].Value = model.xcname;
            parameters[47].Value = model.baobei;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion  Method
       
    }

}
