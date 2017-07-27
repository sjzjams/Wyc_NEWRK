using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wyc_NEWRK.Service
{
    public class servicemodel
    {
        /// <summary>
        /// service:实体类(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        [Serializable]
        public partial class service
        {
            public service()
            { }
            #region Model
            private string _username;
            private string _userage;
            private string _usersex;
            private string _userphoto;
            private string _usermobile;
            private string _useraddress;
            private DateTime? _subtime;
            private string _zhengzhuang;
            private string _chats;
            private DateTime? _eatime;
            private string _telextract;
            private string _eaname;
            private string _userweb;
            private string _theirpeople;
            private int? _flag;
            private string _memo;
            private string _bj;
            private string _sfzy;
            private string _typeflag;
            private DateTime? _hfdata;
            private string _sfyx;
            private string _zzy;
            private string _sfdh;
            private string _deleflag;
            private string _zhenduan;
            private string _stas;
            private string _zzystas;
            private DateTime? _lasttime;
            private string _flagbumen;
            private string _xcbumen;
            private string _kfbumen;
            private string _bianyuan;
            private string _urltext;
            private string _bqname;
            private string _byname;
            private string _byphone;
            private DateTime? _zydate;
            private string _strurl;
            private string _hotflag;
            private string _menzhenname;
            private string _menzhenphone;
            private string _kefuname;
            private string _kefuphone;
            private string _memo2;
            private string _flag2;
            private string _jdflag;
            private string _xcname;
            private DateTime? _baobei;
            /// <summary>
            /// 
            /// </summary>
            public string username
            {
                set { _username = value; }
                get { return _username; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string userage
            {
                set { _userage = value; }
                get { return _userage; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string usersex
            {
                set { _usersex = value; }
                get { return _usersex; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string userphoto
            {
                set { _userphoto = value; }
                get { return _userphoto; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string usermobile
            {
                set { _usermobile = value; }
                get { return _usermobile; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string useraddress
            {
                set { _useraddress = value; }
                get { return _useraddress; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? subtime
            {
                set { _subtime = value; }
                get { return _subtime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string zhengzhuang
            {
                set { _zhengzhuang = value; }
                get { return _zhengzhuang; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string chats
            {
                set { _chats = value; }
                get { return _chats; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? eatime
            {
                set { _eatime = value; }
                get { return _eatime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string telextract
            {
                set { _telextract = value; }
                get { return _telextract; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string eaname
            {
                set { _eaname = value; }
                get { return _eaname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string userweb
            {
                set { _userweb = value; }
                get { return _userweb; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string theirpeople
            {
                set { _theirpeople = value; }
                get { return _theirpeople; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? flag
            {
                set { _flag = value; }
                get { return _flag; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string memo
            {
                set { _memo = value; }
                get { return _memo; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string bj
            {
                set { _bj = value; }
                get { return _bj; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string sfzy
            {
                set { _sfzy = value; }
                get { return _sfzy; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string typeflag
            {
                set { _typeflag = value; }
                get { return _typeflag; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? hfdata
            {
                set { _hfdata = value; }
                get { return _hfdata; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string sfyx
            {
                set { _sfyx = value; }
                get { return _sfyx; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string zzy
            {
                set { _zzy = value; }
                get { return _zzy; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string sfdh
            {
                set { _sfdh = value; }
                get { return _sfdh; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string deleflag
            {
                set { _deleflag = value; }
                get { return _deleflag; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string zhenduan
            {
                set { _zhenduan = value; }
                get { return _zhenduan; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string stas
            {
                set { _stas = value; }
                get { return _stas; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string zzystas
            {
                set { _zzystas = value; }
                get { return _zzystas; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? lasttime
            {
                set { _lasttime = value; }
                get { return _lasttime; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string flagbumen
            {
                set { _flagbumen = value; }
                get { return _flagbumen; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string xcbumen
            {
                set { _xcbumen = value; }
                get { return _xcbumen; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string kfbumen
            {
                set { _kfbumen = value; }
                get { return _kfbumen; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string bianyuan
            {
                set { _bianyuan = value; }
                get { return _bianyuan; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string urltext
            {
                set { _urltext = value; }
                get { return _urltext; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string bqname
            {
                set { _bqname = value; }
                get { return _bqname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string byname
            {
                set { _byname = value; }
                get { return _byname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string byphone
            {
                set { _byphone = value; }
                get { return _byphone; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? zydate
            {
                set { _zydate = value; }
                get { return _zydate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string strurl
            {
                set { _strurl = value; }
                get { return _strurl; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string hotflag
            {
                set { _hotflag = value; }
                get { return _hotflag; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string menzhenname
            {
                set { _menzhenname = value; }
                get { return _menzhenname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string menzhenphone
            {
                set { _menzhenphone = value; }
                get { return _menzhenphone; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string kefuname
            {
                set { _kefuname = value; }
                get { return _kefuname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string kefuphone
            {
                set { _kefuphone = value; }
                get { return _kefuphone; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string memo2
            {
                set { _memo2 = value; }
                get { return _memo2; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string flag2
            {
                set { _flag2 = value; }
                get { return _flag2; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string jdflag
            {
                set { _jdflag = value; }
                get { return _jdflag; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string xcname
            {
                set { _xcname = value; }
                get { return _xcname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? baobei
            {
                set { _baobei = value; }
                get { return _baobei; }
            }
            #endregion Model

        }
    }
}
