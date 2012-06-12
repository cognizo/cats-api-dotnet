// 
// List.cs
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
    public class List
    {
        private string id = String.Empty;
        public string Id
        {
            get { return this.id; }
        }
        
        private string name = String.Empty;
        public string Name
        {
            get { return this.name; }
        }
        
        private CATSApi.ListType? type = null;
        public CATSApi.ListType Type
        {
            get { return (CATSApi.ListType)this.type; }
        }
        
        private string owner = String.Empty;
        public string Owner
        {
            get { return this.owner; }
        }
        
        private DateTime updated = DateTime.MinValue;
        public DateTime Updated
        {
            get { return this.updated; }
        }
        
        public List(XmlNode xml)
        {
            this.id = xml.SelectSingleNode("item_id").InnerText;
            this.name = xml.SelectSingleNode("name").InnerText;
            this.type = (CATSApi.ListType)Enum.Parse(typeof(CATSApi.ListType), xml.SelectSingleNode("type").InnerText);
            this.owner = xml.SelectSingleNode("owner").InnerText;
            DateTime.TryParse(xml.SelectSingleNode("updated").InnerText, out this.updated);
        }
    }
}

