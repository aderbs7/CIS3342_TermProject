﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DatingSiteLibrary;
using Utilities;

namespace TermProject
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)Session["Username"]))
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e)
        {
            lblErrorMsg.Text = string.Empty;
            lblErrorMsg.Visible = false;

            if (validateFields())
            {
                DBConnect objDB = new DBConnect();
                SqlCommand objCmd = new SqlCommand();
                User tempUser = new User();
                UserProfile profile = new UserProfile();
                UserPrivacySettings privacySettings = new UserPrivacySettings();
                string defaultPrivacySetting = "Visible";
                int result = 0, imageSize;
                string fileExt, imageName;
                string haveKids = drpHaveKids.SelectedValue; 
                string wantKids = drpWantKids.SelectedValue;
                string commitment = drpCommitment.SelectedValue;
                string gender = drpGender.SelectedValue;
                int userID = tempUser.getUserID(Session["Username"].ToString());
                string height = txtHeightFeet.Text + "|" + txtHeightIn.Text;

                try
                {
                    if (fileProfilePic.HasFile)
                    {
                        imageSize = fileProfilePic.PostedFile.ContentLength;
                        byte[] imageData = new byte[imageSize];

                        fileProfilePic.PostedFile.InputStream.Read(imageData, 0, imageSize);
                        imageName = fileProfilePic.PostedFile.FileName;

                        fileExt = imageName.Substring(imageName.LastIndexOf("."));
                        fileExt = fileExt.ToLower();

                        //string imageDataAsString = System.Text.Encoding.Default.GetString(imageData);

                        if(fileExt == ".jpg")
                        {                          
                            result = profile.addUserProfileToDB(txtPhone.Text, imageData, txtOccupation.Text, int.Parse(txtAge.Text), height, int.Parse(txtWeight.Text),
                                                       txtTitle.Text, commitment, haveKids, wantKids,txtInterests.Text, txtDescription.Text, userID, gender);
                            result += privacySettings.addUserSettingsToDB(defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting,
                                                            defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting,
                                                            defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, userID);
                            if(result == 2)
                            {
                                //User profile created successfully
                                Session.Add("RequestedProfile", Session["Username"].ToString());
                                Response.Redirect("Profile.aspx"); //redirect to member only search page or profile page?
                            }
                        }
                        else
                        {
                            lblErrorMsg.Text += "*Only jpg file types supported. <br />";
                            lblErrorMsg.Visible = true;
                        }
                    }
                    else //if no profile pic
                    {
                        byte[] noImage = null;
                        //inserts 'null' for the imageData field
                        result = profile.addUserProfileToDB(txtPhone.Text, noImage, txtOccupation.Text, int.Parse(txtAge.Text), height, int.Parse(txtWeight.Text),
                                                   txtTitle.Text, commitment, haveKids, wantKids, txtInterests.Text, txtDescription.Text, userID, gender);
                        result += privacySettings.addUserSettingsToDB(defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting,
                                                            defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting,
                                                            defaultPrivacySetting, defaultPrivacySetting, defaultPrivacySetting, userID);
                        if (result == 2)
                        {
                            //User profile created successfully
                            Session.Add("RequestedProfile", Session["Username"].ToString());
                            Response.Redirect("Profile.aspx"); //redirect to member only search page or profile page?
                        }
                    }
                }
                catch(Exception ex)
                {
                    lblErrorMsg.Text += "Error occurred: [" + ex.Message + "] cmd=" + result;
                    lblErrorMsg.Visible = true;
                }
            }
        }

        private bool validateFields()
        {
            bool valid = true;
            Int64 i;
            if (string.IsNullOrWhiteSpace(txtPhone.Text) || (txtPhone.Text.Length != 10) || !Int64.TryParse(txtPhone.Text, out i))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid phone number. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtOccupation.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your occupation. <br />";
                lblErrorMsg.Visible = true;
            }
            int age;
            if (string.IsNullOrWhiteSpace(txtAge.Text) || !int.TryParse(txtAge.Text, out age) || age <= 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid age. <br />";
                lblErrorMsg.Visible = true;
            }
            int heightFt, heightIn;
            if(string.IsNullOrWhiteSpace(txtHeightFeet.Text) || string.IsNullOrWhiteSpace(txtHeightIn.Text) || !int.TryParse(txtHeightFeet.Text, out heightFt) || 
               !int.TryParse(txtHeightIn.Text, out heightIn) || heightIn > 11 || heightIn < 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid height. <br />";
                lblErrorMsg.Visible = true;
            }
            int weight;
            if(string.IsNullOrWhiteSpace(txtWeight.Text) || !int.TryParse(txtWeight.Text, out weight) || weight <= 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a valid weight. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a title. <br />";
                lblErrorMsg.Visible = true;
            }
            if(drpCommitment.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your commitment preference. <br />";
                lblErrorMsg.Visible = true;
            }
            if (drpGender.SelectedIndex == 0)
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your gender. <br />";
                lblErrorMsg.Visible = true;
            }
            if ((drpHaveKids.SelectedIndex == 0) || (drpWantKids.SelectedIndex == 0))
            {
                valid = false;
                lblErrorMsg.Text += "*Please select your kids preference. <br />";
            }
            if (string.IsNullOrWhiteSpace(txtInterests.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter your interests. <br />";
                lblErrorMsg.Visible = true;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                valid = false;
                lblErrorMsg.Text += "*Please enter a description. <br />";
                lblErrorMsg.Visible = true;
            }

            return valid;
        }
    }
}