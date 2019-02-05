<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SPDataGridWPUserControl.ascx.cs" Inherits="SPDataGridWP.VisualWebPart.SPDataGridWPUserControl" %>

<asp:Label runat="server" ForeColor="Red" ID="errorLabel"></asp:Label>
<SharePoint:SPGridView ID="gridView" AutoGenerateColumns="false" AllowPaging="true" PageSize="3" runat="server"
     OnRowEditing="gridView_RowEditing" OnRowUpdating="gridView_RowUpdating" OnRowCancelingEdit="gridView_RowCancelingEdit"
     OnPageIndexChanging="gridView_PageIndexChanging">

    <Columns>
        <asp:TemplateField HeaderText="Risk ID">
            <ItemTemplate>
                <asp:Label ID="lblRiskId" runat="server" Text='<%# Eval("RiskID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Risk Title">
            <ItemTemplate>
                <asp:Label ID="lblRiskTitle" runat="server" Text='<%#Eval("RiskTitle") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtRiskTitle" runat="server" Text='<%#Eval("RiskTitle") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Risk Description">
            <ItemTemplate>
                <asp:Label ID="lblRiskDescription" runat="server" Text='<%#Eval("RiskDescription") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtRiskDescription" runat="server" Text='<%#Bind("RiskDescription") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Lifetime EBITDA Value">
            <ItemTemplate>
                <asp:Label ID="lblEBITDA" runat="server" Text='<%#Eval("LifetimeEBITDAValue") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="txtEBITDA" runat="server" Text='<%#Bind("LifetimeEBITDAValue") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>

        <asp:CommandField ShowEditButton="True" HeaderText="" />
        <asp:TemplateField HeaderText="">
            <ItemTemplate>
                <asp:ImageButton ID="imgDelete" runat="server" ImageUrl="/_layouts/images/delete.gif"
                    OnClick="imgDelete_Click" Style="height: 15px" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerStyle HorizontalAlign="Center" VerticalAlign="Middle"
Font-Size="14pt" />
</SharePoint:SPGridView>