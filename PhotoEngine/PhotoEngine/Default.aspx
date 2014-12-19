<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PhotoEngine._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Picturesque</h1>
    </div>
    <div class="row">
        <div class="col-sm-4"></div>
        <div class="col-sm-4 contentUpload">
            <div class="row">
                <div class="col-sm-12">
                    <asp:FileUpload ID="fileUploader" runat="server" />
                    <asp:RequiredFieldValidator ID="reqImageValidator" runat="server" ErrorMessage="Required Field" ControlToValidate="fileUploader" Display="Dynamic" ValidationGroup="PreviewGroup"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="formatValidator" runat="server" ErrorMesssage="Only images allowed! (jpeg, jpg or png)" 
                        ControlToValidate="fileUploader" ValidationGroup="PreviewGroup" ClientValidationFunction="UploadFileCheck"></asp:CustomValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <button class="btn btn-block btn-default" runat="server" OnServerClick="btnPreview_OnClick" ValidationGroup="PreviewGroup">Preview</button>
                    <%--<asp:Button ID="btnPreview" runat="server" Text="Preview" OnClick="btnPreview_OnClick" ValidationGroup="PreviewGroup" CssClass="btn btn-block btn-default" />--%>
                </div>
            </div>
            <asp:Panel ID="panelPreview" runat="server" Visible="false">
                <asp:Image ID="imgPreview" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="Upload" OnClick="btnSave_OnClick" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_OnClick" CssClass="btn btn-warning" />
            </asp:Panel>
        </div>
        <div class="col-sm-4"></div>
    </div>

</asp:Content>
