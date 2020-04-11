﻿<%@ Page Title="Profile Creation" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProfileCreation.aspx.cs" Inherits="TermProject.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col text-center">
                <br />
                <br />
                <h2>Create Your Profile</h2>
                <br />
                <br />
            </div>
        </div>
        <div class="row">
            <div class="col text-center">
                Phone: <asp:TextBox ID="txtPhone" runat="server" placeholder="Phone Number" TextMode="Phone"></asp:TextBox>  
                Occupation: <asp:TextBox ID="txtOccupation" runat="server" placeholder="Occupation" MaxLength="50"></asp:TextBox>
                <br />
                <br />
                Age: <asp:TextBox ID="txtAge" runat="server" placeholder="Age" class="col-1"></asp:TextBox>  
                Height: <asp:TextBox ID="txtHeightFeet" runat="server" placeholder="Ft" class="col-1"></asp:TextBox>  
                <asp:TextBox ID="txtHeightIn" runat="server" placeholder="In" class="col-1"></asp:TextBox>  
                Weight: <asp:TextBox ID="txtWeight" runat="server" placeholder="Weight" class="col-1"></asp:TextBox> 
                <br />
                <br />
                Title: <asp:TextBox ID="txtTitle" runat="server" placeholder="Title" class="col-5" MaxLength="50"></asp:TextBox>
                <br />
                <br />
                Commitment: <asp:DropDownList ID="drpCommitment" runat="server">
                    <asp:ListItem>Casual</asp:ListItem>
                    <asp:ListItem>Relationship</asp:ListItem>
                    <asp:ListItem>Marriage</asp:ListItem>
                </asp:DropDownList>
                Have Kids: <asp:DropDownList ID="drpHaveKids" runat="server">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                Want Kids: <asp:DropDownList ID="drpWantKids" runat="server">
                    <asp:ListItem>No</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                Interests: 
                <br />  
                <asp:TextBox ID="txtInterests" runat="server" placeholder="Interests" class="col-5" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                <br />
                <br />
                Description: 
                <br />  
                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description" class="col-5" TextMode="MultiLine" MaxLength="250"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnSaveProfile" class="btn btn-outline-primary" runat="server" Text="Save Profile" />
            </div>
        </div>
    </div>
</asp:Content>