﻿<%@ Page Title="" Language="C#" MasterPageFile="ObjectExplorer.Master" AutoEventWireup="true" CodeBehind="parameters.aspx.cs" Inherits="SkyServer.Tools.Explore.Parameters" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="SkyServer.Tools.Explore" %>
<%@ Import Namespace="SkyServer" %>
<%@ Import Namespace="System.Data" %>

<asp:Content ID="Head1" ContentPlaceHolderID="OEHead" runat="server">    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="OEContent" runat="server">
    <%--<% string cmd; %>    --%>
    <div class="content">
          <table class="content">
        <tr>
            <td colspan="2">
                <h1>Parameters from Spectroscopic Fitting</h1>
                <p>For most objects, SDSS processing pipelines find estimates of the physical parameters most likely to have produced the observed spectrum. Many different estimates are available - the right one to use will depend on the problem at hand.</p>
                <p>Click on the name of each set of estimates for more information on the pipeline that produced those estimates (opens in a new window). Most objects will have either estimated stellar parameters or galaxy properties, but a few objects will have both.</p>
                <h2>Stellar Parameters</h2>
                <h3><a href="<%=globals.SdssUrl%>spectro/sspp.php" target='_blank'>SEGUE Stellar Parameters Pipeline (SSPP) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                <%
                    RunQuery runQuery = new RunQuery();
                    
                    DataSet ds = runQuery.RunCasjobs(fitsParametersSppParams);
                    master.showVTable(ds, 300);
                %>
                 <h3><a href="<%=globals.SdssUrl%>irspec/aspcap.php" target='_blank'>APOGEE Stellar Parameters and Chemical Abundances Pipeline (ASPCAP) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                <%--<p>&nbsp;</p>--%>
                 <h2><a href="<%=globals.SdssUrl%>spectro/galaxy.php" target='_blank'>Galaxy Property Estimates <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h2>
                 <h3><a href="<%=globals.SdssUrl%>spectro/galaxy_portsmouth.php#fitting" target='_blank'>Model Fitting: Portsmouth group (Spectro-photometric matching) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                 <table><tr><td>
                 <h4>Star Forming Model</h4>
                <%
                     ds = runQuery.RunCasjobs(fitsParametersStellarMassStarformingPort);
                     master.showVTable(ds, 250);
                %>     
                  </td><td></td><td>
                  <h4>Passive Model</h4>
                <%
                     ds = runQuery.RunCasjobs(fitsParameterSstellarMassPassivePort);
                     master.showVTable(ds, 250);
                %>     
                  </td></tr>
                 </table>             
                <h3><a href="<%=globals.SdssUrl%>spectro/galaxy_portsmouth.php#kinematics" target='_blank'>Stellar Kinematics: Portsmouth group (Emission Line Fluxes) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                <%
                    ds = runQuery.RunCasjobs(fitsParametersEmissionLinesPort);
                    master.showHTable(ds,600,"");
                %>
                <h3><a href="<%=globals.SdssUrl%>spectro/galaxy_wisconsin.php" target='_blank'>Wisconsin group (Principal Component Analysis) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                <h4>Assuming model spectra from Bruzal &amp; Charlot (2003)</h4>
                <%
                    ds = runQuery.RunCasjobs(fitsParametersStellarMassPCAWiscBC03);
                    master.showHTable(ds, 600, "SpecObjAll");
                %>
                <h4>Assuming model spectra from Maraston &amp; Str&ouml;mback (2011)</h4>
                <%
                    ds = runQuery.RunCasjobs(fitsParametersstellarMassPCAWiscM11);
                    master.showHTable(ds,600,"SpecObjAll");
                 %>
                <h3><a href="<%=globals.SdssUrl%>spectro/galaxy_granada.php" target='_blank'>Granada group (Flexible Stellar Population Synthesis) <img src='../../images/new_window_black.png' alt=' (new window)' /></a></h3>
                <h4>Early Galaxy Formation,Dust Attenuation</h4>
                <%
                     ds = runQuery.RunCasjobs(fitsParametersStellarmassFSPSGranEarlyDust);
                     master.showHTable(ds,600,"DUST");
                 %>
                 <h4>Early Galaxy Formation,No Dust Attenuation</h4>
                 <%
                     ds = runQuery.RunCasjobs(fitsParametersStellarmassFSPSGranEarlyNoDust);
                     master.showHTable(ds, 600, "DUST");
                 %>
                 <h4>Wide Range of Formation Times, Dust Attenuation</h4>
                 <%
                      ds = runQuery.RunCasjobs(fitsParametersStellarmassFSPSGranWideDust);
                      master.showHTable(ds, 600,"DUST");
                 %>
                 <h4>Wide Range of Formation Times,No Dust Attenuation</h4>
                 <%
                      ds = runQuery.RunCasjobs(fitsParametersStellarmassFSPSGranWideNoDust);
                      master.showHTable(ds, 600, "DUST");
                 %>                            
            </td>
         </tr>    
    </table>
</div>
</asp:Content>
