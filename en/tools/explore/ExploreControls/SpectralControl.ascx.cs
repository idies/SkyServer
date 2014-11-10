﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SkyServer.Tools.Explore
{
    public partial class SpectralControl : System.Web.UI.UserControl
    {
        protected Globals globals;
        protected ObjectExplorer master;

        protected string objId;
        protected string specId;     

        protected long plate;
        protected int mjd;
        protected int fiberid;
        protected string instrument;
        protected string objclass;
        protected double redshift_z;
        protected double redshift_err;
        protected string redshift_flags;
        protected string survey;
        protected string programname;
        protected int primary;
        protected int otherspec;
        protected string sourcetype;
        protected double veldisp;
        protected double veldisp_err;
        protected string targeting_flags;
        protected long? specObjId;

        protected void Page_Load(object sender, EventArgs e)
        {
            globals = (Globals)Application[Globals.PROPERTY_NAME];
            master = (ObjectExplorer)Page.Master;
            try
            {
                objId = Request.QueryString["id"];
                specId = Request.QueryString["spec"];
                specObjId = Utilities.ParseId(specId);
            }
            catch(Exception exp){
                specId = null;
            }
            if(specId != null && !specId.Equals(""))
            runQuery();
        }

        private void runQuery() {
            DataSet ds = master.runQuery.RunCasjobs(master.exploreQuery.getSpectroQuery(specId,objId));
            using (DataTableReader reader = ds.Tables[0].CreateDataReader())
            {
                if (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        plate = reader["plate"] is DBNull ? -99999 : (short)reader["plate"]; 

                        mjd = reader["mjd"] is DBNull ? -99999 : (int)reader["mjd"];

                        fiberid = reader["fiberid"] is DBNull ? -99999 : (short)reader["fiberid"];

                        instrument = reader["instrument"] is DBNull ? "" : (string)reader["instrument"];

                        objclass = reader["objclass"] is DBNull ? "" : (string)reader["objclass"];

                        redshift_z = reader["redshift_z"] is DBNull ? -999.99 : (float)reader["redshift_z"];

                        redshift_err = reader["redshift_err"] is DBNull ? -999.99 : (float)reader["redshift_err"];

                        redshift_flags = reader["redshift_flags"] is DBNull ? "" : (string)reader["redshift_flags"];

                        survey = reader["survey"] is DBNull ? "" : (string)reader["survey"];

                        programname = reader["programname"] is DBNull ? "" : (string)reader["programname"];

                        primary = reader["primary"] is DBNull ? -99999 : (short)reader["primary"];

                        otherspec = reader["otherspec"] is DBNull ? -99999 : (int)reader["otherspec"];

                        sourcetype = reader["sourcetype"] is DBNull ? "" : (string)reader["sourcetype"];

                        veldisp = reader["veldisp"] is DBNull ? -999.99 : (float)reader["veldisp"];

                        veldisp_err = reader["veldisp_err"] is DBNull ? -999.99 : (float)reader["veldisp_err"];

                        targeting_flags = reader["targeting_flags"] is DBNull ? "" : (string)reader["targeting_flags"];
                    }
                }
            }
        }
    }
}