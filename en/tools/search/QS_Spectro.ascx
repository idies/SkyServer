﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QS_Spectro.ascx.cs" Inherits="SkyServer.Tools.Search.QS_Spectro" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<table cellspacing='3' cellpadding='3' class='frame'>
<tr><td align=middle>
	<a class='qtitle' href=<%=userguide%>#SpectroscopyConstraints
		onmouseover="return escape('Apply constraints based on spectroscopic data on the objects returned by the query');">
		Spectroscopy Constraints</a></td>
</tr>
<tr><td>

  <table border=0 cellpadding=4 cellspacing=4 width=100%>
  <tr>
	<td class='q' rowspan=2 align=center width=75>
		<a href=<%=userguide%>#Redshift
		onmouseover="return escape('Apply constraints based on redshift');">
		Redshift, </a> <br>
		<a href=<%=userguide%>#SpectralClassification
		onmouseover="return escape('Apply constraints based upon spectroscopically classified object type');">
		Classification</a>
	</td>
	<td class='q'>min</td>
	<td class='q'>
		<table><tr>
		<td><a href=<%=userguide%>#Redshift
			onmouseover="return escape('minimum redshift');">redshift</a></td>
		<td><input type=text name=redshiftMin size=2 class='mag'></td>
		</tr></table>
	</td>
	<td class='q' rowspan=2 align=center>
          <table>
		  <tr valign=top>
			<td><a href=<%=userguide%>#Redshift
			   onmouseover="return escape('Redshift warning flag(s).  Unchecking this will remove the constraint that the redshift should have no warnings associated with it.');">Redshift Warning Flags</a></td>
		  </tr>
		  <tr>
		      <td><input type=checkbox name=zWarning class='mag' checked>No warnings</input></td>
		  </tr></table>
	</td>
	<td class='q' rowspan=2>
		<a href=<%=userguide%>#SpectralClassification
		onmouseover="return escape('Apply constraints based upon spectroscopically classified object type');">Classification</a><br>
		<select size=4 name=class multiple=multiple>
		<option>ALL</option>
		<option>STAR</option>
		<option>GALAXY</option>
		<option>QSO</option>
		</select>
	</td>
  </tr>
  <tr>
	<td class='q'>max</td>
	<td class='q'>
		<table><tr>
		<td><a href=<%=userguide%>#Redshift
			onmouseover="return escape('maximum redshift');">redshift</a></td>
		<td><input type=text name=redshiftMax size=2 class='mag'></td>
		</tr></table>
	</td>
  </tr>  
  </table>

  <table border=0 cellpadding=4 cellspacing=4 width=100%>
<%		
	Response.Write("<tr class='q'><td colspan=5 align=center class=smallbodytext>");
	Response.Write(" (<b>Shift-mouse</b> to select multiple <b>contiguous</b> entries, <b>Ctrl-mouse</b>");
	Response.Write(" to select <b>non-contiguous</b> entries)</td></tr>");
%>
  <tr>
	<td class='q' align=center width=75>
		<a href=<%=userguide%>#Primtarget
		onmouseover="return escape('Apply constraints based upon primary target selection bits');">
		Target Flags<br>(PRIMTARGET)</a>
	</td>
<%
    using (SqlConnection oConn = new SqlConnection(globals.ConnectionString))
    {
        oConn.Open();
        
        using (SqlCommand oCmd = oConn.CreateCommand())
        {
            string cmd = "SELECT [name] FROM DataConstants WHERE field='PrimTarget' ORDER BY value";
            oCmd.CommandText = cmd;
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Response.Write("<td colspan=4><b>No PrimTarget flags found in DataConstants table</b></td>\n");
                }
                else
                {
                    Response.Write("<td class='q' colspan=2><a href=\"" + url + "/help/docs/QS_UserGuide.aspx#PrimTarget\" onmouseover=\"return escape('");
                    Response.Write("The bit-wise OR of all these bits must be greater than 0.');\"><strong>At least one of these bits ON</strong></a><br>\n");
                    Response.Write("\t<SELECT name=\"priFlagsOnList\" multiple=\"multiple+\" size=\"5\">\n");
                    Response.Write("\t\t<OPTION value=\"ignore\" selected>ignore</OPTION>\n");
                    List<string> values = new List<string>();
                    while (reader.Read())
                    {
                        values.Add(reader.GetSqlValue(0).ToString());
                    }
                    foreach (string v in values)
                    {
                        Response.Write("\t\t<OPTION value=\"" + v + "\">" + v + "\n");
                    }
                    Response.Write("\t</OPTION></SELECT>\n");


                    Response.Write(" </td><td class='q' colspan=2><a href=\"" + url + "/help/docs/QS_UserGuide.aspx#PrimTarget\" onmouseover=\"return escape('");
                    Response.Write("The bit-wise AND of all these bits must be equal to 0.');\"><strong>All of these bits OFF</strong></a><br>\n");
                    
                    Response.Write("\t<SELECT name=\"priFlagsOffList\" multiple=\"multiple+\" size=\"5\">\n");
                    Response.Write("\t\t<OPTION value=\"ignore\" selected>ignore</OPTION>\n");
                    foreach (string v in values)
                    {
                        Response.Write("\t\t<OPTION value=\"" + v + "\">" + v + "\n");
                    }
                    Response.Write("\t</OPTION></SELECT>\n");
                    Response.Write(" </td>\n");
                }
            } // using SqlDataReader
        } // using SqlCommand
%>
  </tr>
  <tr>
	<td class='q' align=center width=75> 
		<a href=<%=userguide%>#Sectarget 
		onmouseover="return escape('Apply constraints based upon secondary target selection bits');">
		Target Flags<br>(SECTARGET)</a>
	</td>
<%      using (SqlCommand oCmd = oConn.CreateCommand())
        {
            string cmd = "SELECT [name] FROM DataConstants WHERE field='SecTarget' ORDER BY value";
            oCmd.CommandText = cmd;
            using (SqlDataReader reader = oCmd.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Response.Write("<td colspan=4><b>No SecTarget flags found in DataConstants table</b></td>\n");
                }
                else
                {
                    Response.Write("<td class='q' colspan=2><a href=\"" + url + "/help/docs/QS_UserGuide.aspx#SecTarget\" onmouseover=\"return escape('");
                    Response.Write("The bit-wise OR of all these bits must be greater than 0.');\"><strong>At least one of these bits ON</strong></a><br>\n");
                    Response.Write("\t<SELECT name='secFlagsOnList' multiple='multiple+' size=5>\n");
                    Response.Write("\t\t<OPTION value=\"ignore\" selected>ignore</OPTION>\n");
                    List<string> values = new List<string>();
                    while (reader.Read())
                    {
                        values.Add(reader.GetSqlValue(0).ToString());
                    }

                    foreach (string v in values)
                    {
                        Response.Write("\t\t<OPTION value='" + v + "'>" + v + "\n");
                    }
                    Response.Write("\t</OPTION></SELECT>\n");

                    Response.Write(" </td><td class='q' colspan=2><a href=\"" + url + "/help/docs/QS_UserGuide.aspx#SecTarget\" onmouseover=\"return escape('");
                    Response.Write("The bit-wise AND of all these bits must be equal to 0.');\"><strong>All of these bits OFF</strong></a><br>\n");

                    Response.Write("\t<SELECT name=\"secFlagsOffList\" multiple=\"multiple+\" size=\"5\">\n");
                    Response.Write("\t\t<OPTION value=\"ignore\" selected>ignore</OPTION>\n");
                    foreach (string v in values)
                    {
                        Response.Write("\t\t<OPTION value=\"" + v + "\">" + v + "\n");
                    }
                    Response.Write("\t</OPTION></SELECT>\n");
                    Response.Write(" </td>\n");
                }
            } // using SqlDataReader
        } // using SqlCommand
    } // using SqlConnection
%>
  </tr>
  </table>

<table width="100%">
		<tr>
			<td align="left"><input id=submit type="submit" value="Submit Request"></td>
			<td colspan="2">&nbsp;</td>
			<td align="right"><input id=reset  type="reset" value="Reset Form"></td>
		</tr>
</table>
