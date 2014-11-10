﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SkyServer.Tools.Explore
{
    public partial class ImagingControl : System.Web.UI.UserControl
    {        
        protected Globals globals;
        protected ObjectExplorer master;        

        //--- phototag
        protected string flag; //0
        protected double ra = 0;//1
        protected double dec = 0;//2
        protected int run;//3
        protected int rerun;//4
        protected int camcol;//5
        protected long field;//6
        protected string fieldId;//7
        protected string objId;//8

        //--- PhotoObjAll
        public int clean;//14
        protected string otype;//15
        protected double u;//16
        protected double g;//17
        protected double r;//18
        protected double i;//19
        protected double z;//20
        protected double err_u;//21
        protected double err_g;//22
        protected double err_r;//23
        protected double err_i;//24
        protected double err_z;//25

        //--- PhotoObj
        protected string mode;//26
        protected int mjdNum;//27
        protected int otherObs;//28
        protected long parentId;//29
        protected int nchild;//30
        protected string extinction_r;//31
        protected string petrorad_r;//32

        //--- PhotoZ, photoZRF
        protected string photoZ_KD;//33
        protected string photoZ_RF;//34
        protected string galaxyZoo_Morph;//35

        protected string sdssUrl;

        protected string flagsLink = "";

        public void Page_Load(object sender, EventArgs e)
        {
            globals = (Globals)Application[Globals.PROPERTY_NAME];            
            master  = (ObjectExplorer)Page.Master;
            try
            {
                objId = Request.QueryString["id"];
            }
            catch (Exception exp) {
                //If the querystring is empty and no objid key
                objId = null;
            }
            sdssUrl = globals.SdssUrl;
            flagsLink = sdssUrl + "algorithms/photo_flags_recommend.php";

            if(objId != null && !objId.Equals(""))
            runQuery();
        }

        private void runQuery()
        {
            DataSet ds = master.runQuery.RunCasjobs(master.exploreQuery.getImagingQuery(objId));
            using (DataTableReader reader = ds.Tables[0].CreateDataReader())
            {
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        //// ---
                        flag = (string) reader["flags"];
                        ra =  (double) reader["ra"];
                        dec = (double) reader["dec"];
                        run = reader["run"] is DBNull ? -9 : (short)reader["run"];
                        rerun = reader["rerun"] is DBNull ? -9 : (short)reader["rerun"];
                        camcol = reader["camcol"] is DBNull ? -9 : (byte)reader["camcol"];
                        field = reader["field"] is DBNull ? -9 : (short)reader["field"];
                        fieldId =RunQuery.checkNullorParse(reader.GetValue(7));
                        objId = RunQuery.checkNullorParse(reader.GetValue(8));

                        //photoObjall
                        clean = reader["clean"] is DBNull ? -99999 : (int)reader["clean"]; ;
                        otype = reader["clean"] is DBNull ? "" :(string)reader["otype"];

                        ////--- magnitudes
                        u = reader["u"] is DBNull ? -999.99 : (float)reader["u"];
                        g = reader["u"] is DBNull ? -999.99 : (float)reader["g"];
                        r = reader["u"] is DBNull ? -999.99 : (float)reader["r"];
                        i = reader["u"] is DBNull ? -999.99 : (float)reader["i"];
                        z = reader["u"] is DBNull ? -999.99 : (float)reader["z"];

                        ////--- mag errors
                        err_u = reader["err_u"] is DBNull ? -999.99 : (float)reader["err_u"];
                        err_g = reader["err_g"] is DBNull ? -999.99 : (float)reader["err_g"];
                        err_r = reader["err_r"] is DBNull ? -999.99 : (float)reader["err_r"];
                        err_i = reader["err_i"] is DBNull ? -999.99 : (float)reader["err_i"];
                        err_z = reader["err_z"] is DBNull ? -999.99 : (float)reader["err_z"];

                        ////--- PhotoObj
                        mode = reader["mode"] is DBNull ? "" : (string)reader["mode"];

                        mjdNum = reader["mjdNum"] is DBNull ? -9 :(int) reader["mjdNum"];

                        otherObs = reader["Other observations"] is DBNull ? -99999 : (int)reader["Other observations"];

                        parentId = reader["parentID"] is DBNull ? -99999 : (long)reader["parentID"];

                        nchild = reader["nChild"] is DBNull ? -999: (short)reader["nChild"];

                        extinction_r = reader.GetValue(26).ToString();

                        petrorad_r = reader.GetValue(27).ToString();

                        ////--- PhotoZ, photoZRF
                        photoZ_KD = (string)reader["photoZ_KD"];

                        photoZ_RF = (string)reader["photoZ_RF"];

                        galaxyZoo_Morph = (string)reader["galaxyZoo_Morph"];
                    }
                }
            }
        }

        protected string getUnit(string tablename, string columname) {
            string unit = "";
             DataSet ds = master.runQuery.RunCasjobs(master.exploreQuery.getUnit(tablename,columname));
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         unit = reader.GetString(0);
                     }
                 }
             }
             return unit;
        }
    }
}