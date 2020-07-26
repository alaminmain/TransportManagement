<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/login.aspx.cs" Inherits="TransportManagerUI.login" %>

<!DOCTYPE html>

<html >
<head>
  <meta charset="UTF-8">
  <title>Transport Management</title>
  
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/meyer-reset/2.0/reset.min.css">
<link href="Styles/loginpage.css" rel="stylesheet" />
  <link rel='stylesheet prefetch' href='http://fonts.googleapis.com/css?family=Roboto:400,100,300,500,700,900|RobotoDraft:400,100,300,500,700,900'>
<link rel='stylesheet prefetch' href='http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css'>

      <link rel="stylesheet" href="css/style.css">

  
</head>

<body>
  
<!-- Form Mixin-->
<!-- Input Mixin-->
<!-- Button Mixin-->
<!-- Pen Title-->
<div class="pen-title">
  <h1>Welcome to Transport Management</h1>
</div>
<!-- Form Module-->
<div class="module form-module">
  <div class="toggle" style="right: 0; top: 0"><i class="fa fa-times fa-pencil"></i>
    
  </div>
  <div class="form">
    <h2>Login to your account</h2>
    <form runat="server">
    <asp:TextBox ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
    <asp:Button ID="btnLogin" runat="server" Text="Login" 
        onclick="btnLogin_Click" />
        <p>
    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label></p>
    </form>
  </div>
  
 
</div>
  <script src='http://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min.js'></script>


    <script src="js/index.js"></script>

</body>
</html>
