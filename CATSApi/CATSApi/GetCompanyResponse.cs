// 
// GetCompanyResponse.cs
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
    public class GetCompanyResponse : Response
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
        
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
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
        
        private string phone1 = String.Empty;
        public string Phone1
        {
            get { return this.phone1; }
        }
        
        private string phone2 = String.Empty;
        public string Phone2
        {
            get { return this.phone2; }
        }
        
        private string url = String.Empty;
        public string Url
        {
            get { return this.url; }
        }
        
        private string keyTechnologies = String.Empty;
        public string KeyTechnologies
        {
            get { return this.keyTechnologies; }
        }
        
        private string notes = String.Empty;
        public string Notes
        {
            get { return this.notes; }
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
        
        private bool? isHot = null;
        public bool IsHot
        {
            get { return (bool)this.isHot; }
        }
        
        private string faxNumber = String.Empty;
        public string FaxNumber
        {
            get { return this.faxNumber; }
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
        
        public GetCompanyResponse(string xml)
            : base(xml)
        {
        }
        
        protected override void ParseResponse(XmlDocument xml)
        {
            XmlNode item = xml.SelectSingleNode("/response/result");
                
            if (item == null)
            {
                return;
            }
            
            Int32.TryParse(item.SelectSingleNode("id").InnerText, out this.id);         
            this.summary = item.SelectSingleNode("summary").InnerText;
            this.name = item.SelectSingleNode("name").InnerText;
            this.address = item.SelectSingleNode("address").InnerText;
            this.city = item.SelectSingleNode("city").InnerText;
            this.state = item.SelectSingleNode("state").InnerText;
            this.zip = item.SelectSingleNode("zip").InnerText;
            this.phone1 = item.SelectSingleNode("phone1").InnerText;
            this.phone2 = item.SelectSingleNode("phone2").InnerText;
            this.url = item.SelectSingleNode("url").InnerText;
            this.keyTechnologies = item.SelectSingleNode("key_technologies").InnerText;
            this.notes = item.SelectSingleNode("notes").InnerText;
            Int32.TryParse(item.SelectSingleNode("entered_by").InnerText, out this.enteredBy);
            Int32.TryParse(item.SelectSingleNode("owner").InnerText, out this.owner);
            DateTime.TryParse(item.SelectSingleNode("date_created").InnerText, out this.dateCreated);
            DateTime.TryParse(item.SelectSingleNode("date_modified").InnerText, out this.dateModified);          
            this.isHot = CATSApi.StringToBool(item.SelectSingleNode("is_hot").InnerText);
            this.faxNumber = item.SelectSingleNode("fax_number").InnerText;
            Int32.TryParse(item.SelectSingleNode("country_id").InnerText, out this.countryId);                      
            
            foreach (XmlNode extraFieldNode in xml.SelectNodes("//*[starts-with(name(.), 'extra_field')]"))
            {
                this.extraFields.Add(extraFieldNode.LocalName, extraFieldNode.InnerText);
            }
            
            base.ParseResponse(xml);
        }
    }
}

