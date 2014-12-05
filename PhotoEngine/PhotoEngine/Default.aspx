<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PhotoEngine._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Photo Engine</h1>
    </div>

    <div class="row">
        <div class="col-sm-12 contentUpload">
            <asp:FileUpload ID="fileUploader" runat="server" />
            <asp:RegularExpressionValidator ID="photoValidator" runat="server" ControlToValidate="fileUploader"
                ErrorMessage="Only images allowed! (jpeg, png)"
                ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.jpg|.png)$" Display="Dynamic"></asp:RegularExpressionValidator><br/>
            <asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_OnClick" CssClass="btn btn-block btn-default" /> <br/>
            <asp:Panel ID="panelPreview" runat="server" Visible="false">
                <asp:Image ID="imgPreview" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="Upload" OnClick="btnSave_OnClick" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" CssClass="btn btn-warning" />
            </asp:Panel>
        </div>
    </div>

</asp:Content>
