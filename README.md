# Wyc_NEWRK2013-2-25 
记录之前搞的东西
使用说明，此项目是为了实现一种伪原创方式。利用火车头采集相关内容，然后把每一篇文章按每段分开，然后再根据标题分词自动匹配相对应的段乱，然后进行文章重组。
1、用火车头采集文章后找到采集文章生成的数据库放到，项目bin\Debug\App_Data文件夹下。
2、然后运行项目导入
3、在测试查询选项卡，创建索引。
4、处理内容，进行文章重组。
5、导出，可以选择导出数据库，支持sqlite数据库限于收费版火车头，支持生成access数据库。
本项目出自石家庄小罗网址http://www.luoblog.com(我的网站由于忘了续费被别人给注册了。。。)

此项目源码开放

开发环境vs2010+sql2008 +SqliteDev
当中文档盘古分词，app_data文件夹下包含：关键词txt，导入的数据库mdb，需要生成的文章标题sytitle.txt
dict文件夹下包含：盘古词库，停用词.txt，同义词.txt，
使用方式通过火车头采集数据文件格式mdb，导入以后创建索引，处理内容，导出。
本程序用到盘古分词处理的一些应用（以及Lucene.Net）。主要有对字符串的分词处理，里面有自己做的小实例可供参考。

运行界面部门效果是参照Hubble项目。

主要运行程序Wyc_WinForm,运行显示界面没有任何功能。主要功能实现导入，导出。和重建索引搜索。


数据库 俩表，test3是导入数据存的，test4是处理数据后的数据。test5是最后生成需要导出的数据

创建数据库用到sql2008新特性--表值参数(Table-valued parameter)

--表值参数(Table-valued parameter)这是一个创建例子。数据库中test3、4、5都是这样创建
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test3Table]')

AND type in (N'U')) 
DROP TABLE [dbo].[test3Table] 
GO 
USE [wyc] 
GO 
     Create table test3Table
     (  
	   ID int IDENTITY PRIMARY KEY,
       Title varchar(255),
       Content ntext,
       keys varchar(255),
       bztype varchar(255)
     )
     go  
     --Create Table Valued  
IF EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id 
WHERE st.name = N'test3Udt' AND ss.name = N'dbo') 
DROP TYPE [dbo].[test3Udt] 
GO 
USE [wyc] 
GO 
     CREATE TYPE test3Udt AS TABLE  
     (
       Title varchar(255),  
       Content ntext,
       keys varchar(255),
       bztype varchar(255)
     )  



Wyc_WinForm\App_Data文件夹下SpiderResult.mdb access数据库是导入数据。

通过导入数据处理以后生成一个db3的数据库文件。其中用到sqlite数据库操作，其中导出数据实例用的就是sqlite特性。仅供参考。
