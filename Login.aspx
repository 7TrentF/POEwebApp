<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <h1 >Login</h1>
<br />
<br />

<div>
    <table align="center">
        <tr> 
        <td id="Username">  Username&nbsp;</td>
        <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
       </tr>    

         <tr>
         <td id="Password"> Password</td>
         <td> <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>     
        </tr>  
       
      

         <tr>
         <td> &nbsp;</td>
         <td> &nbsp;</td>     
        </tr>  
       
      

         <tr>
         <td>&nbsp;</td>
         <td> <asp:Button ID="BtnLogin" runat="server" Text="Login" BackColor="#CC00FF" Font-Bold="True" ForeColor="White" OnClick="BtnLogin_Click" Width="93px"/></td>
         <td><asp:Button ID="btnExit" runat="server" Text="Exit" BackColor="#CC00FF" Font-Bold="True" ForeColor="White" OnClick="btnExit_Click" Width="93px"/></td>
        </tr>
         </table>
          <a href="Register.aspx" target="_self" id="RegisterHyperLink" runat="server" OnClick="CheckAuthenticationAndRedirect"> <h5>Don't have an account click here to register </h5> </a>
</div>
</asp:Content>
