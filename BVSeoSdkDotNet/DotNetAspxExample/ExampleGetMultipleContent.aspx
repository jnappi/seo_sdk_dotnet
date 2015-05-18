<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExampleGetMultipleContent.aspx.cs" Inherits="DotNetAspxExample.ExampleGetMultipleContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body onhashchange="hashChanged();">
    <form id="bvform" runat="server">
        <asp:Label ID="lblContentType1" runat="server">ContentType1: </asp:Label>
        <asp:ListBox runat="server" id="ContentType1" SelectionMode="Single" Rows="1" AutoPostBack="true" OnSelectedIndexChanged="ContentType_Changed">
            <asp:ListItem Text="questions" Value="q"></asp:ListItem>
            <asp:ListItem Text="reviews" Value="r"></asp:ListItem>
            <asp:ListItem Text="spotlights" Value="sp"></asp:ListItem>
            <asp:ListItem Text="stories" Value="s"></asp:ListItem>
        </asp:ListBox>
        <!-- BV reviews content -->
	    <div id="BVRRSummaryContainer1" runat="server"></div>	
	    <div id="BVRRContainer1" runat="server"></div>
        <!-- BV non-reviews content -->
        <div id="BVContent1" runat="server"></div>
        <br />
        <asp:Label ID="lblContentType2" runat="server">ContentType2: </asp:Label>
        <asp:ListBox runat="server" id="ContentType2" SelectionMode="Single" Rows="1" AutoPostBack="true" OnSelectedIndexChanged="ContentType_Changed">
            <asp:ListItem Text="questions" Value="q"></asp:ListItem>
            <asp:ListItem Text="reviews" Value="r"></asp:ListItem>
            <asp:ListItem Text="spotlights" Value="sp"></asp:ListItem>
            <asp:ListItem Text="stories" Value="s"></asp:ListItem>
        </asp:ListBox>
        <!-- BV reviews content -->
	    <div id="BVRRSummaryContainer2" runat="server"></div>	
	    <div id="BVRRContainer2" runat="server"></div>
        <!-- BV non-reviews content -->
        <div id="BVContent2" runat="server"></div>
        <input name="pageUrl" id="pageUrl" type="hidden" runat="server" />
        <script type="text/javascript">
            handlePageUrl();
            function savePageUrl() {
                if (document.getElementById("pageUrl")) {
                    document.getElementById("pageUrl").value = window.location.href;
                }
                if (document.getElementById("bvform")) {
                    document.getElementById("bvform").action = window.location.href;
                }
            }
            function hashChanged() {
                savePageUrl();
                document.getElementById("bvform").submit();
            }
            function handlePageUrl() {
                if (document.getElementById("pageUrl")) {
                    if (document.getElementById("pageUrl").value  !== window.location.href) {
                        savePageUrl();
                        document.getElementById("bvform").submit();
                    }
                }
            }
        </script>
    </form>
</body>
</html>
