<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="ConvertImageToText.aspx.cs" Inherits="PHTWeb.api.ConvertImageToText" ValidateRequest="false" EnableSessionState="False"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="inputPanel" runat="server">
            <fieldset>
                <legend>File Upload</legend>
                <div class="form-group">
                    <label for="imageFile" runat="server">File:</label>
                    <input class="form-control" type="file" id="imageFile" runat="server" />
                    <span class="help-block">The file to be processed.</span>
                </div>
                <button id="submitFile" type="submit" class="btn btn-default" runat="server">Submit</button>
            </fieldset>
        </asp:Panel>
    </form>
</body>
</html>
