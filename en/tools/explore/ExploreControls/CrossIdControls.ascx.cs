﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SkyServer.Tools.Explore
{
    public partial class CrossIdControls : System.Web.UI.UserControl
    {
        protected Globals globals;
        protected ObjectExplorer master;

        protected string objId;

        protected string usno;
        protected string properMotion;
        protected float angle;

        protected string first;
        protected string peakflux;
        protected float major;
        protected float minor;

        protected string rosat;
        protected float cps;
        protected float hr1;
        protected float hr2;
        protected float ext;

        protected string rc3;
        protected string hubletype;
        protected string magnitude;
        protected float hydrogenIndex;

        protected string wise;
        protected float wmag1;
        protected float wmag2;
        protected float wmag3;
        protected float wmag4;
        protected string wiselink;
        protected string linkQuery;

        protected string twomass;
        protected float j;
        protected float h;
        protected float k;
        protected string phQual;

        protected bool isUSNO = false;
        protected bool isFIRST = false;
        protected bool isROSAT = false;
        protected bool isRC3 = false;
        protected bool isWISE = false;
        protected bool is2MASS = false;

        protected RunQuery runQuery;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = (Globals)Application[Globals.PROPERTY_NAME];
            master = (ObjectExplorer)Page.Master;
            objId = master.objId;
            runQuery = new RunQuery();
            if (master.objId != null && !master.objId.Equals(""))
            execQuery();
        }

        private void execQuery() { 
            
            //USNO Query
            string cmd = ExplorerQueries.USNO.Replace("@objId",objId);
             DataSet ds = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                        isUSNO = true;
                        usno = reader.GetString(0);
                        properMotion = reader.GetString(1);
                        angle = reader.GetFloat(2);
                     }
                 }
             }
             //FIRST Query   
             cmd = ExplorerQueries.FIRST.Replace("@objId", objId);
             ds  = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                        isFIRST = true;
                        first = reader.GetString(0);
                        peakflux = reader.GetString(1);
                        major = reader.GetFloat(2);
                        minor = reader.GetFloat(3);
                     }
                 }
             }

             //ROSAT Query
             cmd = ExplorerQueries.ROSAT.Replace("@objId", objId);
             ds  = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         isROSAT = true;
                         rosat = reader.GetString(0);
                         cps = reader.GetFloat(1);
                         hr1 = reader.GetFloat(2);
                         hr2 = reader.GetFloat(3);
                         ext = reader.GetFloat(4);
                     }
                 }
             }

             //RC3
             cmd = ExplorerQueries.RC3.Replace("@objId", objId);
             ds  = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         isRC3 = true;
                         rc3 = reader.GetString(0);
                         hubletype = reader.GetString(1);
                         magnitude = reader.GetString(2);
                         hydrogenIndex = reader.GetFloat(3);
                     }
                 }
             }

             //WISE
             linkQuery = ExplorerQueries.wiseLinkCrossId.Replace("@objId", objId);
            
             
             cmd = ExplorerQueries.WISE.Replace("@objId", objId);
             ds  = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         isWISE = true;
                         wise = reader.GetString(0);
                         wmag1 = reader.GetFloat(1);
                         wmag2 = reader.GetFloat(2);
                         wmag3 = reader.GetFloat(3);
                         wmag4 = reader.GetFloat(4);
                         wiselink = reader.GetString(5);
                     }
                 }
             }

             //TWOMASS
             cmd = ExplorerQueries.TWOMASS.Replace("@objId", objId);
             ds = runQuery.RunCasjobs(cmd);
             using (DataTableReader reader = ds.Tables[0].CreateDataReader())
             {
                 if (reader.Read())
                 {
                     if (reader.HasRows)
                     {
                         is2MASS = true;
                         twomass = reader.GetString(0);
                         j = reader.GetFloat(1);
                         h = reader.GetFloat(2);
                         k = reader.GetFloat(3);
                         phQual = reader.GetString(4);
                     }
                 }
             }

        }
    }
}