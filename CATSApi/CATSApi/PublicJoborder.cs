// 
// PublicJoborder.cs
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
    public class PublicJoborder
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
        
        private int recruiter = -1;
        public int Recruiter
        {
            get { return this.recruiter; }
        }
        
        private int contactId = -1;
        public int ContactId
        {
            get { return this.contactId; }
        }
        
        private int companyId = -1;
        public int CompanyId
        {
            get { return this.companyId; }
        }
        
        private int enteredBy = -1;
        public int EnteredBy
        {
            get { return this.enteredBy; }
        }
        
        private int owner = -1;
        public int Owner
        {
            get { return this.owner; }
        }
        
        private string clientJobId = String.Empty;
        public string ClientJobId
        {
            get { return this.clientJobId; }
        }
        
        private string title = String.Empty;
        public string Title
        {
            get { return this.title; }
        }
        
        private string description = String.Empty;
        public string Description
        {
            get { return this.description; }
        }
        
        private string notes = String.Empty;
        public string Notes
        {
            get { return this.notes; }
        }
        
        private CATSApi.JoborderType? type = null;
        public CATSApi.JoborderType Type
        {
            get { return (CATSApi.JoborderType)this.type; }
        }
        
        private string duration = String.Empty;
        public string Duration
        {
            get { return this.duration; }
        }
        
        private string rateMax = String.Empty;
        public string RateMax
        {
            get { return this.rateMax; }
        }
        
        private string salary = String.Empty;
        public string Salary
        {
            get { return this.salary; }
        }
        
        private string status = String.Empty;
        public string Status
        {
            get { return this.status; }
        }
        
        private bool? isHot = null;
        public bool IsHot
        {
            get { return (bool)this.isHot; }
        }
        
        private int openings = -1;
        public int Openings
        {
            get { return this.openings; }
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
        
        private DateTime startDate = DateTime.MinValue;
        public DateTime StartDate
        {
            get { return this.startDate; }
        }
        
        private DateTime dateCreated = DateTime.MinValue;
        public DateTime DateCreated
        {
            get { return this.dateCreated; }
        }
        
        private int companyDeparmentId = -1;
        public int CompanyDepartmentId
        {
            get { return this.companyDeparmentId; }
        }
        
        private int sourcerId = -1;
        public int SourcerId
        {
            get { return this.sourcerId; }
        }
        
        private int openingsAvailable = -1;
        public int OpeningsAvailable
        {
            get { return this.openingsAvailable; }
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
        
        public PublicJoborder(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("id").InnerText, out this.id);
            this.summary = xml.SelectSingleNode("summary").InnerText;
            Int32.TryParse(xml.SelectSingleNode("recruiter").InnerText, out this.recruiter);
            Int32.TryParse(xml.SelectSingleNode("contact_id").InnerText, out this.contactId);
            Int32.TryParse(xml.SelectSingleNode("company_id").InnerText, out this.companyId);
            Int32.TryParse(xml.SelectSingleNode("entered_by").InnerText, out this.enteredBy);
            Int32.TryParse(xml.SelectSingleNode("owner").InnerText, out this.owner);
            this.clientJobId = xml.SelectSingleNode("client_job_id").InnerText;
            this.title = xml.SelectSingleNode("title").InnerText;
            this.description = xml.SelectSingleNode("title").InnerText;
            this.notes = xml.SelectSingleNode("notes").InnerText;
            switch (xml.SelectSingleNode("type").InnerText)
            {
                case "H":
                    this.type = CATSApi.JoborderType.Hire;
                    break;
                case "C":
                    this.type = CATSApi.JoborderType.Contract;
                    break;
                case "C2H":
                    this.type = CATSApi.JoborderType.ContractToHire;
                    break;
                case "FL":
                    this.type = CATSApi.JoborderType.Freelance;
                    break;
            }
            this.duration = xml.SelectSingleNode("duration").InnerText;
            this.rateMax = xml.SelectSingleNode("rate_max").InnerText;
            this.salary = xml.SelectSingleNode("salary").InnerText;
            this.status = xml.SelectSingleNode("status").InnerText;
            this.isHot = CATSApi.StringToBool(xml.SelectSingleNode("is_hot").InnerText);
            Int32.TryParse(xml.SelectSingleNode("openings").InnerText, out this.openings);
            this.city = xml.SelectSingleNode("city").InnerText;
            this.state = xml.SelectSingleNode("state").InnerText;
            this.zip = xml.SelectSingleNode("zip").InnerText;
            DateTime.TryParse(xml.SelectSingleNode("start_date").InnerText, out this.startDate);
            DateTime.TryParse(xml.SelectSingleNode("date_created").InnerText, out this.dateCreated);
            Int32.TryParse(xml.SelectSingleNode("company_department_id").InnerText, out this.companyDeparmentId);
            Int32.TryParse(xml.SelectSingleNode("sourcer_id").InnerText, out this.sourcerId);
            Int32.TryParse(xml.SelectSingleNode("openings_available").InnerText, out this.openingsAvailable);
            Int32.TryParse(xml.SelectSingleNode("country_id").InnerText, out this.countryId);
            
            foreach (XmlNode extraFieldNode in xml.SelectNodes("//*[starts-with(name(.), 'extra_field')]"))
            {
                this.extraFields.Add(extraFieldNode.LocalName, extraFieldNode.InnerText);
            }                                   
        }
    }
}

