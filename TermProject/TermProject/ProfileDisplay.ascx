﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProfileDisplay.ascx.cs" Inherits="TermProject.ProfileDisplay" %>


    <asp:Table ID="tblProfle" runat="server" BorderStyle="None" CellPadding="10">
        <asp:TableRow>
            <asp:TableCell RowSpan="4">
                <asp:Image ID="imgProfilePic" runat="server" Height="160" Width="140" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2">
                <asp:Label ID="lblAge" runat="server" Text="Age: "></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Button ID="btnViewProfile" runat="server" Text="View Profile" OnClick="btnViewProfile_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>