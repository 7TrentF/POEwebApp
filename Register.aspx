<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication1.Register" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body style="background-color: black; color: white;">
      <h1 >Register</h1>
<br />
<br />

<div>

    <form id="form1" runat="server">
   <div>
       <table align="center">
           <tr> 
               <td id="Username"> Username&nbsp;</td>
               <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
           </tr>   
         
                 <tr>
      <td id="Password"> Password</td>
      <td> <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>     
     </tr>  

            <tr>
 <td>&nbsp;</td>
 <td> <asp:Button ID="Button1" runat="server" Text="Register" BackColor="#CC00FF" Font-Bold="True" ForeColor="White" OnClick="BtnLogin_Click" Width="93px" /></td>
 <td></td>
</tr>

       </table>
   </div>
</form>

   
          
</div>
</body>
</html>
 