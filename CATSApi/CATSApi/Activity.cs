// 
// Activity.cs
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
using System.Xml;

namespace CATS
{
    public class Activity
    {
        private int id = -1;
        public int Id
        {
            get { return this.id; }
        }
        
        private DateTime created = DateTime.MinValue;
        public DateTime Created
        {
            get { return this.created; }
        }
        
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }
        
        private string regarding = String.Empty;
        public string Regarding
        {
            get { return this.regarding; }
        }
        
        private string type = String.Empty;
        public string Type
        {
            get { return this.type; }
        }
        
        private string notes = String.Empty;
        public string Notes
        {
            get { return this.notes; }
        }
        
        private string enteredBy = String.Empty;
        public string EnteredBy
        {
            get { return this.enteredBy; }
        }
        
        public Activity(XmlNode xml)
        {
            Int32.TryParse(xml.SelectSingleNode("item_id").InnerText, out this.id);         
            DateTime.TryParse(
                xml.SelectSingleNode("created").InnerText,
                out this.created
            );          
            this.name = xml.SelectSingleNode("name").InnerText;
            this.regarding = xml.SelectSingleNode("regarding").InnerText;
            this.type = xml.SelectSingleNode("type").InnerText;
            this.notes = xml.SelectSingleNode("notes").InnerText;
            this.enteredBy = xml.SelectSingleNode("entered_by").InnerText;
        }
    }
}

