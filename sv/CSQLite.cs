using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using BaseDLL;


namespace sv
{
    public static class CSQLite
    {
        public static SQLiteConnection SQLiteConnection(string Name, string Version = "3")
        {
            string connectionString = string.Concat($"Data Source={Name};Version={Version};");
            return new SQLiteConnection(connectionString);
        }
        public static SQLiteCommand Cursor(string sql, SQLiteConnection db)
        {
            SQLiteCommand SQLiteCommand = new SQLiteCommand(sql, db);
            SQLiteCommand.ExecuteNonQuery();
            return SQLiteCommand;
        }
        public static class Insert
        {
            public static void CreatUseTabel()
            {
                if (!File.Exists("Database"))
                {
                    string sql = @"CREATE TABLE IF NOT EXISTS IDCardInfo(id INTEGER PRIMARY KEY  AUTOINCREMENT, CreaTime varchar(100), Name varchar(100), Sex varchar(100),Nation varchar(100),Born varchar(100),Address varchar(100),IDCardNo varchar(100),GrantDept varchar(100),UserLifeBegin varchar(100),UserLifeEnd varchar(100),PassID varchar(100),IssuesTimes varchar(100),reserved varchar(100),PhotoFileName varchar(100),CardType varchar(100) ,EngName varchar(100),CertVol varchar(100),szPath varchar(100),Sucessed varchar(100),Similarity varchar(100),PhotoFile VARBINARY (50000),PhotoFile1 VARBINARY (50000),PhotoFile2 VARBINARY (50000));";
                    // Log 日志表
                    sql += @"CREATE TABLE IF NOT EXISTS Log(id INTEGER PRIMARY KEY  AUTOINCREMENT,ip TEXT,mac TEXT,name TEXT,time TEXT,info TEXT,version TEXT,reserved TEXT);";
                    // 网络缓存表
                    sql += @"drop table if exists Cache;CREATE TABLE IF NOT EXISTS Cache(id INTEGER PRIMARY KEY  AUTOINCREMENT,CreaTime varchar(100),Url TEXT,Content varchar(65536),reserved TEXT,UNIQUE (Url));";
                    //打印日志表
                    sql += @"CREATE TABLE IF NOT EXISTS LogPrint(id INTEGER PRIMARY KEY  AUTOINCREMENT,CreaTime varchar(100),ProjectName varchar(1000),Num integer,Name varchar(100),IDCardNo varchar(100),IP varchar(100),MAC varchar(100),reserved TEXT);";
                    //SQL 注入
                    SQLiteConnection db = new SQLiteConnection(string.Concat($"Data Source=Database;Version=3;"));
                    db.Open();
                    SQLiteCommand sQLiteCommand = new SQLiteCommand(sql, db);
                    sQLiteCommand.ExecuteNonQuery();
                    db.Close();
                }
            }

            public static bool WriteIDCardData(IDCardData idcardData, string CameraPictrue1 = null, string CameraPictrue2 = null, double Similarity = 0)
            {
                string Sucessed = Similarity > 0.7 ? "对比通过" : "对比失败";
                Sucessed = CameraPictrue1 == null ? "" : Sucessed;
                try
                {
                    SQLiteConnection db = SQLiteConnection("Database");
                    db.Open();
                    string cmdText = string.Format("insert into IDCardInfo (CreaTime,Name,Sex,Nation,Born,Address,IDCardNo,GrantDept,UserLifeBegin,UserLifeEnd,PassID,IssuesTimes,reserved,PhotoFileName,CardType,EngName,CertVol,szPath,Sucessed,Similarity,PhotoFile,PhotoFile1,PhotoFile2) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',{20},{21},{22})", new object[]
                    {
                        DateTime.Now.ToString("yyyy年MM月dd日 dddd HH:mm:ss"),
                        idcardData.Name,
                        idcardData.Sex,
                        idcardData.Nation,
                        idcardData.Born,
                        idcardData.Address,
                        idcardData.IDCardNo,
                        idcardData.GrantDept,
                        idcardData.UserLifeBegin,
                        idcardData.UserLifeEnd,
                        idcardData.PassID,
                        idcardData.IssuesTimes,
                        idcardData.reserved,
                        idcardData.PhotoFileName,
                        idcardData.CardType,
                        idcardData.EngName,
                        idcardData.CertVol,
                        idcardData.szPath,
                        Sucessed,
                        Similarity,
                        idcardData.PhotoFileName==null?"null":"@PhotoFile",
                        CameraPictrue1==null?"null":"@PhotoFile1",
                        CameraPictrue2==null?"null":"@PhotoFile2"
                   });
                    SQLiteCommand SQLiteCommand = new SQLiteCommand(cmdText, db);
                    byte[] array;
                    if (idcardData.PhotoFileName != null)
                    {
                        array = File.ReadAllBytes(idcardData.PhotoFileName);
                        SQLiteCommand.Parameters.Add("@PhotoFile", DbType.Binary, array.Length);
                        SQLiteCommand.Parameters["@PhotoFile"].Value = array;
                    }

                    if (CameraPictrue1 != null)
                    {
                        array = File.ReadAllBytes(CameraPictrue1);
                        SQLiteCommand.Parameters.Add("@PhotoFile1", DbType.Binary, array.Length);
                        SQLiteCommand.Parameters["@PhotoFile1"].Value = array;
                    }
                    if (CameraPictrue2 != null)
                    {
                        array = File.ReadAllBytes(CameraPictrue2);
                        SQLiteCommand.Parameters.Add("@PhotoFile2", DbType.Binary, array.Length);
                        SQLiteCommand.Parameters["@PhotoFile2"].Value = array;
                    }

                    SQLiteCommand.ExecuteNonQuery();

                    db.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }  
        }
    }

}
