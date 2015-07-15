using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WFM.Common
{
    /// <summary>
    /// AppLog の概要の説明です
    /// </summary>
    public class AppLog
    {
        //タブ
        private const string Const_DelimiterTab = "\t";
        //ログファイルのディレクトリ
        private const string Const_BasePath = @"\log";
        //ログファイルのディレクトリ
        private const string Const_BaseBackUpPath = @"\log\backup";
        //ログファイルの文字コード
        private static Encoding Const_Encoding = Encoding.GetEncoding("shift_jis");
        //ログファイルの拡張子
        private const string Const_Extention = ".log";
        //Mutexを定義
        private static Mutex mu = new Mutex(false);

        private const string Const_delimiterSpace = " ";

        #region ログ出力
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="message">メッセージ本体</param>
        public static void WriteLog(string message)
        {
            try
            {
                mu.WaitOne();

                //フォルダを取得
                string strLogDirPath = HttpContext.Current.Server.MapPath("~/");

                //ディレクトリチェック
                string logDirectory = strLogDirPath + Const_BasePath;

                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                //LOGファイルのバックアップフォルダ
                string bkDirectory = strLogDirPath + Const_BaseBackUpPath;

                if (!Directory.Exists(bkDirectory))
                {
                    Directory.CreateDirectory(bkDirectory);
                }

                //現在時間を取得
                DateTime dtNow = DateTime.Now;

                //ファイルパス
                string fileLog = logDirectory + @"\" + dtNow.ToString("yyyyMMdd") + Const_Extention;
                string bkFileLog = bkDirectory + @"\" + dtNow.ToString("yyyyMMdd") + Const_Extention;

                //ファイル既存チェック
                if (File.Exists(fileLog))
                {
                    //ファイルは1兆サイズ超える場合、BackUpフォルダに移動
                    FileInfo fileInfo = new FileInfo(fileLog);
                    if (fileInfo.Length / 1024 > 1024)
                    {
                        File.Copy(fileLog, bkFileLog, true);
                        File.Delete(fileLog);
                    }
                }

                //ログを出力
                using (StreamWriter sr = new StreamWriter(fileLog, true, Const_Encoding))
                {
                    sr.WriteLine(dtNow.ToString("yyyy/MM/dd HH:mm:ss.fff") + " " + message);
                    sr.Flush();
                    sr.Close();
                }
            }
            catch (IOException)
            {
                throw;
            }
            finally
            {
                mu.ReleaseMutex();
            }
        }
        #endregion

        #region Traceログを出力
        /// <summary>
        /// Traceログを出力
        /// </summary>
        /// <param name="ex"></param>
        public static void TraceLog(Exception ex)
        {
            int threadID = Thread.CurrentThread.ManagedThreadId;

            StackTrace st = new StackTrace(ex);
            string methodName = string.Empty;
            string className = string.Empty;
            foreach (StackFrame sf in st.GetFrames())
            {
                methodName = sf.GetMethod().Name;
                if (sf.GetMethod().ReflectedType == null) continue;

                className = sf.GetMethod().ReflectedType.FullName;
                if (className != st.GetFrame(0).GetMethod().ReflectedType.FullName
                    && methodName != "OnExceptionOccured")
                {
                    break;
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(threadID);
            sb.Append(Const_DelimiterTab);
            sb.Append(className);
            sb.Append(Const_DelimiterTab);
            sb.Append(methodName);
            sb.Append(Const_DelimiterTab);
            sb.Append(ex.Message);
            sb.Append(Const_DelimiterTab);
            sb.Append(ex.StackTrace);

            WriteLog(sb.ToString());
        }
        #endregion

        #region リクエストログ
        /// <summary>
        /// リクエストログ
        /// </summary>
        public static void LogRequest()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(HttpContext.Current.Request.RequestType);
            sb.Append(Const_delimiterSpace);
            sb.Append(HttpContext.Current.Response.StatusCode);
            sb.Append(Const_delimiterSpace);
            sb.Append(HttpContext.Current.Request.RawUrl);
            sb.Append(Const_delimiterSpace);
            sb.Append("User:" + GetUserID());
            sb.Append(Const_delimiterSpace);
            sb.Append("IP:" + GetIP());

            WriteLog(sb.ToString());
        }
        #endregion

        #region ユーザコードを取得する
        /// <summary>
        /// ユーザコードを取得する
        /// </summary>
        /// <returns>ユーザコード</returns>
        static private string GetUserID()
        {
            if (HttpContext.Current.User != null)
            {
                return HttpContext.Current.User.Identity.Name;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region ＩＰアドレスを取得する
        /// <summary>
        /// ＩＰアドレスを取得する
        /// </summary>
        /// <returns>ＩＰアドレス</returns>
        static private string GetIP()
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        #endregion
    }
}
