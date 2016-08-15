<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showData.aspx.cs" Inherits="report_webform.showData" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Style.css" rel="stylesheet" />
</head>
<body onload="createTable()">
    <form id="form1" runat="server">
    
        <div class="table-responsive">
              <table class=" table table-striped table-bordered table-hover table-condensed" border="1">
              <thead>
              <tr>
              <th>PO Number</th>
              <th class=  "number" >Invoice Total</th>
              <th class=  "number" >Line Total</th>   
              <th class=  "number" >Variance</th>  
              <th class=  "view" >Action</th>                                                                
              </tr>                                  
              </thead>
                  <tbody id="tableResults" runat="server">
                      </tbody>
              </table>
              </div>                              

    </form>

<script src="jquery-1.9.1.min.js"></script>
<script src="scripts/bootstrap.min.js"></script> 
</body>
</html>
