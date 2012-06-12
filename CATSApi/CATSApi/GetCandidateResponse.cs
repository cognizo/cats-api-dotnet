// 
// GetCandidateResponse.cs
//  
// Author:
//       Graham Floyd <gfloyd@catsone.com>
// 
// Copyright (c) 2012 CATS Software, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Xml;

namespace CATS
{
    public class GetCandidateResponse : Response
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private string summary = String.Empty;
        public string Summary
        {
            get { return this.summary; }
        }
        
        private string firstName = String.Empty;
        public string FirstName
        {
            get { return this.firstName; }
        }
        
        private string middleName = String.Empty;
        public string MiddleName
        {
            get { return this.middleName; }
        }
        
        private string lastName = String.Empty;
        public string LastName
        {
            get { return this.lastName; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private string phoneHome = String.Empty;
        public string PhoneHome
        {
            get { return this.phoneHome; }
        }
        
        private string phoneCell = String.Empty;
        public string PhoneCell
        {
            get { return this.phoneCell; }
        }
        
        private string phoneWork = String.Empty;
        public string PhoneWork
        {
            get { return this.phoneWork; }
        }
        
        private string address = String.Empty;
        public string Address
        {
            get { return this.address; }
        }
        
        private string city = String.Empty;
        public string City
        {
            get { return this.city; }
        }
        
        private string state = String.Empty;
        public string State
        {
            get { return this.state; }
        }
        
        private string zip = String.Empty;
        public string Zip
        {
            get { return this.zip; }
        }
        
        private string source = String.Empty;
        public string Source
        {
            get { return this.source; }
        }
        
        private DateTime dateAvailable = DateTime.MinValue;
        public DateTime DateAvailable
        {
            get { return this.dateAvailable; }
        }
        
        private bool? canRelocate = null;
        public bool CanRelocate
        {
            get { return (bool)this.canRelocate; }
        }
        
        private string notes = String.Empty;
        public string Notes
        {
            get { return this.notes; }
        }
        
        private string keySkills = String.Empty;
        public string KeySkills
        {
            get { return this.keySkills; }
        }
        
        private string currentEmployer = String.Empty;
        public string CurrentEmployer
        {
            get { return this.currentEmployer; }
        }
        
        private DateTime dateCreated = DateTime.MinValue;
        public DateTime DateCreated
        {
            get { return this.dateCreated; }
        }
        
        private DateTime dateModified = DateTime.MinValue;
        public DateTime DateModified
        {
            get { return this.dateModified; }
        }
        
        private int owner = -1;
        public int Owner
        {
            get { return this.owner; }
        }
        
        private int enteredBy = -1;
        public int EnteredBy
        {
            get { return this.enteredBy; }
        }
        
        private string email1 = String.Empty;
        public string Email1
        {
            get { return this.email1; }
        }
        
        private string email2 = String.Empty;
        public string Email2
        {
            get { return this.email2; }
        }
        
        private string webSite = String.Empty;
        public string WebSite
        {
            get { return this.webSite; }
        }
        
        private bool? isHot = null;
        public bool IsHot
        {
            get { return (bool)this.isHot; }
        }
        
        private string desiredPay = String.Empty;
        public string DesiredPay
        {
            get { return this.desiredPay; }
        }
        
        private string currentPay = String.Empty;
        public string CurrentPay
        {
            get { return this.currentPay; }
        }
        
        private bool? isActive = null;
        public bool IsActive
        {
            get { return (bool)this.isActive; }
        }
        
        private string bestTimeToCall = String.Empty;
        public string BestTimeToCall
        {
            get { return this.bestTimeToCall; }
        }
        
        private int countryId = -1;
        public int CountryId
        {
            get { return this.countryId; }
        }
        
        private Dictionary<string, string> extraFields = new Dictionary<string, string>();
        public Dictionary<string, string> ExtraFields
        {
            get { return this.extraFields; }
        }
        
        public GetCandidateResponse(string xml) 
            : base(xml)
        {
        }
        
        protected override void ParseResponse(XmlDocument xml)
        {
            XmlNode item = xml.SelectSingleNode("/response/item");
            
            if (item == null)
            {
                return;
            }
            
            Int32.TryParse(item.SelectSingleNode("id").InnerText, out this.id);         
            this.summary = item.SelectSingleNode("summary").InnerText;
            this.firstName = item.SelectSingleNode("first_name").InnerText;
            this.middleName = item.SelectSingleNode("middle_name").InnerText;
            this.lastName = item.SelectSingleNode("last_name").InnerText;
            this.title = item.SelectSingleNode("title").InnerText;          
            this.phoneHome = item.SelectSingleNode("phone_home").InnerText;
            this.phoneCell = item.SelectSingleNode("phone_cell").InnerText;
            this.phoneWork = item.SelectSingleNode("phone_work").InnerText;
            this.address = item.SelectSingleNode("address").InnerText;
            this.city = item.SelectSingleNode("city").InnerText;
            this.state = item.SelectSingleNode("state").InnerText;
            this.zip = item.SelectSingleNode("zip").InnerText;
            this.source = item.SelectSingleNode("source").InnerText;
            DateTime.TryParse(item.SelectSingleNode("date_available").InnerText, out this.dateAvailable);          
            this.canRelocate = CATSApi.StringToBool(item.SelectSingleNode("can_relocate").InnerText);
            this.notes = item.SelectSingleNode("notes").InnerText;
            this.keySkills = item.SelectSingleNode("key_skills").InnerText;
            this.currentEmployer = item.SelectSingleNode("current_employer").InnerText;
            DateTime.TryParse(item.SelectSingleNode("date_created").InnerText, out this.dateCreated);
            DateTime.TryParse(item.SelectSingleNode("date_modified").InnerText, out this.dateModified);
            Int32.TryParse(item.SelectSingleNode("owner").InnerText, out this.owner);
            Int32.TryParse(item.SelectSingleNode("entered_by").InnerText, out this.enteredBy);          
            this.email1 = item.SelectSingleNode("email1").InnerText;
            this.email2 = item.SelectSingleNode("email2").InnerText;
            this.webSite = item.SelectSingleNode("web_site").InnerText;
            this.isHot = CATSApi.StringToBool(item.SelectSingleNode("is_hot").InnerText);
            this.desiredPay = item.SelectSingleNode("desired_pay").InnerText;
            this.currentPay = item.SelectSingleNode("current_pay").InnerText;
            this.isActive = CATSApi.StringToBool(item.SelectSingleNode("is_active").InnerText);
            this.bestTimeToCall = item.SelectSingleNode("best_time_to_call").InnerText;
            Int32.TryParse(item.SelectSingleNode("country_id").InnerText, out this.countryId);      
            
            foreach (XmlNode extraFieldNode in xml.SelectNodes("//*[starts-with(name(.), 'extra_field')]"))
            {
                this.extraFields.Add(extraFieldNode.LocalName, extraFieldNode.InnerText);
            }
            
            base.ParseResponse(xml);
        }
    }
}

