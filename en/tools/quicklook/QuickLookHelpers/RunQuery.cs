﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;

namespace SkyServer.Tools.QuickLook
{
    // use casjobs to run
    public class RunQuery 
    {

        private Globals globals;
        
        private string token = "";

        string[] injection = new string[] { "--", ";", "/*", "*/", "'", "\"" };

        string requestUri;
        string WSrequestUri;

        public RunQuery() {            
            globals = new Globals();
            requestUri = globals.CasjobsRESTapi;
            WSrequestUri = globals.DatabaseSearchWS;
        }

        public RunQuery(string token) {

            globals = new Globals();
            requestUri = globals.CasjobsRESTapi;
            WSrequestUri = globals.DatabaseSearchWS;
            this.token = token;

        }



        public string GetClientIP()
        {
            string clientIP = "unknown";
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    clientIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else
                {
                    if (HttpContext.Current.Request.UserHostAddress != null)
                    {
                        clientIP = HttpContext.Current.Request.UserHostAddress;
                    }
                    else
                    {
                        clientIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                if (clientIP == "")
                    clientIP = "unspecified";
            }
            catch { }
            return clientIP;
        }



        public DataSet RunDatabaseSearch(string command, string format, string ClientIP, string TaskName)
        {

            WebRequest req = WebRequest.Create(WSrequestUri + "?cmd=" + Uri.EscapeDataString(command) + "&format=" + format + "&clientIP=" + ClientIP + "&task=" + TaskName);//select%20top%2010%20ra,dec%20from%20Frame&format=csv"
            WebResponse resp = req.GetResponse();
            BinaryFormatter fmt = new BinaryFormatter();
            DataSet ds = new DataSet();
            ds = (DataSet)fmt.Deserialize(resp.GetResponseStream());
            return ds;
            //Stream s = resp.GetResponseStream();
            //StreamReader sr = new StreamReader(s, Encoding.ASCII);
            //string doc = sr.ReadToEnd();
        }





         //<summary>
         
         //</summary>
         //<param name="command"></param>
         //<returns></returns>
        public DataSet RunCasjobs(string command, string taskname)
        {
           // throw new IndexOutOfRangeException("There is an invalid argument");
            
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(requestUri);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/x-dataset";
               
                if(!token.Equals("") && token != null)
                    request.Headers.Add("X-Auth-Token", token);

                StreamWriter streamWriter = new StreamWriter(request.GetRequestStream());
                StringWriter sw = new StringWriter();
                JsonWriter jsonWriter = new JsonTextWriter(sw);
                jsonWriter.WriteStartObject();
                jsonWriter.WritePropertyName("Query");
                jsonWriter.WriteValue(command);
                jsonWriter.WritePropertyName("TaskName");
                jsonWriter.WriteValue(taskname);
                //jsonWriter.WritePropertyName("ReturnDataSet");
                //jsonWriter.WriteValue(true);
                jsonWriter.WriteEndObject();
                jsonWriter.Close();
                streamWriter.Write(sw.ToString());
                streamWriter.Close();

                DataSet ds = null;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    BinaryFormatter fmt = new BinaryFormatter();
                    ds = (DataSet)fmt.Deserialize(response.GetResponseStream());
                }
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception("There is an error running this Query.\n Query:" + command + " ");
                
            }
        }

        ///// <summary>
        ///// Directly connect to database
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //public DataSet RunCasjobs(string command)
        //{
        //    DataSet ds = new DataSet();
        //    using (SqlConnection oConn = new SqlConnection(globals.ConnectionString))
        //    {
        //        oConn.Open();
        //        using (SqlCommand oCmd = oConn.CreateCommand())
        //        {
        //            oCmd.CommandText = command;
        //            using (SqlDataAdapter da = new SqlDataAdapter(command, globals.ConnectionString))
        //            {
        //                da.Fill(ds);
        //            }
        //        }
        //    }
        //    return ds;
        //}

        ////***this was added to use the namevalue pair options
        //// Just kept for the reference.

        //private NameValueCollection runDb(string query)
        //{
        //    NameValueCollection nvc = new NameValueCollection();

        //    using (SqlConnection oConn = new SqlConnection(global.ConnectionString))
        //    {
        //        oConn.Open();
        //        using (SqlCommand oCmd = oConn.CreateCommand())
        //        {
        //            oCmd.CommandText = query;
        //            using (SqlDataReader reader = oCmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        //string[] names = new string[reader.FieldCount];
        //                        for (int i = 0; i < reader.FieldCount; i++)
        //                        {
        //                            nvc.Add(reader.GetName(i), reader.GetValue(i).ToString());
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    return nvc;
        //}
    }
}