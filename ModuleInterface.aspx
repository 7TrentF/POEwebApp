<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModuleInterface.aspx.cs" Inherits="WebApplication1.ModuleInterface" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   
        <meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0" />
<title><%: Page.Title %>- Ride You Rent Application</title>

<asp:PlaceHolder runat="server">
    <%: Scripts.Render("~/bundles/modernizr") %>
</asp:PlaceHolder>

<webopt:bundlereference runat="server" path="~/Content/css" />
<link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />



<style type="text/css">
    .auto-style1 {
        width: 693px;
        height: 244px;
    }
    .auto-style2 {
        margin-left: 607px;
    }
    .auto-style3 {
    width: 84px;
    display: block; /* Set the display property to block */
}
    .auto-style4 {
        width: 259px;
    }
    .auto-style5 {
        width: 50px;
    }

       body {
        background-color: black;
        color: white;
        font-family: Arial, sans-serif;
        margin: 0; /* Reset default margin */
        padding: 0; /* Reset default padding */
        display: flex;
        flex-direction: column;
        min-height: 100vh; /* Set the minimum height of the viewport */
    }

    .auto-style1 {
        width: 300px; /* Adjust the width as needed */
    }

  .module-list {
    width: 50%;
    margin-left:200px;
    margin-top: 0px; /* Add space above the list view */
    align-self: flex-start;
    box-sizing: border-box;
    padding-right: 100px;
}

  #Panel1 {
   margin-left: 50px;
   border: 2px white;
   margin-right: 500px;
   padding: 5px;
   height :800px;
   width:30%;
   
}
  .flex-container {
   display: flex;
   justify-content: space-between;
}

  #Panel1 form {
   width: 45%;
}

     #Panel2 {
       width: 70%;
        border: 2px white;
       margin-left: 30%;
       margin-top: 20px; /* Add space above the new panel */
       align-self: flex-start;
       box-sizing: border-box;
       padding-right: 100px;
   }


       #Panel1, #Panel3 {
   flex: 1 0 auto; /* Allow the items to shrink but not grow */
   margin: 10px; /* Add space around the items */
 }


      header {
       height: 100px;
       background-color: black;
       color: white;
       text-align: center;
       padding: 10px;
       margin-bottom:50px
   }
    
</style>
</head>

<body style="background-color: black; color: white;">
     <header>
       <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark">
     <div class="container">
         <a class="navbar-brand" runat="server" href="~/">Time Management Application </a>
         <button type="button" class="navbar-toggler" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" title="Toggle navigation" aria-controls="navbarSupportedContent"
             aria-expanded="false" aria-label="Toggle navigation">
             <span class="navbar-toggler-icon"></span>
         </button>
         <div class="collapse navbar-collapse d-sm-inline-flex justify-content-between">
             <ul class="navbar-nav flex-grow-1">
                 <li class="nav-item"><a class="nav-link" runat="server" href="~/">Home</a></li>
                 <li class="nav-item"><a class="nav-link" runat="server" href="~/About">About</a></li>
                 <li class="nav-item"><a class="nav-link" runat="server" href="~/Login">LogIn</a></li>
             </ul>
         </div>
     </div>
 </nav>
   </header>

  <div style="display: flex; flex-direction: row; flex-wrap: wrap;">

         <asp:Panel ID="Panel1" runat="server" CssClass="flex-container">
         <form id="form1" runat="server">
                    <asp:ScriptManager runat="server">
      <Scripts>
          <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
          <%--Framework Scripts--%>
          <asp:ScriptReference Name="MsAjaxBundle" />
          <asp:ScriptReference Name="jquery" />
          <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
          <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
          <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
          <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
          <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
          <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
          <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
          <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
          <asp:ScriptReference Name="WebFormsBundle" />
          <%--Site Scripts--%>
      </Scripts>
           </asp:ScriptManager>


             
     <div class="auto-style1">
     <h3 style="text-align:center">Add Module</h3>
     <br />
  

         
         <%---------------------------------------------------------------------------Module-----------------------------------------------------------------------------------%>
 <div>
      <asp:Label ID="lblModuleCode" runat="server" Text="Module Code:"></asp:Label>
     <asp:TextBox ID="txtModule_Code" runat="server" OnTextChanged="txtModule_Code_TextChanged"></asp:TextBox>
 </div>
     <br /> 

 <div>
      <asp:Label ID="lblModuleName" runat="server" Text="Module Name:"></asp:Label> 
      <asp:TextBox ID="Module_Name_textBox" runat="server"></asp:TextBox>
</div>
     <br /> 

<div>
   <asp:Label ID="lblCredts" runat="server" Text="Credits:" style="display: block;"></asp:Label>
   <asp:TextBox ID="Module_Credits_textBox" runat="server" style="display: block;"></asp:TextBox>
</div>
     <br />   

 <div>
     <asp:Label ID="lblClassHrs" runat="server" Text="Class hours per week:"></asp:Label>
     <asp:TextBox ID="Class_Hours_Per_Week_textBox" runat="server"></asp:TextBox>
</div>
     <br /> 
         
 <div>
     <asp:Label ID="lblNumOfWeeks" runat="server" Text="Number Of Weeks per Semester:"></asp:Label>
     <asp:TextBox ID="Number_Of_Weeks_Per_Semester_textBox" runat="server"></asp:TextBox>
</div>
     <br /> 

         
<div>
    <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" style="display: block;"></asp:Label>
    <asp:TextBox ID="StartDatePicker" runat="server" TextMode="Date" style="display: block;"></asp:TextBox>
    
</div>
    <br /> 

<div>
 <br /> <!-- Add a line break to separate the buttons -->
<asp:Button runat="server" Text="Add" OnClick="AddModule_Click"  ForeColor="#FF00FFE3" BackColor="Black" Width="100px" BorderColor="White"></asp:Button>
<asp:Button runat="server" Text="List" OnClick="List_Click" ForeColor="#FF00FFE3" BackColor="Black" Width="100px" BorderColor="White" style="margin-bottom: 100px;"></asp:Button>
</div>  

          <hr />
           <h3 style="text-align:center">Self Study Planner </h3>
<div>
    <asp:Label ID="lblssModuleCode" runat="server" Text="Module Code:"></asp:Label>
    <asp:TextBox ID="ssModuleCode" runat="server"></asp:TextBox>
</div>
  <br /> 
<div>
    <asp:Label ID="lblHrsSpent" runat="server" Text="Hrs Spent:" style="display: block;"></asp:Label>
    <asp:TextBox ID="txtHrsSpent" runat="server" style="display: block;"></asp:TextBox>
</div>
  <br /> 
<div>
    <asp:Label ID="lblWorkDate" runat="server" Text="Date:" style="display: block;"></asp:Label>
    <asp:TextBox ID="txtWorkDate" runat="server" TextMode="Date" style="display: block;margin-bottom: 50px;"></asp:TextBox>
</div>

        
    <asp:Button runat="server" Text="Calculate" OnClick="Calculate_Click"  ForeColor="#FF00FFE3" BackColor="Black" Width="100px" BorderColor="White"></asp:Button>



  <br /> <br /><br /><br /><br />
              
</div>
</form>
</asp:Panel>
          <asp:Panel ID="Panel3" runat="server">
            <%---------------------------------------------------------------------------List View-----------------------------------------------------------------------------------%>
 <asp:ListView ID="ModuleListView" runat="server" DataKeyNames="ModuleID" OnSelectedIndexChanged="ModuleListView_SelectedIndexChanged" OnItemDataBound="ModuleListView_ItemDataBound">
    <LayoutTemplate>
        <table>
            <thead>
                <tr>
                    <th style="margin-right: 50px;">Module Code</th>
                    <th style="margin-right: 50px;">Module Name</th>
                    <th style="margin-right: 50px;">Credits</th>
                    <th style="margin-right: 5px;">Class Hours Per Week</th>
                    <th style="margin-right: 50px;">Weeks In Semester</th>
                    <th style="margin-right: 10px;">Self Study Hours</th>
                    <th>Start Date</th>
                </tr>
            </thead>
            <tbody runat="server" id="itemPlaceholder"></tbody>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr runat="server" style="color: #FF00FFE3; background-color: black;">
            <td><%# Eval("ModuleCode") %></td>
            <td><%# Eval("ModuleName") %></td>
            <td><%# Eval("Credits") %></td>
            <td><%# Eval("ClassHoursPerWeek") %></td>
            <td><%# Eval("WeeksInSemester") %></td>
            <td><%# Eval("SelfStudyHrs") %></td>
            <td><%# Eval("StartDate") %></td>
        </tr>
    </ItemTemplate>
</asp:ListView>

</asp:Panel>
      <br />
       <br />
       <br /> 
</div>
    
         <%---------------------------------------------------------------------------footer-----------------------------------------------------------------------------------%>
            <%--  <hr />
              <footer>
              <p>&copy; <%: DateTime.Now.Year %> - Time Management Application</p>
              </footer>
    --%>

   

</body>
</html>
